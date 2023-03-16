using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private bool _isPaused;
    private bool _isRunning;
    private CancellationTokenSource _cts;

    private event Action _onTimerStarted;
    private event Action<float, float> _onTimerTick; //<timeLeft, deltaTime>
    private event Action _onTimerFinished;
    private event Action<bool> _onIsPaused;

    public bool isPaused
    {
        get => _isPaused;
        set
        {
            if (_isPaused != value)
            {
                _isPaused = value;
                _onIsPaused?.Invoke(_isPaused);
            }
        }
    }

    public bool isRunning => _isRunning;        
    
    public Timer()
    {
    }

    public async void Start(float seconds)
    {
        Reset();
        _cts = new CancellationTokenSource();
        _isRunning = true;

        await StartInternal(seconds, _cts.Token);

        _cts?.Dispose();
        _cts = null;
        _isRunning = false;
    }

    private async Task StartInternal(float seconds, CancellationToken token)
    {
        _onTimerStarted?.Invoke();

        var secondsLeft = seconds;
        while (secondsLeft > 0)
        {
            await Task.Yield();

            if (!isPaused)
            {
                secondsLeft -= Time.deltaTime;
                _onTimerTick?.Invoke(secondsLeft, Time.deltaTime);
            }

            if (token.IsCancellationRequested)
            {
                _onTimerFinished?.Invoke();
                return;
            }
        }

        _onTimerFinished?.Invoke();
    }

    public void Reset()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
            _isRunning = false;
        }
    }

    public void SubscribeOnTimerStarted(Action handler)
    {
        _onTimerStarted += handler;
    }

    public void SubscribeOnTimerTick(Action<float, float> handler)
    {
        _onTimerTick += handler;
    }

    public void SubscribeOnTimerFinished(Action handler)
    {
        _onTimerFinished += handler;
    }

    public void SubscribeIsPaused(Action<bool> handler)
    {
        _onIsPaused += handler;
    }
}
