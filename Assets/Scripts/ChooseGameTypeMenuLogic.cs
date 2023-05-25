using System;

public class ChooseGameTypeMenuLogic : IMenuLogic
{
    private ChooseGameTypeMenuView _view;

    private IMenuLogic _singlePlayerMainMenu;
    private IMenuLogic _createOrJoinLobbyMenu;

    public ChooseGameTypeMenuLogic(ChooseGameTypeMenuView view, IMenuLogic singlePlayerMainMenu, IMenuLogic createOrJoinLobbyMenu)
    {
        _view = view;
        _singlePlayerMainMenu = singlePlayerMainMenu;
        _createOrJoinLobbyMenu = createOrJoinLobbyMenu;

        _singlePlayerMainMenu.SubscribeOnReturn(OnReturnToChooseGameType);
        createOrJoinLobbyMenu.SubscribeOnReturn(OnReturnToChooseGameType);

        _view.SubscribeOnSinglePlayerButtonClick(OnSinglePlayerButtonClickHandler);
        _view.SubscribeOnMultiplayerButtonClick(OnMultiPlayerButtonClickHandler);

        Show();
    }

    private void OnSinglePlayerButtonClickHandler()
    {
        Hide();
        _singlePlayerMainMenu.Show();
    }

    private void OnMultiPlayerButtonClickHandler()
    {
        Hide();
        _createOrJoinLobbyMenu.Show();
    }

    private void OnReturnToChooseGameType() => Show();    

    public void Show()
    {
        _view.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _view.gameObject.SetActive(false);
    }

    public IDisposable SubscribeOnShow(Action handler)
    {
        throw new NotImplementedException();
    }

    public IDisposable SubscribeOnReturn(Action handler)
    {
        throw new NotImplementedException();
    }
}
