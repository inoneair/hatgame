using System;
using UnityEngine;

public class MainMenuLogic
{
    private MainMenuView _mainMenuView;
    private GameSettingsController _gameSettingsController;
    private WordsLibraryController _wordsLibraryController;

    private event Action _onStartGame;

    public MainMenuLogic(MainMenuView view, GameSettingsController gameSettingsController, WordsLibraryController wordsLibraryController)
    {
        _mainMenuView = view;
        _gameSettingsController = gameSettingsController;
        _wordsLibraryController = wordsLibraryController;

        if (_gameSettingsController.roundDuration < 1)
            _gameSettingsController.roundDuration = 60;

        _mainMenuView.SetRoundDurationWithoutNotify(_gameSettingsController.roundDuration);

        _mainMenuView.SubscribeOnStartButtonCLick(OnStartButtonClickHandler);
        _mainMenuView.SubscribeOnRoundDurationChanged(OnRoundDurationChangedHandler);
        _mainMenuView.SubscribeOnChooseWordsGroup(OnChooseWordsGroupHandler);
        _mainMenuView.SetWordsGroups(_wordsLibraryController.GetGroups());

        _mainMenuView.isStartButtonInteractable = false;
    }

    public void SubscribeOnStartGame(Action handler)
    {
        _onStartGame += handler;
    }

    private void OnStartButtonClickHandler() => _onStartGame?.Invoke();

    private void OnRoundDurationChangedHandler(int roundDuration)
    {
        if (roundDuration > 0)
            _gameSettingsController.roundDuration = roundDuration;
        else
            _mainMenuView.SetRoundDurationWithoutNotify(_gameSettingsController.roundDuration);
    }

    private void OnChooseWordsGroupHandler(string group)
    {
        _mainMenuView.isStartButtonInteractable = group != null;
        _gameSettingsController.wordsGroup = group;        
    }
}
