using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Coroutine _coroutine;

    public string text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public bool isTimerRunnig => _coroutine != null;

    public void StartTimer(float seconds, Action callback)
    {
        StopTimer();

        _coroutine = StartCoroutine(TimerCoroutine(seconds, callback));
    }

    public void StopTimer()
    {
        if (isTimerRunnig)
            StopCoroutine(_coroutine);
    }

    private IEnumerator TimerCoroutine(float seconds, Action callback)
    {
        while (seconds > 0)
        {
            yield return null;
            seconds -= Time.deltaTime;
            _text.text = ((int)seconds).ToString();
        }

        _coroutine = null;
        callback();
    }
}
