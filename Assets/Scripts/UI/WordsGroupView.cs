using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordsGroupView : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private TMP_Text _text;

    public Action<WordsGroupView> OnChooseItem;

    private void Awake()
    {
        _toggle.onValueChanged.AddListener(OnValueChangedHandler);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChangedHandler);
    }

    public string text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public bool isOn
    {
        get => _toggle.isOn;
        set => _toggle.isOn = value;
    }

    public void SetIsOnWithoutNotify(bool value) =>
        _toggle.SetIsOnWithoutNotify(value);
    
    private void OnValueChangedHandler(bool value) =>
        OnChooseItem?.Invoke(this);   
    
}
