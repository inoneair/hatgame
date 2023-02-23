using System.IO;
using UnityEngine;

public class GameSettingsController
{
    private int _roundDuration;

    public int roundDuration
    {
        get => _roundDuration;
        set => _roundDuration = value;        
    }

    public GameSettingsController()
    {
        _roundDuration = 60;
    }
}
