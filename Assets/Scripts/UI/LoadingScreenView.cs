using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;

public class LoadingScreenView : MonoBehaviour
{
    public enum State
    {
        Loading,
        Complete
    }

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _loadingImage;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _loadingText;
    [SerializeField] private string _loadingCompleteText;
    private bool _isSwitchOn = false;

    private TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> _fadeTweener;
    private TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> _loadingImageTweener;

    public void SwitchOn()
    {
        if (_isSwitchOn)
            return;

        _isSwitchOn = true;
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 1.0f;
    }

    public void SwitchOff()
    {
        _isSwitchOn = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.gameObject.SetActive(false);
    }

    public async Task SwitchOffWithAnimation()
    {
        if (!_isSwitchOn)
            return;

        _isSwitchOn = false;

        _fadeTweener = _canvasGroup.DOFade(0, 1f);
        await _fadeTweener.AsyncWaitForCompletion();
        _canvasGroup.gameObject.SetActive(false);
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Loading:
                _text.text = _loadingText;
                StartLoadingImageAnimation();
                break;

            case State.Complete:
                _text.text = _loadingCompleteText;
                StopLoadingImageAnimation();
                break;
        }
    }

    private void StartLoadingImageAnimation()
    {
        _loadingImageTweener ??= _loadingImage.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.WorldAxisAdd).SetLoops(-1);        
    }

    private void StopLoadingImageAnimation()
    {
        if (_loadingImageTweener != null)
        {
            _loadingImageTweener.Complete();
            _loadingImageTweener = null;
        }
    }

}
