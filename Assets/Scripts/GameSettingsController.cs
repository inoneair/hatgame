using System.IO;
using UnityEngine;

public class GameSettingsController
{
    private int _roundDuration = 60;
    private string _wordsGroup = string.Empty;

    public int roundDuration
    {
        get => _roundDuration;
        set => _roundDuration = value;        
    }

    public string wordsGroup
    {
        get => _wordsGroup;
        set => _wordsGroup = value;
    }
}
