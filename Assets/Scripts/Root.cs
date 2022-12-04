using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private InGameMenuView _inGameMenuView;

    private GameSettingsController _gameSettingsController;
    private MainMenuLogic _mainMenuLogic;
    private InGameLogic _inGameLogic;

    private void Awake()
    {
        _gameSettingsController = new GameSettingsController();
        _mainMenuLogic = new MainMenuLogic(_mainMenuView, _gameSettingsController);
        _mainMenuLogic.SubscribeOnStartGame(StartGame);

        _inGameLogic = new InGameLogic(_inGameMenuView, _gameSettingsController);
        _inGameLogic.SubscribeOnReturn(ToMainMenu);

        ToMainMenu();
    }

    private void StartGame()
    {
        _mainMenuView.gameObject.SetActive(false);
        _inGameMenuView.gameObject.SetActive(true);

        _inGameLogic.InitLogicToPlay();
    }

    private void ToMainMenu()
    {
        _mainMenuView.gameObject.SetActive(true);
        _inGameMenuView.gameObject.SetActive(false);
    }
}
