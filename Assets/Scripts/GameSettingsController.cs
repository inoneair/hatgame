using UnityEngine;

public class GameSettingsController
{
    private bool _isInfiniteRoundDuration = false;
    private int _roundDuration = 60;
    private string _wordsGroup = string.Empty;

    public bool isInfiniteRoundDuration
    {
        get => _isInfiniteRoundDuration;
        set => _isInfiniteRoundDuration = value;
    }

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
