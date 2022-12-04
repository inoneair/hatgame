using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public string text
    {
        get => _text.text;
        set => _text.text = value;
    }
}
