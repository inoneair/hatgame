using System;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerMainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private SetRoundDurationView _setRoundDurationView;
    [SerializeField] private ChooseWordsGroupView _chooseWordsGroupView;
    [SerializeField] private Button _returnButton;

    private Action _onStartButonClick;
    private Action<bool> _onIsInfiniteRoundDurationChanged;
    private Action<int> _onRoundDurationChanged;
    private Action<string> _onChooseWordsGroup;
    private Action _onReturnButtonClick;

    public bool isStartButtonInteractable
    {
        get => _startButton.interactable;
        set => _startButton.interactable = value;
    }

    public bool isRoundDurationFieldInteractable
    {
        get => _setRoundDurationView.isRoundDurationInputFieldInteractable;
        set => _setRoundDurationView.isRoundDurationInputFieldInteractable = value;
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClickHandler);
        _setRoundDurationView.SubscribeOnIsInfiniteRoundDurationChanged(OnIsInfiniteRoundDurationChangedHandler);
        _setRoundDurationView.SubscribeOnRoundDurationChanged(OnRoundDurationChangedHandler);
        _chooseWordsGroupView.SubscribeOnChooseWordsGroup(OnChooseWordsGroupHandler);
        _returnButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClickHandler);
    }

    public void SetRoundDurationWithoutNotify(int roundDuration) =>
        _setRoundDurationView.SetRoundDurationWithoutNotify(roundDuration);

    public void SetWordsGroups(string[] groups) =>    
        _chooseWordsGroupView.SetGroups(groups);

    public void SubscribeOnStartButtonCLick(Action handler)
    {
        _onStartButonClick += handler;
    }

    public void SubscribeOnIsInfiniteRoundDurationChanged(Action<bool> handler)
    {
        _onIsInfiniteRoundDurationChanged += handler;
    }

    public void SubscribeOnRoundDurationChanged(Action<int> handler)
    {
        _onRoundDurationChanged += handler;
    }

    public void SubscribeOnChooseWordsGroup(Action<string> handler)
    {
        _onChooseWordsGroup += handler;
    }

    public void SubscribeOnReturnButtonClick(Action handler)
    {
        _onReturnButtonClick += handler;
    }

    private void OnStartButtonClickHandler() => _onStartButonClick?.Invoke();

    private void OnIsInfiniteRoundDurationChangedHandler(bool value) => _onIsInfiniteRoundDurationChanged?.Invoke(value);

    private void OnRoundDurationChangedHandler(int roundDuration) => _onRoundDurationChanged?.Invoke(roundDuration);

    private void OnChooseWordsGroupHandler(string group) => _onChooseWordsGroup?.Invoke(group);

    private void OnReturnButtonClickHandler() => _onReturnButtonClick?.Invoke();

}
