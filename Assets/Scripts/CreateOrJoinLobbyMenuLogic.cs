using System;

public class CreateOrJoinLobbyMenuLogic : IMenuLogic
{
    private CreateOrJoinLobbyMenuView _createOrJoinLobbyMenuView;

    private string _lobbyName;

    private Action _onReturn;

    public CreateOrJoinLobbyMenuLogic(CreateOrJoinLobbyMenuView createOrJoinLobbyMenuView)
    {
        _createOrJoinLobbyMenuView = createOrJoinLobbyMenuView;

        _createOrJoinLobbyMenuView.HideErrorText();
        _createOrJoinLobbyMenuView.isCreateLobbyButtonInteractable = false;
        _createOrJoinLobbyMenuView.isJoinLobbyButtonInteractable = false;

        _createOrJoinLobbyMenuView.SubscribeOnCreateLobbyButtonClick(TryToCreateLobby);
        _createOrJoinLobbyMenuView.SubscribeOnJoinLobbyButtonCLick(TryToJoinLobby);
        _createOrJoinLobbyMenuView.SubscribeOnLobbyNameChanged(SetLobbyName);
        _createOrJoinLobbyMenuView.SubscribeOnReturnButtonClick(ReturnToPreviousMenu);
                
        Hide();
    }

    private void TryToCreateLobby()
    {

    }

    private void TryToJoinLobby()
    {

    }

    private void SetLobbyName(string name)
    {
        _lobbyName = name;

        var isLobbyNameValid = !string.IsNullOrWhiteSpace(_lobbyName);
        _createOrJoinLobbyMenuView.isCreateLobbyButtonInteractable = isLobbyNameValid;
        _createOrJoinLobbyMenuView.isJoinLobbyButtonInteractable = isLobbyNameValid;
    }

    private void ReturnToPreviousMenu()
    {
        _onReturn?.Invoke();
        Hide();
    }

    public void Show()
    {
        _createOrJoinLobbyMenuView.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _createOrJoinLobbyMenuView.gameObject.SetActive(false);
    }

    public void SubscribeOnReturn(Action handler)
    {
        _onReturn += handler;
    }
}
