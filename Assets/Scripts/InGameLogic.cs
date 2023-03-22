using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class InGameLogic
{
    private InGameMenuView _inGameMenuView;
    private LoadingScreenView _loadingScreenView;
    private GameSettingsController _gameSettingsController;
    private WordsLibraryController _wordsLibraryController;
    private GuessWordsLogic _guessWordsLogic;
    private Timer _roundTimer;

    private Action _onReturn;

    private List<string> _guessedWords = new List<string>();
    private List<string> _skippedWords = new List<string>();

    public InGameLogic(InGameMenuView inGameMenuView, LoadingScreenView loadingScreenView, GameSettingsController gameSettingsController, WordsLibraryController wordsLibraryController)
    {
        _inGameMenuView = inGameMenuView;
        _loadingScreenView = loadingScreenView;
        _gameSettingsController = gameSettingsController;
        _wordsLibraryController = wordsLibraryController;
        _guessWordsLogic = new GuessWordsLogic();
        _roundTimer = new Timer();

        _inGameMenuView.SetIsPauseActiveWithoutNotify(false);
        _inGameMenuView.SetStartButtonStateWithoutNotify();

        _inGameMenuView.SubscribeStartFinishRoundButtonClick(OnStartRoundButtonHandler);
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
        
        _inGameMenuView.pauseRoundViewInteractable = !_gameSettingsController.isInfiniteRoundDuration;

        _inGameMenuView.startFinishRoundButtonEnabled = true;
        _inGameMenuView.pauseRoundViewEnabled = false;
        _inGameMenuView.wordGuessedButtonEnabled = false;
        _inGameMenuView.skipWordButtonEnabled = false;
        _inGameMenuView.noWordsMessageEnabled = false;
        _inGameMenuView.timerViewEnabled = false;
        _inGameMenuView.wordViewEnabled = false;
        _inGameMenuView.guessedWordsListEnabled = false;
        _inGameMenuView.skippedWordsListEnabled = false;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;

        _loadingScreenView.SetState(LoadingScreenView.State.Complete);

        await _loadingScreenView.SwitchOffWithAnimation();
    }

    private void SetReadyToPlayAnotherRoundState()
    {
        _inGameMenuView.startFinishRoundButtonEnabled = true;
        _inGameMenuView.pauseRoundViewEnabled = false;
        _inGameMenuView.wordGuessedButtonEnabled = false;
        _inGameMenuView.skipWordButtonEnabled = false;
        _inGameMenuView.noWordsMessageEnabled = false;
        _inGameMenuView.timerViewEnabled = false;
        _inGameMenuView.wordViewEnabled = false;

        _inGameMenuView.SetGuessedWords(_guessedWords);
        _inGameMenuView.SetSkippedWords(_skippedWords);
        _inGameMenuView.guessedWordsListEnabled = true;
        _inGameMenuView.skippedWordsListEnabled = true;

        _inGameMenuView.timerValue = _gameSettingsController.roundDuration;
    }

    private void SetPlayingRoundState()
    {
        _guessedWords.Clear();
        _skippedWords.Clear();

        _inGameMenuView.wordToGuess = _guessWordsLogic.GetNextWord();

        if(!_gameSettingsController.isInfiniteRoundDuration)
            _roundTimer.Start(_gameSettingsController.roundDuration);

        _inGameMenuView.startFinishRoundButtonEnabled = true;
        _inGameMenuView.pauseRoundViewEnabled = true;
        _inGameMenuView.wordGuessedButtonEnabled = true;
        _inGameMenuView.skipWordButtonEnabled = true;
        _inGameMenuView.noWordsMessageEnabled = false;
        _inGameMenuView.timerViewEnabled = !_gameSettingsController.isInfiniteRoundDuration & true;
        _inGameMenuView.wordViewEnabled = true;
        _inGameMenuView.guessedWordsListEnabled = false;
        _inGameMenuView.skippedWordsListEnabled = false;
    }

    private void SetNoWordsState()
    {
        if (!_gameSettingsController.isInfiniteRoundDuration)
            _roundTimer.Reset();

        _inGameMenuView.startFinishRoundButtonEnabled = false;
        _inGameMenuView.pauseRoundViewEnabled = false;
        _inGameMenuView.wordGuessedButtonEnabled = false;
        _inGameMenuView.skipWordButtonEnabled = false;
        _inGameMenuView.noWordsMessageEnabled = true;
        _inGameMenuView.timerViewEnabled = false;
        _inGameMenuView.wordViewEnabled = false;

        _inGameMenuView.SetGuessedWords(_guessedWords);
        _inGameMenuView.SetSkippedWords(_skippedWords);
        _inGameMenuView.guessedWordsListEnabled = true;
        _inGameMenuView.skippedWordsListEnabled = true;
    }

    private void OnStartRoundButtonHandler(bool isStartClicked)
    {
        if (isStartClicked)
            SetPlayingRoundState();
        else
            FinishRound();
    }

    private void OnWordGuessedButtonHandler()
    {
        _guessedWords.Add(_guessWordsLogic.currentGuessingWord);
        TryToGetNextWord();
    }

    private void OnSkipWordButtonHandler()
    {
        _skippedWords.Add(_guessWordsLogic.currentGuessingWord);
        TryToGetNextWord();
    }

    private void OnIsRoundTimerPausedHandler(bool isPaused)
    {
        _inGameMenuView.wordGuessedButtonInteractable = !isPaused;
        _inGameMenuView.skipWordButtonInteractable = !isPaused;

        _inGameMenuView.SetIsPauseActiveWithoutNotify(isPaused);
    }

    private void OnReturnButtonClickHandler()
    {
        FinishRound();

        _onReturn?.Invoke();
    }

    private void OnTimerTickHandler(float timeLeft, float deltaTime)
    {
        _inGameMenuView.timerValue = (int)timeLeft;
    }

    private void OnTimerFinishedHandler()
    {
        FinishRound();
    }

    private void FinishRound()
    {
        if (!_gameSettingsController.isInfiniteRoundDuration)
        {
            if (_roundTimer.isRunning)
                _roundTimer.Reset();

            if (_roundTimer.isPaused)
                _roundTimer.isPaused = false;
        }

        _inGameMenuView.SetStartButtonStateWithoutNotify();
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
