using TMPro;
using UnityEngine;

public class WordView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public string text
    {
        get => _text.text;
        set => _text.text = value;
    }
}
