using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerEntity
{
    private SinglePlayerMainMenuLogic _mainMenuLogic;
    private SinglePlayerInGameLogic _inGameLogic;

    public SinglePlayerEntity(SinglePlayerMainMenuView mainMenuView, SinglePlayerInGameMenuView inGameMenuView, LoadingScreenView loadingScreenView, GameSettingsController gameSettingsController, WordsLibraryController wordsLibraryController)        
    {
        _mainMenuLogic = new SinglePlayerMainMenuLogic(mainMenuView, gameSettingsController, wordsLibraryController);
        _mainMenuLogic.SubscribeOnStartGame(StartGame);

        _inGameLogic = new SinglePlayerInGameLogic(inGameMenuView, loadingScreenView, gameSettingsController, wordsLibraryController);
        _inGameLogic.SubscribeOnReturn(ToMainMenu);
    }

    public IMenuLogic mainMenuLogic => _mainMenuLogic;

    private async void StartGame()
    {
        await _inGameLogic.StartPlay();
    }

    private void ToMainMenu()
    {
        _mainMenuLogic.Show();
    }
}
