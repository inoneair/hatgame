using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerEntity
{
    private SinglePlayerMainMenuLogic _mainMenuLogic;
    private SinglePlayerInGameLogic _inGameLogic;

    public SinglePlayerEntity(SinglePlayerMainMenuLogic mainMenulogic, SinglePlayerInGameLogic inGameLogic)        
    {
        _mainMenuLogic = mainMenulogic;
        _mainMenuLogic.SubscribeOnStartGame(StartGame);

        _inGameLogic = inGameLogic;
        _inGameLogic.SubscribeOnReturn(ToMainMenu);
    }

    private async void StartGame()
    {
        await _inGameLogic.StartPlay();
    }

    private void ToMainMenu()
    {
        _mainMenuLogic.Show();
    }
}
