using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseRoundView : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;

    private bool _isPauseActive;

    private Action<bool> _onIsPauseActiveChanged;

    public bool isPauseActive
    {
        get => _isPauseActive;
        set
        {
            if (_isPauseActive != value)
            {
                SetIsPauseActiveWithoutNotify(value);
                _onIsPauseActiveChanged?.Invoke(_isPauseActive);
            }
        }
    }

    private void Awake()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClickHandler);
        _resumeButton.onClick.AddListener(OnResumeButtonClickHandler);        
    }

    private void OnDestroy()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClickHandler);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClickHandler);
    }

    public void SetIsPauseActiveWithoutNotify(bool value)
    {
        _isPauseActive = value;

        _pauseButton.gameObject.SetActive(!value);
        _resumeButton.gameObject.SetActive(value);
    }

    public void SubscribeIsPauseActiveChanged(Action<bool> handler)
    {
        _onIsPauseActiveChanged += handler;
    }

    private void OnPauseButtonClickHandler() =>    
        isPauseActive = true;    

    private void OnResumeButtonClickHandler() =>    
        isPauseActive = false;
    
}
