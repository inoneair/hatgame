using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private WebFileLoader _webFileLoader;
    [SerializeField] private SinglePlayerMainMenuView _singlePlayerMainMenuView;
    [SerializeField] private SinglePlayerInGameMenuView _singlePlayerInGameMenuView;
    [SerializeField] private LoadingScreenView _loadingScreenView;
    [SerializeField] private ChooseGameTypeMenuView _chooseGameTypeMenuView;

    private WordsLibraryController _wordsLibraryController;
    private GameSettingsController _gameSettingsController;
    private ChooseGameTypeMenuLogic _chooseGameTypeMenuLogic;

    private async void Awake()
    {
        _webFileLoader.Init();
        _loadingScreenView.SwitchOff();

        _gameSettingsController = new GameSettingsController();
        _wordsLibraryController = new WordsLibraryController();
        await _wordsLibraryController.Init();

        var singlePlayerMainMenuLogic = new SinglePlayerMainMenuLogic(_singlePlayerMainMenuView, _gameSettingsController, _wordsLibraryController);
        var singlePlayerInGameLogic = new SinglePlayerInGameLogic(_singlePlayerInGameMenuView, _loadingScreenView, _gameSettingsController, _wordsLibraryController);

        var multiPlayerMainMenuLogic = new MultiPlayerMainMenuLogic();

        _chooseGameTypeMenuLogic = new ChooseGameTypeMenuLogic(_chooseGameTypeMenuView, singlePlayerMainMenuLogic, multiPlayerMainMenuLogic);

        var singlePlayerEntity = new SinglePlayerEntity(singlePlayerMainMenuLogic, singlePlayerInGameLogic);

        //ToMainMenu();
    }

}
