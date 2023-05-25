using System;
using Hatgame.Multiplayer;
using Hatgame.Common;

public class CreateOrJoinLobbyMenuLogic : IMenuLogic
{
    private CreateOrJoinLobbyMenuView _createOrJoinLobbyMenuView;

    private NetworkController _networkController;
    private ClientMacthmakingController _matchmakingController;

    private string _lobbyName;

    private Action _onReturn;
    private Action _onShow;

    public CreateOrJoinLobbyMenuLogic(CreateOrJoinLobbyMenuView createOrJoinLobbyMenuView)
    {
        _networkController = NetworkController.instance;
        _matchmakingController = ClientMacthmakingController.instance;
        _createOrJoinLobbyMenuView = createOrJoinLobbyMenuView;

        _networkController.SubscribeOnClientConnect(OnConnected);
        _networkController.SubscribeOnClientDisconnect(OnDisconnected);

        _createOrJoinLobbyMenuView.HideErrorText();
        _createOrJoinLobbyMenuView.isCreateLobbyButtonInteractable = false;
        _createOrJoinLobbyMenuView.isJoinLobbyButtonInteractable = false;

        _createOrJoinLobbyMenuView.SubscribeOnCreateLobbyButtonClick(TryToCreateLobby);
        _createOrJoinLobbyMenuView.SubscribeOnJoinLobbyButtonCLick(TryToJoinLobby);
        _createOrJoinLobbyMenuView.SubscribeOnLobbyNameChanged(SetLobbyName);
        _createOrJoinLobbyMenuView.SubscribeOnReturnButtonClick(ReturnToPreviousMenu);
                
        Hide();
    }

    public void Show()
    {
        _createOrJoinLobbyMenuView.gameObject.SetActive(true);

        _onShow?.Invoke();
    }

    public void Hide()
    {
        _createOrJoinLobbyMenuView.gameObject.SetActive(false);
    }

    public IDisposable SubscribeOnReturn(Action handler)
    {
        _onReturn += handler;

        return new Unsubscriber(() => _onReturn -= handler);
    }

    public IDisposable SubscribeOnShow(Action handler)
    {
        _onShow += handler;

        return new Unsubscriber(() => _onShow -= handler);
    }

    private void TryToCreateLobby()
    {
        _matchmakingController.CreateLobby(_lobbyName);
    }

    private void TryToJoinLobby()
    {
        _matchmakingController.JoinLobby(_lobbyName);
    }

    private void SetLobbyName(string name)
    {
        _lobbyName = name;

        UpdateCreateAndJoinButtons();
    }

    private void UpdateCreateAndJoinButtons()
    {
        var isLobbyNameValid = !string.IsNullOrWhiteSpace(_lobbyName);
        var isReadyToCreateOrJoinLobby = isLobbyNameValid && NetworkController.instance.isNetworkActive;
        _createOrJoinLobbyMenuView.isCreateLobbyButtonInteractable = isReadyToCreateOrJoinLobby;
        _createOrJoinLobbyMenuView.isJoinLobbyButtonInteractable = isReadyToCreateOrJoinLobby;
    }

    private void ReturnToPreviousMenu()
    {
        _onReturn?.Invoke();
        Hide();
    }

    private void OnConnected()
    {
        _createOrJoinLobbyMenuView.isSearchServerRunning = false;

        UpdateCreateAndJoinButtons();
    }

    private void OnDisconnected()
    {
        _createOrJoinLobbyMenuView.isSearchServerRunning = false;

        UpdateCreateAndJoinButtons();
    }
}
