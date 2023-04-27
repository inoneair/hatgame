using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGameTypeMenuLogic
{
    private ChooseGameTypeMenuView _view;

    private SinglePlayerMainMenuLogic _singlePlayerMainMenuLogic;
    private MultiPlayerMainMenuLogic _multiPlayerMainMenuLogic;

    public ChooseGameTypeMenuLogic(ChooseGameTypeMenuView view, SinglePlayerMainMenuLogic singlePlayerMainMenuLogic, MultiPlayerMainMenuLogic multiPlayerMainMenuLogic)
    {
        _view = view;
        _singlePlayerMainMenuLogic = singlePlayerMainMenuLogic;
        _multiPlayerMainMenuLogic = multiPlayerMainMenuLogic;

        _singlePlayerMainMenuLogic.SubscribeOnReturn(OnReturnToChooseGameType);

        _view.SubscribeOnSinglePlayerButtonClick(OnSinglePlayerButtonClickHandler);
        _view.SubscribeOnMultiplayerButtonClick(OnMultiPlayerButtonClickHandler);

        _view.gameObject.SetActive(true);
    }

    private void OnSinglePlayerButtonClickHandler()
    {
        _view.gameObject.SetActive(false);
        _singlePlayerMainMenuLogic.Show();
    }

    private void OnMultiPlayerButtonClickHandler()
    {
        //_view.gameObject.SetActive(false);
    }

    private void OnReturnToChooseGameType()
    {
        _view.gameObject.SetActive(true);
    }
}
