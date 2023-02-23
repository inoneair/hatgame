using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] SetRoundDurationView _setRoundDurationView;
    [SerializeField] Button _exitButton;

    private Action _onStartButonClick;
    private Action<string> _onWordsFileChosen;
    private Action<int> _onRoundDurationChanged;
    private Action _onExitButonClick;

    public bool isStartButtonInteractable
    {
        get => _startButton.interactable;
        set => _startButton.interactable = value;
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClickHandler);
        _setRoundDurationView.SubscribeOnRoundDurationChanged(OnRoundDurationChangedHandler);
        _exitButton.onClick.AddListener(OnExitButtonClickHandler);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClickHandler);
        _exitButton.onClick.RemoveListener(OnExitButtonClickHandler);
    }

    public void SetRoundDurationWithoutNotify(int roundDuration) =>
        _setRoundDurationView.SetRoundDurationWithoutNotify(roundDuration);

    public void SubscribeOnStartButtonCLick(Action handler)
    {
        _onStartButonClick += handler;
    }

    public void SubscribeOnWordsFileChosen(Action<string> handler)
    {
        _onWordsFileChosen += handler;
    }

    public void SubscribeOnRoundDurationChanged(Action<int> handler)
    {
        _onRoundDurationChanged += handler;
    }

    public void SubscribeOnExitButtonCLick(Action handler)
    {
        _onExitButonClick += handler;
    }

    private void OnStartButtonClickHandler() => _onStartButonClick?.Invoke();

    private void OnWordsFileChosenHandler(string filePath) => _onWordsFileChosen?.Invoke(filePath);

    private void OnRoundDurationChangedHandler(int roundDuration) => _onRoundDurationChanged?.Invoke(roundDuration);

    private void OnExitButtonClickHandler() => _onExitButonClick?.Invoke();
}
