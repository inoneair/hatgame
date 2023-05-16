using System;

public class SinglePlayerMainMenuLogic : IMenuLogic
{
    private SinglePlayerMainMenuView _view;
    private GameSettingsController _gameSettingsController;
    private WordsLibraryController _wordsLibraryController;

    private Action _onStartGame;
    private Action _onReturn;

    public SinglePlayerMainMenuLogic(SinglePlayerMainMenuView view, GameSettingsController gameSettingsController, WordsLibraryController wordsLibraryController)
    {
        _view = view;
        _gameSettingsController = gameSettingsController;
        _wordsLibraryController = wordsLibraryController;

        if (_gameSettingsController.roundDuration < 1)
            _gameSettingsController.roundDuration = 60;

        _view.SetRoundDurationWithoutNotify(_gameSettingsController.roundDuration);

        _view.SubscribeOnStartButtonCLick(OnStartButtonClickHandler);
        _view.SubscribeOnReturnButtonClick(OnReturnButtonClickHandler);
        _view.SubscribeOnIsInfiniteRoundDurationChanged(OnIsInfiniteRoundDurationChangedHandler);
        _view.SubscribeOnRoundDurationChanged(OnRoundDurationChangedHandler);
        _view.SubscribeOnChooseWordsGroup(OnChooseWordsGroupHandler);
        _view.SetWordsGroups(_wordsLibraryController.GetGroups());

        _view.isStartButtonInteractable = false;

        Hide();
    }

    public void Show()
    {
        _view.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _view.gameObject.SetActive(false);
    }

    public void SubscribeOnStartGame(Action handler)
    {
        _onStartGame += handler;
    }

    public void SubscribeOnReturn(Action handler)
    {
        _onReturn += handler;
    }

    private void OnStartButtonClickHandler()
    {
        _view.gameObject.SetActive(false);

        _onStartGame?.Invoke();
    }

    private void OnReturnButtonClickHandler()
    {
        _view.gameObject.SetActive(false);

        _onReturn?.Invoke();
    }

    private void OnIsInfiniteRoundDurationChangedHandler(bool value)
    {
        _view.isRoundDurationFieldInteractable = !value;
        _gameSettingsController.isInfiniteRoundDuration = value;
    }

    private void OnRoundDurationChangedHandler(int roundDuration)
    {
        if (roundDuration > 0)
            _gameSettingsController.roundDuration = roundDuration;
        else
            _view.SetRoundDurationWithoutNotify(_gameSettingsController.roundDuration);
    }

    private void OnChooseWordsGroupHandler(string group)
    {
        _view.isStartButtonInteractable = group != null;
        _gameSettingsController.wordsGroup = group;
    }
}
