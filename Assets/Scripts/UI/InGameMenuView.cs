using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SinglePlayerInGameMenuView : MonoBehaviour
{
    [SerializeField] private SwitchingButton _startFinishRoundButton;
    [SerializeField] private Button _wordGuessedButton;
    [SerializeField] private SwitchingButton _pauseRoundView;
    [SerializeField] private Button _skipWordButton;
    [SerializeField] private Button _returnButton;

    [SerializeField] private TMP_Text _noWordsMessage;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private WordView _wordView;
    [SerializeField] private WordsList _guessedWordsList;
    [SerializeField] private WordsList _skippedWordsList;

    private event Action<bool> _onStartFinishRoundButtonClick;
    private event Action _onWordGuessedButtonClick;
    private event Action<bool> _onIsRoundPauseActiveChanged;
    private event Action _onSkipWordButtonClick;
    private event Action _onReturnButtonClick;

    public bool startFinishRoundButtonEnabled
    {
        get => _startFinishRoundButton.gameObject.activeSelf;
        set => _startFinishRoundButton.gameObject.SetActive(value);
    }

    public bool pauseRoundViewEnabled
    {
        get => _pauseRoundView.gameObject.activeSelf;
        set => _pauseRoundView.gameObject.SetActive(value);
    }

    public bool wordGuessedButtonEnabled
    {
        get => _wordGuessedButton.gameObject.activeSelf;
        set => _wordGuessedButton.gameObject.SetActive(value);
    }

    public bool skipWordButtonEnabled
    {
        get => _skipWordButton.gameObject.activeSelf;
        set => _skipWordButton.gameObject.SetActive(value);
    }

    public bool noWordsMessageEnabled
    {
        get => _noWordsMessage.gameObject.activeSelf;
        set => _noWordsMessage.gameObject.SetActive(value);
    }

    public bool timerViewEnabled
    {
        get => _timerView.gameObject.activeSelf;
        set => _timerView.gameObject.SetActive(value);
    }

    public bool wordViewEnabled
    {
        get => _wordView.gameObject.activeSelf;
        set => _wordView.gameObject.SetActive(value);
    }

    public bool guessedWordsListEnabled
    {
        get => _guessedWordsList.gameObject.activeSelf;
        set => _guessedWordsList.gameObject.SetActive(value);
    }

    public bool skippedWordsListEnabled
    {
        get => _skippedWordsList.gameObject.activeSelf;
        set => _skippedWordsList.gameObject.SetActive(value);
    }

    public bool wordGuessedButtonInteractable
    {
        get => _wordGuessedButton.interactable;
        set => _wordGuessedButton.interactable = value;
    }

    public bool skipWordButtonInteractable
    {
        get => _skipWordButton.interactable;
        set => _skipWordButton.interactable = value;
    }

    public bool pauseRoundViewInteractable
    {
        get => _pauseRoundView.interactable;
        set => _pauseRoundView.interactable = value;
    }

    public int timerValue
    {
        set => _timerView.text = value.ToString();
    }

    public string wordToGuess
    {
        set => _wordView.text = value;
    }

    public bool isPauseActive
    {
        get => _pauseRoundView.isOn;
        set => _pauseRoundView.isOn = value;
    }

    private void Awake()
    {
        _startFinishRoundButton.SubscribeIsOnChanged(OnStartFinishRoundButtonClickHandler);
        _wordGuessedButton.onClick.AddListener(OnWordGuessedButtonClickHandler);
        _pauseRoundView.SubscribeIsOnChanged(OnIsRoundPauseActiveChangedHandler);
        _skipWordButton.onClick.AddListener(OnSkipWordButtonClickHandler);
        _returnButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void OnDestroy()
    {
        _startFinishRoundButton.UnsubscribeIsOnChanged(OnStartFinishRoundButtonClickHandler);
        _wordGuessedButton.onClick.RemoveListener(OnWordGuessedButtonClickHandler);
        _skipWordButton.onClick.RemoveListener(OnSkipWordButtonClickHandler);
        _returnButton.onClick.RemoveListener(OnReturnButtonClickHandler);
    }

    public void SetIsPauseActiveWithoutNotify(bool value) =>
        _pauseRoundView.SetIsOnWithoutNotify(value);

    public void SetStartButtonStateWithoutNotify() => _startFinishRoundButton.SetIsOnWithoutNotify(false);

    public void SetGuessedWords(IList<string> words)
    {
        //_guessedWordsList.label = $"Отгадано слов -\n{words.Count}";
        _guessedWordsList.SetWords(words);
    }

    public void SetSkippedWords(IList<string> words)
    {
        //_skippedWordsList.label = $"Пропущено слов -\n{words.Count}";
        _skippedWordsList.SetWords(words);
    }

    public void SubscribeStartFinishRoundButtonClick(Action<bool> handler)
    {
        _onStartFinishRoundButtonClick += handler;
    }

    public void SubscribeWordGuessedButtonClick(Action handler)
    {
        _onWordGuessedButtonClick += handler;
    }

    public void SubscribeIsRoundPauseActiveChanged(Action<bool> handler)
    {
        _onIsRoundPauseActiveChanged += handler;
    }

    public void SubscribeSkipWordButtonClick(Action handler)
    {
        _onSkipWordButtonClick += handler;
    }

    public void SubscribeReturnButtonClick(Action handler)
    {
        _onReturnButtonClick += handler;
    }

    private void OnStartFinishRoundButtonClickHandler(bool value)
    {
        _onStartFinishRoundButtonClick?.Invoke(value);
    }

    private void OnWordGuessedButtonClickHandler()
    {
        _onWordGuessedButtonClick?.Invoke();
    }

    private void OnIsRoundPauseActiveChangedHandler(bool value)
    {
        _onIsRoundPauseActiveChanged?.Invoke(value);
    }

    private void OnSkipWordButtonClickHandler()
    {
        _onSkipWordButtonClick?.Invoke();
    }

    private void OnReturnButtonClickHandler()
    {
        _onReturnButtonClick?.Invoke();
    }
}
