using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameMenuView : MonoBehaviour
{
    [SerializeField] private Button _startRoundButton;
    [SerializeField] private Button _wordGuessedButton;
    [SerializeField] private PauseRoundView _pauseRoundView;
    [SerializeField] private Button _skipWordButton;
    [SerializeField] private Button _returnButton;

    [SerializeField] private TMP_Text _noWordsMessage;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private WordView _wordView;
    [SerializeField] private WordsGuessedCountView _wordsGuessedCountView;

    private event Action _onStartRoundButtonClick;
    private event Action _onWordGuessedButtonClick;
    private event Action<bool> _onIsRoundPauseActiveChanged;
    private event Action _onSkipWordButtonClick;
    private event Action _onReturnButtonClick;

    public bool startRoundButtonEnabled
    {
        get => _startRoundButton.gameObject.activeSelf;
        set => _startRoundButton.gameObject.SetActive(value);
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

    public bool wordsGuessedCountViewEnabled
    {
        get => _wordsGuessedCountView.gameObject.activeSelf;
        set => _wordsGuessedCountView.gameObject.SetActive(value);
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

    public int timerValue
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

    public bool isPauseActive
    {
        get => _pauseRoundView.isPauseActive;
        set => _pauseRoundView.isPauseActive = value;
    }

    private void Awake()
    {
        _startRoundButton.onClick.AddListener(OnStartRoundButtonClickHandler);
        _wordGuessedButton.onClick.AddListener(OnWordGuessedButtonClickHandler);
        _pauseRoundView.SubscribeIsPauseActiveChanged(OnIsRoundPauseActiveChangedHandler);
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

    public void SetIsPauseActiveWithoutNotify(bool value) =>
        _pauseRoundView.SetIsPauseActiveWithoutNotify(value);
    
    public void SubscribeStartRoundButtonClick(Action handler)
    {
        _onStartRoundButtonClick += handler;
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

    private void OnStartRoundButtonClickHandler()
    {
        _onStartRoundButtonClick?.Invoke();
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
