using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class ProcessMarker : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _animatedTransform;

    private bool _isSwitchOn = false;
    private TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> _fadeTweener;
    private TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> _loadingImageTweener;

    public bool isSwitchOn => _isSwitchOn;

    public void SwitchOn()
    {
        if (_isSwitchOn)
            return;

        _isSwitchOn = true;
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 1.0f;

        StartAnimation();
    }

    public async Task SwitchOnWithAnimation()
    {
        if (_isSwitchOn)
            return;

        _isSwitchOn = true;
        _canvasGroup.gameObject.SetActive(true);
        _fadeTweener = _canvasGroup.DOFade(1, 1f);
        await _fadeTweener.AsyncWaitForCompletion();

        StartAnimation();
    }

    public void SwitchOff()
    {
        _isSwitchOn = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.gameObject.SetActive(false);

        StopAnimation();
    }

    public async Task SwitchOffWithAnimation()
    {
        if (!_isSwitchOn)
            return;

        _isSwitchOn = false;
        _fadeTweener = _canvasGroup.DOFade(0, 1f);
        await _fadeTweener.AsyncWaitForCompletion();
        _canvasGroup.gameObject.SetActive(false);

        StopAnimation();
    }

    private void StartAnimation()
    {
        _loadingImageTweener ??= _animatedTransform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.WorldAxisAdd).SetLoops(-1);
    }

    private void StopAnimation()
    {
        if (_loadingImageTweener != null)
        {
            _loadingImageTweener.Complete();
            _loadingImageTweener = null;
        }
    }
}
