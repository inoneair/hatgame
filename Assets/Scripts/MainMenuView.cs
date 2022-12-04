using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] ChooseWordsFileView _chooseWordsFileView;
    [SerializeField] Button _exitButton;

    private Action _onStartButonClick;
    private Action<string> _onWordsFileChosen;
    private Action _onExitButonClick;

    public string wordsFile
    {
        get => _chooseWordsFileView.filePath;
        set => _chooseWordsFileView.filePath = value;
    }

    public bool isStartButtonInteractable
    {
        get => _startButton.interactable;
        set => _startButton.interactable = value;
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClickHandler);
        _chooseWordsFileView.SubscribeOnFileChosen(OnWordsFileChosenHandler);
        _exitButton.onClick.AddListener(OnExitButtonClickHandler);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClickHandler);
        _exitButton.onClick.RemoveListener(OnExitButtonClickHandler);
    }

    public void SetWordsFileWithoutNotify(string wordsFile) =>    
        _chooseWordsFileView.SetFilePathWihoutNotify(wordsFile);    

    public void SubscribeOnStartButtonCLick(Action handler)
    {
        _onStartButonClick += handler;
    }

    public void SubscribeOnWordsFileChosen(Action<string> handler)
    {
        _onWordsFileChosen += handler;
    }

    public void SubscribeOnExitButtonCLick(Action handler)
    {
        _onExitButonClick += handler;
    }

    private void OnStartButtonClickHandler() => _onStartButonClick?.Invoke();

    private void OnWordsFileChosenHandler(string filePath) => _onWordsFileChosen?.Invoke(filePath);

    private void OnExitButtonClickHandler() => _onExitButonClick?.Invoke();

}
