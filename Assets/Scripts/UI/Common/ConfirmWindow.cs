using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Hatgame.Common;

public class ConfirmWindow : MonoBehaviour
{
    [SerializeField] Button _confirmButton;
    [SerializeField] Button _cancelButton;
    [SerializeField] TMP_Text _textInfoText;

    private TMP_Text _confirmButtonTextComponent;
    private TMP_Text _cancelButtonTextComponent;

    private string _infoText;
    private string _confirmButtonText;
    private string _cancelButtonText;

    private Action _onClickConfirmButton;
    private Action _onClickCancelButton;

    // Start is called before the first frame update
    private void Awake()
    {
        (_confirmButtonText, _confirmButtonTextComponent) = TryToGetTextFromButton(_confirmButton);
        (_cancelButtonText, _cancelButtonTextComponent) = TryToGetTextFromButton(_cancelButton);
        _infoText = TryToGetTextFromComponent (_textInfoText);

        _confirmButton.onClick.AddListener(OnClickConfirmButtonHandler);
        _confirmButton.onClick.AddListener(OnClickCancelButtonHandler);
    }

    private void OnDestroy()
    {
        _confirmButton.onClick.RemoveListener(OnClickConfirmButtonHandler);
        _cancelButton.onClick.RemoveListener(OnClickCancelButtonHandler);
    }

    public string infoText
    {
        get => _textInfoText.text;
        set
        {
            _infoText = value;

            if (_textInfoText != null)
                _textInfoText.text = _infoText;
        }
    }

    public string confirmButtonText
    {
        get => _confirmButtonText;
        set
        {
            _confirmButtonText = value;

            if (_confirmButtonTextComponent != null)
                _confirmButtonTextComponent.text = _confirmButtonText;
        }
    }

    public string cancelButtonText
    {
        get => _confirmButtonText;
        set
        {
            _confirmButtonText = value;

            if (_cancelButtonTextComponent != null)
                _cancelButtonTextComponent.text = _cancelButtonText;
        }
    }

    public IDisposable SubscribeOnClickConfirmButton(Action handler)
    {
        _onClickConfirmButton += handler;

        return new Unsubscriber(() => _onClickConfirmButton -= handler);
    }

    public IDisposable SubscribeOnClickCancelButton(Action handler)
    {
        _onClickCancelButton += handler;

        return new Unsubscriber(() => _onClickCancelButton -= handler);
    }

    private (string text, TMP_Text component) TryToGetTextFromButton(Button button)
    {
        var component = button.GetComponentInChildren<TMP_Text>();
        var text = TryToGetTextFromComponent(component);

        return (text, component);
    }

    private string TryToGetTextFromComponent(TMP_Text component) => component!=null ? component.text : string.Empty;

    private void OnClickConfirmButtonHandler()
    {
        _onClickConfirmButton?.Invoke();
    }

    private void OnClickCancelButtonHandler()
    {
        _onClickCancelButton?.Invoke();
    }
}
