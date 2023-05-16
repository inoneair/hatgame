using System;
using UnityEngine;
using UnityEngine.UI;

public class CreateOrJoinLobbyMenuView : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _joinLobbyButton;
    [SerializeField] private InputField _lobbyInputField;
    [SerializeField] private Text _errorText;
    [SerializeField] private Button _returnButton;

    private Action _onCreateLobbyButonClick;
    private Action _onJoinLobbyButtonClick;
    private Action _onReturnButtonClick;
    private Action<string> _onLobbyNameChanged;

    public bool isCreateLobbyButtonInteractable
    {
        get => _createLobbyButton.interactable;
        set => _createLobbyButton.interactable = value;
    }

    public bool isJoinLobbyButtonInteractable
    {
        get => _joinLobbyButton.interactable;
        set => _joinLobbyButton.interactable = value;
    }

    public string errorText
    {
        get => _errorText.text;
        set => _errorText.text = value;
    }

    public bool isErrorTextShown =>
        _errorText.gameObject.activeSelf;    

    private void Awake()
    {
        _createLobbyButton.onClick.AddListener(OnCreateLobbyButtonClickHandler);
        _joinLobbyButton.onClick.AddListener(OnJoinLobbyButtonClickHandler);
        _lobbyInputField.onValueChanged.AddListener(OnLobbyNameChangedHandler);
        _returnButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void OnDestroy()
    {
        _createLobbyButton.onClick.RemoveListener(OnCreateLobbyButtonClickHandler);
        _joinLobbyButton.onClick.RemoveListener(OnJoinLobbyButtonClickHandler);
        _lobbyInputField.onValueChanged.RemoveListener(OnLobbyNameChangedHandler);
        _returnButton.onClick.RemoveListener(OnReturnButtonClickHandler);
    }

    public void ShowErrorText()
    {
        _errorText.gameObject.SetActive(true);
    }

    public void HideErrorText()
    {
        _errorText.gameObject.SetActive(false);
    }

    public void SubscribeOnCreateLobbyButtonClick(Action handler)
    {
        _onCreateLobbyButonClick += handler;
    }

    public void SubscribeOnJoinLobbyButtonCLick(Action handler)
    {
        _onJoinLobbyButtonClick += handler;
    }

    public void SubscribeOnReturnButtonClick(Action handler)
    {
        _onReturnButtonClick += handler;
    }

    public void SubscribeOnLobbyNameChanged(Action<string> handler)
    {
        _onLobbyNameChanged += handler;
    }

    private void OnCreateLobbyButtonClickHandler() => _onCreateLobbyButonClick?.Invoke();

    private void OnJoinLobbyButtonClickHandler() => _onJoinLobbyButtonClick?.Invoke();

    private void OnLobbyNameChangedHandler(string lobbyName) => _onLobbyNameChanged.Invoke(lobbyName);

    private void OnReturnButtonClickHandler() => _onReturnButtonClick?.Invoke();
}
