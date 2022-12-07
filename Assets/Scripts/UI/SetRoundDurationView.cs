using System;
using UnityEngine;
using TMPro;

public class SetRoundDurationView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _roundDurationInputField;

    private int _roundDuration;

    private event Action<int> _onRoundDurationChanged;

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

    private void Awake()
    {
        _roundDurationInputField.onEndEdit.AddListener(OnRoundDurationChangedHandler);
    }

    private void OnDestroy()
    {
        _roundDurationInputField.onEndEdit.RemoveListener(OnRoundDurationChangedHandler);
    }

    public void SubscribeOnRoundDurationChanged(Action<int> handler)
    {
        _onRoundDurationChanged += handler;
    }

    public void SetRoundDurationWithoutNotify(int value)
    {
        _roundDuration = value;
        _roundDurationInputField.text = value.ToString();
    }

    private void OnRoundDurationChangedHandler(string inputStr)
    {
        _roundDuration = int.Parse(inputStr);
        _onRoundDurationChanged?.Invoke(_roundDuration);
    }
}
