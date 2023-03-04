using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] SetRoundDurationView _setRoundDurationView;
    [SerializeField] ChooseWordsGroupView _chooseWordsGroupView;

    private Action _onStartButonClick;
    private Action<int> _onRoundDurationChanged;
    private Action<string> _onChooseWordsGroup;

    public bool isStartButtonInteractable
    {
        get => _startButton.interactable;
        set => _startButton.interactable = value;
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClickHandler);
        _setRoundDurationView.SubscribeOnRoundDurationChanged(OnRoundDurationChangedHandler);
        _chooseWordsGroupView.SubscribeOnChooseWordsGroup(OnChooseWordsGroupHandler);
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

    public void SubscribeOnRoundDurationChanged(Action<int> handler)
    {
        _onRoundDurationChanged += handler;
    }

    public void SubscribeOnChooseWordsGroup(Action<string> handler)
    {
        _onChooseWordsGroup += handler;
    }

    private void OnStartButtonClickHandler() => _onStartButonClick?.Invoke();

    private void OnRoundDurationChangedHandler(int roundDuration) => _onRoundDurationChanged?.Invoke(roundDuration);

    private void OnChooseWordsGroupHandler(string group) => _onChooseWordsGroup?.Invoke(group);

}
