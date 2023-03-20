using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordsList : MonoBehaviour
{
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] TMP_Text _text;
    [SerializeField] TMP_Text _label;

    public string label
    {
        get => _label.text;
        set => _label.text = value;
    }

    public void SetWords(IEnumerable<string> words)
    {
        var stringBuilder = new StringBuilder();
        foreach (var word in words)
        {
            stringBuilder.Append(word);
            stringBuilder.Append('\n');
        }
        _text.text = stringBuilder.ToString();
        _scrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _text.preferredHeight);
    }
}
