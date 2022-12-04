using System;
using System.Collections;
using System.Collections.Generic;

public class InGameLogic
{
    private InGameMenuView _inGameMenuView;
    private GameSettingsController _gameSettingsController;
    private GuessWordsLogic _guessWordsLogic;

    private Action _onReturn;

    private int _guessedWordsCount = 0;

    public InGameLogic(InGameMenuView view, GameSettingsController gameSettingsController)
    {
        _inGameMenuView = view;
        _gameSettingsController = gameSettingsController;
        _guessWordsLogic = new GuessWordsLogic();

        _inGameMenuView.SubscribeStartRoundButtonClick(OnStartRoundButtonHandler);
        _inGameMenuView.SubscribeWordGuessedButtonClick(OnWordGuessedButtonHandler);
        _inGameMenuView.SubscribeSkipWordButtonClick(OnSkipWordButtonHandler);
        _inGameMenuView.SubscribeReturnButtonClick(OnReturnButtonClickHandler);
    }

    public void InitLogicToPlay()
    {
        var wordsToGuess = WordsToGuessLoader.LoadWords(_gameSettingsController.wordsFile);
        _guessWordsLogic.StartToGuess(wordsToGuess);
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;

        SetReadyToPlayFirstRoundState();
    }

    public void SubscribeOnReturn(Action handler)
    {
        _onReturn += handler;
    }

    private void SetReadyToPlayFirstRoundState()
    {
        _inGameMenuView.StartRoundButtonEnabled = true;
        _inGameMenuView.WordGuessedButtonEnabled = false;
        _inGameMenuView.SkipWordButtonEnabled = false;
        _inGameMenuView.TimerViewEnabled = true;
        _inGameMenuView.WordViewEnabled = false;
        _inGameMenuView.WordsGuessedCountViewEnabled = false;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;
    }

    private void SetReadyToPlayAnotherRoundState()
    {
        _inGameMenuView.StartRoundButtonEnabled = true;
        _inGameMenuView.WordGuessedButtonEnabled = false;
        _inGameMenuView.SkipWordButtonEnabled = false;
        _inGameMenuView.TimerViewEnabled = true;
        _inGameMenuView.WordViewEnabled = false;
        _inGameMenuView.WordsGuessedCountViewEnabled = true;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;
    }

    private void SetPlayingRoundState()
    {
        _inGameMenuView.StartRoundButtonEnabled = false;
        _inGameMenuView.WordGuessedButtonEnabled = true;
        _inGameMenuView.SkipWordButtonEnabled = true;
        _inGameMenuView.TimerViewEnabled = true;
        _inGameMenuView.WordViewEnabled = true;
        _inGameMenuView.WordsGuessedCountViewEnabled = false;
    }

    private void OnStartRoundButtonHandler()
    {
        _inGameMenuView.wordToGuess = _guessWordsLogic.GetNextWord();

        _inGameMenuView.StartTimer(_gameSettingsController.roundDuration, OnTimerFinished);

        SetPlayingRoundState();
    }

    private void OnWordGuessedButtonHandler()
    {
        ++_guessedWordsCount;
        _inGameMenuView.wordToGuess = _guessWordsLogic.GetNextWord();
    }

    private void OnSkipWordButtonHandler()
    {
        _inGameMenuView.wordToGuess = _guessWordsLogic.GetNextWord();
    }

    private void OnReturnButtonClickHandler()
    {
        _inGameMenuView.StopTimer();
        _onReturn?.Invoke();
    }

    private void OnTimerFinished()
    {
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;
        SetReadyToPlayAnotherRoundState();
    }
}
