using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] SetRoundDurationView _setRoundDurationView;

    private Action _onStartButonClick;
    private Action<int> _onRoundDurationChanged;

    public bool isStartButtonInteractable
    {
        get => _startButton.interactable;
        set => _startButton.interactable = value;
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClickHandler);
        _setRoundDurationView.SubscribeOnRoundDurationChanged(OnRoundDurationChangedHandler);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClickHandler);
    }

    public void SetRoundDurationWithoutNotify(int roundDuration) =>
        _setRoundDurationView.SetRoundDurationWithoutNotify(roundDuration);

    public void SubscribeOnStartButtonCLick(Action handler)
    {
        _onStartButonClick += handler;
    }

    public void SubscribeOnRoundDurationChanged(Action<int> handler)
    {
        _onRoundDurationChanged += handler;
    }

    private void OnStartButtonClickHandler() => _onStartButonClick?.Invoke();

    private void OnRoundDurationChangedHandler(int roundDuration) => _onRoundDurationChanged?.Invoke(roundDuration);

}
