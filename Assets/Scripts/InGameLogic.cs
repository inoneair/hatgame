using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class InGameLogic
{
    private InGameMenuView _inGameMenuView;
    private LoadingScreenView _loadingScreenView;
    private GameSettingsController _gameSettingsController;
    private WordsLibraryController _wordsLibraryController;
    private GuessWordsLogic _guessWordsLogic;
    private Timer _roundTimer;

    private Action _onReturn;

    private int _guessedWordsCount = 0;

    public InGameLogic(InGameMenuView inGameMenuView, LoadingScreenView loadingScreenView, GameSettingsController gameSettingsController, WordsLibraryController wordsLibraryController)
    {
        _inGameMenuView = inGameMenuView;
        _loadingScreenView = loadingScreenView;
        _gameSettingsController = gameSettingsController;
        _wordsLibraryController = wordsLibraryController;
        _guessWordsLogic = new GuessWordsLogic();
        _roundTimer = new Timer();

        _inGameMenuView.SetIsPauseActiveWithoutNotify(false);

        _inGameMenuView.SubscribeStartRoundButtonClick(OnStartRoundButtonHandler);
        _inGameMenuView.SubscribeIsRoundPauseActiveChanged((isPauseActive) => _roundTimer.isPaused = isPauseActive);
        _inGameMenuView.SubscribeWordGuessedButtonClick(OnWordGuessedButtonHandler);
        _inGameMenuView.SubscribeSkipWordButtonClick(OnSkipWordButtonHandler);
        _inGameMenuView.SubscribeReturnButtonClick(OnReturnButtonClickHandler);

        _roundTimer.SubscribeOnTimerTick(OnTimerTickHandler);
        _roundTimer.SubscribeOnTimerFinished(OnTimerFinishedHandler);
        _roundTimer.SubscribeIsPaused(OnIsRoundTimerPausedHandler);
    }

    public async Task InitLogicToPlay() => await SetReadyToPlayFirstRoundState();

    public void SubscribeOnReturn(Action handler)
    {
        _onReturn += handler;
    }

    private async Task SetReadyToPlayFirstRoundState()
    {
        _loadingScreenView.SwitchOn();
        _loadingScreenView.SetState(LoadingScreenView.State.Loading);
        
        var wordsToGuess = await _wordsLibraryController.LoadWords(_gameSettingsController.wordsGroup);

        _guessWordsLogic.StartToGuess(wordsToGuess);
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;

        _inGameMenuView.startRoundButtonEnabled = true;
        _inGameMenuView.pauseRoundViewEnabled = false;
        _inGameMenuView.wordGuessedButtonEnabled = false;
        _inGameMenuView.skipWordButtonEnabled = false;
        _inGameMenuView.noWordsMessageEnabled = false;
        _inGameMenuView.timerViewEnabled = false;
        _inGameMenuView.wordViewEnabled = false;
        _inGameMenuView.wordsGuessedCountViewEnabled = false;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;

        _loadingScreenView.SetState(LoadingScreenView.State.Complete);

        await _loadingScreenView.SwitchOffWithAnimation();
    }

    private void SetReadyToPlayAnotherRoundState()
    {
        _inGameMenuView.startRoundButtonEnabled = true;
        _inGameMenuView.pauseRoundViewEnabled = false;
        _inGameMenuView.wordGuessedButtonEnabled = false;
        _inGameMenuView.skipWordButtonEnabled = false;
        _inGameMenuView.noWordsMessageEnabled = false;
        _inGameMenuView.timerViewEnabled = false;
        _inGameMenuView.wordViewEnabled = false;
        _inGameMenuView.wordsGuessedCountViewEnabled = true;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;
    }

    private void SetPlayingRoundState()
    {
        _inGameMenuView.wordToGuess = _guessWordsLogic.GetNextWord();

        _roundTimer.Start(_gameSettingsController.roundDuration);

        _guessedWordsCount = 0;

        _inGameMenuView.startRoundButtonEnabled = false;
        _inGameMenuView.pauseRoundViewEnabled = true;
        _inGameMenuView.wordGuessedButtonEnabled = true;
        _inGameMenuView.skipWordButtonEnabled = true;
        _inGameMenuView.noWordsMessageEnabled = false;
        _inGameMenuView.timerViewEnabled = true;
        _inGameMenuView.wordViewEnabled = true;
        _inGameMenuView.wordsGuessedCountViewEnabled = false;
    }

    private void SetNoWordsState()
    {
        _roundTimer.Reset();
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;

        _inGameMenuView.startRoundButtonEnabled = false;
        _inGameMenuView.pauseRoundViewEnabled = false;
        _inGameMenuView.wordGuessedButtonEnabled = false;
        _inGameMenuView.skipWordButtonEnabled = false;
        _inGameMenuView.noWordsMessageEnabled = true;
        _inGameMenuView.timerViewEnabled = false;
        _inGameMenuView.wordViewEnabled = false;
        _inGameMenuView.wordsGuessedCountViewEnabled = true;
    }

    private void OnStartRoundButtonHandler()
    {
        SetPlayingRoundState();
    }

    private void OnWordGuessedButtonHandler()
    {
        ++_guessedWordsCount;
        TryToGetNextWord();
    }

    private void OnIsRoundTimerPausedHandler(bool isPaused)
    {
        _inGameMenuView.wordGuessedButtonInteractable = !isPaused;
        _inGameMenuView.skipWordButtonInteractable = !isPaused;

        _inGameMenuView.SetIsPauseActiveWithoutNotify(isPaused);
    }

    private void OnSkipWordButtonHandler() => TryToGetNextWord();

    private void OnReturnButtonClickHandler()
    {
        _roundTimer.Reset();
        _roundTimer.isPaused = false;
        _onReturn?.Invoke();
    }

    private void OnTimerTickHandler(float timeLeft, float deltaTime)
    {
        _inGameMenuView.timerValue = (int)timeLeft;
    }

    private void OnTimerFinishedHandler()
    {
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;
        if (_guessWordsLogic.wordsToGuessCount != 0)
            SetReadyToPlayAnotherRoundState();
    }

    private void TryToGetNextWord()
    {
        var nextWord = _guessWordsLogic.GetNextWord();
        if (nextWord == null)
            SetNoWordsState();
        else
            _inGameMenuView.wordToGuess = nextWord;
    }
}
