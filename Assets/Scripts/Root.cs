using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Root : MonoBehaviour
{
    [SerializeField] private string _serverAddress;
    [SerializeField] private Transport _networkTransport;
    [SerializeField] private WebFileLoader _webFileLoader;
    [SerializeField] private SinglePlayerMainMenuView _singlePlayerMainMenuView;
    [SerializeField] private SinglePlayerInGameMenuView _singlePlayerInGameMenuView;
    [SerializeField] private LoadingScreenView _loadingScreenView;
    [SerializeField] private ChooseGameTypeMenuView _chooseGameTypeMenuView;
    [SerializeField] private CreateOrJoinLobbyMenuView _createOrJoinLobbyMenuView;
    [SerializeField] private ConfirmWindow _confirmWindow;

    private WordsLibraryController _wordsLibraryController;
    private GameSettingsController _gameSettingsController;
    private ChooseGameTypeMenuLogic _chooseGameTypeMenuLogic;

    private async void Awake()
    {
        _webFileLoader.Init();
        _loadingScreenView.SwitchOff();
        _confirmWindow.gameObject.SetActive(false);

        _gameSettingsController = new GameSettingsController();
        _wordsLibraryController = new WordsLibraryController();
        await _wordsLibraryController.Init();

        var singlePlayerEntity = new SinglePlayerEntity(_singlePlayerMainMenuView, _singlePlayerInGameMenuView, _loadingScreenView, _gameSettingsController, _wordsLibraryController);
        var multiPlayerEntity = new MultiPlayerEntity(_createOrJoinLobbyMenuView, _serverAddress, _networkTransport );

        _chooseGameTypeMenuLogic = new ChooseGameTypeMenuLogic(_chooseGameTypeMenuView, singlePlayerEntity.mainMenuLogic, multiPlayerEntity.createOrJoinLobbyMenuLogic);

    }

}
