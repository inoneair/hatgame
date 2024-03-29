using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingButton : MonoBehaviour
{
    [SerializeField] private Button _onStateButton;
    [SerializeField] private Button _offStateButton;

    private bool _isOn;
    private bool _interactable;

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

    public bool interactable
    {
        get => _interactable;
        set
        {
            _interactable = value;

            _onStateButton.interactable = _interactable;
            _offStateButton.interactable = _interactable;
        }
    }

    private void Awake()
    {
        interactable = _onStateButton.interactable & _offStateButton.interactable;
        
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
