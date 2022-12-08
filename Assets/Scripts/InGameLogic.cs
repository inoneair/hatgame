using System;

public class InGameLogic
{
    private InGameMenuView _inGameMenuView;
    private GameSettingsController _gameSettingsController;
    private GuessWordsLogic _guessWordsLogic;
    private Timer _roundTimer;

    private Action _onReturn;

    private int _guessedWordsCount = 0;

    public InGameLogic(InGameMenuView view, GameSettingsController gameSettingsController)
    {
        _inGameMenuView = view;
        _gameSettingsController = gameSettingsController;
        _guessWordsLogic = new GuessWordsLogic();
        _roundTimer = new Timer();

        _inGameMenuView.SubscribeStartRoundButtonClick(OnStartRoundButtonHandler);
        _inGameMenuView.SubscribeWordGuessedButtonClick(OnWordGuessedButtonHandler);
        _inGameMenuView.SubscribeSkipWordButtonClick(OnSkipWordButtonHandler);
        _inGameMenuView.SubscribeReturnButtonClick(OnReturnButtonClickHandler);

        _roundTimer.SubscribeOnTimerTick(OnTimerTickHandler);
        _roundTimer.SubscribeOnTimerFinished(OnTimerFinishedHandler);
    }

    public void InitLogicToPlay() => SetReadyToPlayFirstRoundState();

    public void SubscribeOnReturn(Action handler)
    {
        _onReturn += handler;
    }

    private void SetReadyToPlayFirstRoundState()
    {
        var wordsToGuess = WordsToGuessLoader.LoadWords(_gameSettingsController.wordsFile);
        _guessWordsLogic.StartToGuess(wordsToGuess);
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;

        _inGameMenuView.StartRoundButtonEnabled = true;
        _inGameMenuView.WordGuessedButtonEnabled = false;
        _inGameMenuView.SkipWordButtonEnabled = false;
        _inGameMenuView.NoWordsMessageEnabled = false;
        _inGameMenuView.TimerViewEnabled = false;
        _inGameMenuView.WordViewEnabled = false;
        _inGameMenuView.WordsGuessedCountViewEnabled = false;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;
    }

    private void SetReadyToPlayAnotherRoundState()
    {
        _inGameMenuView.StartRoundButtonEnabled = true;
        _inGameMenuView.WordGuessedButtonEnabled = false;
        _inGameMenuView.SkipWordButtonEnabled = false;
        _inGameMenuView.NoWordsMessageEnabled = false;
        _inGameMenuView.TimerViewEnabled = false;
        _inGameMenuView.WordViewEnabled = false;
        _inGameMenuView.WordsGuessedCountViewEnabled = true;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;
    }

    private void SetPlayingRoundState()
    {
        _inGameMenuView.wordToGuess = _guessWordsLogic.GetNextWord();

        _roundTimer.Start(_gameSettingsController.roundDuration);

        _guessedWordsCount = 0;

        _inGameMenuView.StartRoundButtonEnabled = false;
        _inGameMenuView.WordGuessedButtonEnabled = true;
        _inGameMenuView.SkipWordButtonEnabled = true;
        _inGameMenuView.NoWordsMessageEnabled = false;
        _inGameMenuView.TimerViewEnabled = true;
        _inGameMenuView.WordViewEnabled = true;
        _inGameMenuView.WordsGuessedCountViewEnabled = false;
    }

    private void SetNoWordsState()
    {
        _roundTimer.Reset();
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;

        _inGameMenuView.StartRoundButtonEnabled = false;
        _inGameMenuView.WordGuessedButtonEnabled = false;
        _inGameMenuView.SkipWordButtonEnabled = false;
        _inGameMenuView.NoWordsMessageEnabled = true;
        _inGameMenuView.TimerViewEnabled = false;
        _inGameMenuView.WordViewEnabled = false;
        _inGameMenuView.WordsGuessedCountViewEnabled = true;
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

    private void OnSkipWordButtonHandler() => TryToGetNextWord();

    private void OnReturnButtonClickHandler()
    {
        _roundTimer.Reset();
        _onReturn?.Invoke();
    }

    private void OnTimerTickHandler(float timeLeft, float deltaTime)
    {
        _inGameMenuView.timerValue = (int)timeLeft;
    }

    private void OnTimerFinishedHandler()
    {
        _inGameMenuView.wordsGuessedCount = _guessedWordsCount;
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
