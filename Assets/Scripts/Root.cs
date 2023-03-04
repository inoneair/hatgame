using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private WebFileLoader _webFileLoader;
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private InGameMenuView _inGameMenuView;
    [SerializeField] private LoadingScreenView _loadingScreenView;

    private WordsLibraryController _wordsLibraryController;
    private GameSettingsController _gameSettingsController;
    private MainMenuLogic _mainMenuLogic;
    private InGameLogic _inGameLogic;

    private async void Awake()
    {
        _webFileLoader.Init();
        _loadingScreenView.SwitchOff();

        _wordsLibraryController = new WordsLibraryController();
        await _wordsLibraryController.Init();

        _gameSettingsController = new GameSettingsController();
        _mainMenuLogic = new MainMenuLogic(_mainMenuView, _gameSettingsController, _wordsLibraryController);
        _mainMenuLogic.SubscribeOnStartGame(StartGame);

        _inGameLogic = new InGameLogic(_inGameMenuView, _loadingScreenView, _gameSettingsController, _wordsLibraryController);
        _inGameLogic.SubscribeOnReturn(ToMainMenu);

        ToMainMenu();
    }

    private async void StartGame()
    {
        _mainMenuView.gameObject.SetActive(false);
        _inGameMenuView.gameObject.SetActive(true);

        await _inGameLogic.InitLogicToPlay();
    }

    private void ToMainMenu()
    {
        _mainMenuView.gameObject.SetActive(true);
        _inGameMenuView.gameObject.SetActive(false);
    }
}
