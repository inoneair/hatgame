using System;
using System.IO;
using UnityEngine;

public class MainMenuLogic
{
    private MainMenuView _mainMenuView;
    private GameSettingsController _gameSettingsController;

    private event Action _onStartGame;

    public MainMenuLogic(MainMenuView view, GameSettingsController gameSettingsController)
    {
        _mainMenuView = view;
        _gameSettingsController = gameSettingsController;

        var wordsFile = _gameSettingsController.wordsFile;
        if (File.Exists(wordsFile))
        {
            _mainMenuView.SetWordsFileWithoutNotify(wordsFile);
            _mainMenuView.isStartButtonInteractable = true;
        }
        else
        {
            _mainMenuView.isStartButtonInteractable = false;
        }

        _mainMenuView.SubscribeOnStartButtonCLick(OnStartButtonClickHandler);
        _mainMenuView.SubscribeOnWordsFileChosen(OnWordsFileChosenHandler);
        _mainMenuView.SubscribeOnExitButtonCLick(OnExitButtonClickHandler);
    }

    public void SubscribeOnStartGame(Action handler)
    {
        _onStartGame += handler;
    }

    private void OnStartButtonClickHandler() => _onStartGame?.Invoke();

    private void OnWordsFileChosenHandler(string wordsFile)
    {
        if(File.Exists(wordsFile))
        {
            _gameSettingsController.wordsFile = wordsFile;
            _mainMenuView.isStartButtonInteractable = true;
        }            
    }

    private void OnExitButtonClickHandler() => Application.Quit();
}
