using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenuView : MonoBehaviour
{
    [SerializeField] Button _returnButton;

    private Action _onReturnButtonClick;

    private void Awake()
    {
        _returnButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void OnDestroy()
    {
        _returnButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void OnReturnButtonClickHandler() => _onReturnButtonClick?.Invoke();
}
