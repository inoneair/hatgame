using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private ProcessMarker _marker;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _loadingText;
    [SerializeField] private string _loadingCompleteText;

    private void Awake()
    {
        SwitchOff();
    }

    public void SwitchOn()
    {
        _text.text = _loadingText;
        _marker.SwitchOn();
    }

    public async Task SwitchOnWithAnimation()
    {
        _text.text = _loadingText;
        await _marker.SwitchOnWithAnimation();
    }

    public void SwitchOff()
    {
        _text.text = _loadingCompleteText;
        _marker.SwitchOff();
    }

    public async Task SwitchOffWithAnimation()
    {
        _text.text = _loadingCompleteText;
        await _marker.SwitchOffWithAnimation();
    }
}
