using System.IO;
using UnityEngine;

public class GameSettingsController
{
    private const string path = "Settings.json";
    private const string debugDataDirectory = "DebugData";

    private GameSettingsData _gameSettingsData;

    public string wordsFile
    {
        get => _gameSettingsData.wordsFile;
        set
        {
            if (_gameSettingsData.wordsFile == value)
                return;

            _gameSettingsData.wordsFile = value;
            SaveGameSettings();
        }
    }

    public float roundDuration
    {
        get => _gameSettingsData.roundDuration;
        set
        {
            if (_gameSettingsData.roundDuration == value)
                return;

            _gameSettingsData.roundDuration = value;
            SaveGameSettings();
        }
    }

    public GameSettingsController() => LoadGameSettings();

    private void LoadGameSettings()
    {
        string gameSettingsDirectory;
#if UNITY_EDITOR
        gameSettingsDirectory = Path.Combine(Directory.GetCurrentDirectory(), debugDataDirectory);
        if (!Directory.Exists(gameSettingsDirectory))
            Directory.CreateDirectory(gameSettingsDirectory);
#else
        gameSettingsDirectory = Directory.GetCurrentDirectory();
#endif

        var gameSettingsPath = Path.Combine(gameSettingsDirectory, path);
        if(File.Exists(gameSettingsPath))
        {
            using StreamReader reader = new StreamReader(gameSettingsPath);
            var settingsAsJson = reader.ReadToEnd();
            _gameSettingsData = JsonUtility.FromJson<GameSettingsData>(settingsAsJson);
            _gameSettingsData ??= new GameSettingsData();
        }
        else
        {
            _gameSettingsData = new GameSettingsData();
        }
    }

    private void SaveGameSettings()
    {
        var settingsAsJson = JsonUtility.ToJson(_gameSettingsData, true);

        string gameSettingsDirectory;
#if UNITY_EDITOR
        gameSettingsDirectory = Path.Combine(Directory.GetCurrentDirectory(), debugDataDirectory);
        if(!Directory.Exists(gameSettingsDirectory))
            Directory.CreateDirectory(gameSettingsDirectory);
#else
        gameSettingsDirectory = Directory.GetCurrentDirectory();
#endif

        var gameSettingsPath = Path.Combine(gameSettingsDirectory, path);
        using StreamWriter writer = new StreamWriter(gameSettingsPath);
        writer.Write(settingsAsJson);
        writer.Close();
    }
}
