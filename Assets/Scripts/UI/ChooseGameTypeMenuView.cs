using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGameTypeMenuView : MonoBehaviour
{
    [SerializeField] Button _singlePlayerButton;
    [SerializeField] Button _multiPlayerButton;

    private Action _onSinglePlayerButtonClick;
    private Action _onMultiPlayerButtonClick;

    private void Awake()
    {
        _singlePlayerButton.onClick.AddListener(OnSinglePlayerButtonClickHandler);
        _multiPlayerButton.onClick.AddListener(OnMultiPlayerButtonClickHandler);
    }

    public void SubscribeOnSinglePlayerButtonClick(Action handler)
    {
        _onSinglePlayerButtonClick += handler;
    }

    public void SubscribeOnMultiplayerButtonClick(Action handler)
    {
        _onMultiPlayerButtonClick += handler;
    }

    private void OnSinglePlayerButtonClickHandler() => _onSinglePlayerButtonClick?.Invoke();

    private void OnMultiPlayerButtonClickHandler() => _onMultiPlayerButtonClick?.Invoke();
}
