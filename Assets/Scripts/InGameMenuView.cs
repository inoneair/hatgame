using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameMenuView : MonoBehaviour
{
    [SerializeField] private Button _startRoundButton;
    [SerializeField] private Button _wordGuessedButton;
    [SerializeField] private Button _skipWordButton;
    [SerializeField] private Button _returnButton;

    [SerializeField] private TMP_Text _noWordsMessage;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private WordView _wordView;
    [SerializeField] private WordsGuessedCountView _wordsGuessedCountView;

    private event Action _onStartRoundButtonClick;
    private event Action _onWordGuessedButtonClick;
    private event Action _onSkipWordButtonClick;
    private event Action _onReturnButtonClick;

    private Coroutine _coroutine;

    public bool isTimerRunnig => _coroutine != null;

    public bool StartRoundButtonEnabled
    {
        get => _startRoundButton.gameObject.activeSelf;
        set => _startRoundButton.gameObject.SetActive(value);
    }

    public bool WordGuessedButtonEnabled
    {
        get => _wordGuessedButton.gameObject.activeSelf;
        set => _wordGuessedButton.gameObject.SetActive(value);
    }

    public bool SkipWordButtonEnabled
    {
        get => _skipWordButton.gameObject.activeSelf;
        set => _skipWordButton.gameObject.SetActive(value);
    }

    public bool NoWordsMessageEnabled
    {
        get => _noWordsMessage.gameObject.activeSelf;
        set => _noWordsMessage.gameObject.SetActive(value);
    }

    public bool TimerViewEnabled
    {
        get => _timerView.gameObject.activeSelf;
        set => _timerView.gameObject.SetActive(value);
    }

    public bool WordViewEnabled
    {
        get => _wordView.gameObject.activeSelf;
        set => _wordView.gameObject.SetActive(value);
    }

    public bool WordsGuessedCountViewEnabled
    {
        get => _wordsGuessedCountView.gameObject.activeSelf;
        set => _wordsGuessedCountView.gameObject.SetActive(value);
    }

    public float timerValue
    {
        set => _timerView.text = value.ToString();
    }

    public string wordToGuess
    {
        set => _wordView.text = value;
    }

    public int wordsGuessedCount
    {
        set => _wordsGuessedCountView.text = value.ToString();
    }

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

    private void Awake()
    {
        _startRoundButton.onClick.AddListener(OnStartRoundButtonClickHandler);
        _wordGuessedButton.onClick.AddListener(OnWordGuessedButtonClickHandler);
        _skipWordButton.onClick.AddListener(OnSkipWordButtonClickHandler);
        _returnButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void OnDestroy()
    {
        _startRoundButton.onClick.RemoveListener(OnStartRoundButtonClickHandler);
        _wordGuessedButton.onClick.RemoveListener(OnWordGuessedButtonClickHandler);
        _skipWordButton.onClick.RemoveListener(OnSkipWordButtonClickHandler);
        _returnButton.onClick.RemoveListener(OnReturnButtonClickHandler);
    }

    public void SubscribeStartRoundButtonClick(Action handler)
    {
        _onStartRoundButtonClick += handler;
    }

    public void SubscribeWordGuessedButtonClick(Action handler)
    {
        _onWordGuessedButtonClick += handler;
    }

    public void SubscribeSkipWordButtonClick(Action handler)
    {
        _onSkipWordButtonClick += handler;
    }

    public void SubscribeReturnButtonClick(Action handler)
    {
        _onReturnButtonClick += handler;
    }

    private void OnStartRoundButtonClickHandler()
    {
        _onStartRoundButtonClick?.Invoke();
    }

    private void OnWordGuessedButtonClickHandler()
    {
        _onWordGuessedButtonClick?.Invoke();
    }

    private void OnSkipWordButtonClickHandler()
    {
        _onSkipWordButtonClick?.Invoke();
    }

    private void OnReturnButtonClickHandler()
    {
        _onReturnButtonClick?.Invoke();
    }

    private IEnumerator TimerCoroutine(float seconds, Action callback)
    {
        while (seconds > 0)
        {
            yield return null;
            seconds -= Time.deltaTime;
            _timerView.text = ((int)seconds).ToString();
        }

        _coroutine = null;
        callback();
    }
}
