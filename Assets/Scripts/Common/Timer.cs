using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private CancellationTokenSource _cts;

    private event Action _onTimerStarted;

    private event Action<float, float> _onTimerTick; //<timeLeft, deltaTime>

    private event Action _onTimerFinished;

    public bool isActive { get; private set; }

    public bool isPaused { get; set; }

    public Timer()
    {
    }

    public async void Start(float seconds)
    {
        Reset();
        _cts = new CancellationTokenSource();

        await StartInternal(seconds, _cts.Token);

        _cts?.Dispose();
        _cts = null;
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
}
