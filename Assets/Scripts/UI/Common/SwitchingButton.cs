using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingButton : MonoBehaviour
{
    [SerializeField] private Button _onStateButton;
    [SerializeField] private Button _offStateButton;

    private bool _isOn;

    private Action<bool> _isOnChanged;

    public bool isOn
    {
        get => _isOn;
        set
        {
            if (_isOn != value)
            {
                SetIsOnWithoutNotify(value);
                _isOnChanged?.Invoke(_isOn);
            }
        }
    }

    private void Awake()
    {
        _onStateButton.onClick.AddListener(OnStateButtonClickHandler);
        _offStateButton.onClick.AddListener(OffStateButtonClickHandler);

        SetIsOnWithoutNotify(false);
    }

    private void OnDestroy()
    {
        _onStateButton.onClick.RemoveListener(OnStateButtonClickHandler);
        _offStateButton.onClick.RemoveListener(OffStateButtonClickHandler);
    }

    public void SetIsOnWithoutNotify(bool value)
    {
        _isOn = value;

        _onStateButton.gameObject.SetActive(!value);
        _offStateButton.gameObject.SetActive(value);
    }

    public void SubscribeIsOnChanged(Action<bool> handler)
    {
        _isOnChanged += handler;
    }

    public void UnsubscribeIsOnChanged(Action<bool> handler)
    {
        if (_isOnChanged != null)
            _isOnChanged -= handler;
    }

    private void OnStateButtonClickHandler() =>
        isOn = true;

    private void OffStateButtonClickHandler() =>
        isOn = false;
}
