using System;
using UnityEngine;
using UnityEngine.UI;

public class SetRoundDurationView : MonoBehaviour
{
    [SerializeField] private Toggle _isInfiniteRoundDurationToggle;
    [SerializeField] private InputField _roundDurationInputField;

    private bool _isInfiniteRoundDuration;
    private int _roundDuration;

    private event Action<int> _onRoundDurationChanged;
    private event Action<bool> _onIsInfiniteRoundDurationChanged;

    public bool isInfiniteRoundDuration
    {
        get => _isInfiniteRoundDuration;
        set
        {
            if (_isInfiniteRoundDuration != value)
            {
                _isInfiniteRoundDuration = value;
                _isInfiniteRoundDurationToggle.isOn = value;
            }
        }
    }

    public int roundDuration
    {
        get => _roundDuration;
        set
        {
            if (_roundDuration != value)
            {
                _roundDuration = value;
                _roundDurationInputField.text = _roundDuration.ToString();

                _onRoundDurationChanged?.Invoke(_roundDuration);
            }
        }
    }

    public bool isRoundDurationInputFieldInteractable
    {
        get => _roundDurationInputField.interactable;
        set => _roundDurationInputField.interactable = value;
    }

    private void Awake()
    {
        _isInfiniteRoundDurationToggle.onValueChanged.AddListener(OnIsInfiniteRoundDurationChangedHandler);
        _roundDurationInputField.onEndEdit.AddListener(OnRoundDurationChangedHandler);
    }

    private void OnDestroy()
    {
        _isInfiniteRoundDurationToggle.onValueChanged.RemoveListener(OnIsInfiniteRoundDurationChangedHandler);
        _roundDurationInputField.onEndEdit.RemoveListener(OnRoundDurationChangedHandler);
    }

    public void SubscribeOnIsInfiniteRoundDurationChanged(Action<bool> handler)
    {
        _onIsInfiniteRoundDurationChanged += handler;
    }

    public void SubscribeOnRoundDurationChanged(Action<int> handler)
    {
        _onRoundDurationChanged += handler;
    }

    public void SetIsInfiniteRoundDurationWithoutNotify(bool value)
    {
        _isInfiniteRoundDuration = value;
        _isInfiniteRoundDurationToggle.SetIsOnWithoutNotify(value);
    }

    public void SetRoundDurationWithoutNotify(int value)
    {
        _roundDuration = value;
        _roundDurationInputField.text = value.ToString();
    }

    private void OnIsInfiniteRoundDurationChangedHandler(bool isOn)
    {
        _isInfiniteRoundDuration = isOn;
        _onIsInfiniteRoundDurationChanged?.Invoke(_isInfiniteRoundDuration);
    }

    private void OnRoundDurationChangedHandler(string inputStr)
    {
        _roundDuration = int.Parse(inputStr);
        _onRoundDurationChanged?.Invoke(_roundDuration);
    }
}
