using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class WordsLibraryController
{
    private const string _pathToFile = "WordsLibrary.json";
    private Dictionary<string, WordsGroupData> _wordsGroups = new Dictionary<string, WordsGroupData>();

    public async Task Init()
    {
        await LoadWordsLibrary(_pathToFile);
    }

    public string[] GetGroups() => _wordsGroups.Keys.ToArray();
    
    public async Task<string[]> LoadWords(string group)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return await LoadWordsFromWeb(group);
#else
        return await LoadWordsDebugData(group);
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private async Task<string[]> LoadWordsFromWeb(string group)
    {
        string[] words = null;
        var groupData = _wordsGroups[group];
        bool isGetFileData = false;
        WebFileLoader.Instance.GetFileData(groupData.file, (text) =>
        {
            words = JsonUtility.FromJson<WordsToGuessData>(text).words;
            isGetFileData = true;
        });

        while (!isGetFileData)
            await Task.Yield();

        return words;
    }
#endif

#if UNITY_EDITOR
    private async Task<string[]> LoadWordsDebugData(string group)
    {
        var webGLTemplatePath = UnityEditor.PlayerSettings.WebGL.template.Replace("PROJECT:", "");
        var localPath = $"Assets/WebGLTemplates/{webGLTemplatePath}/{_wordsGroups[group].file}";
        var fullPath = Path.GetFullPath(localPath);

        string[] words = null;
        if (File.Exists(fullPath))
        {
            using StreamReader reader = new StreamReader(fullPath);
            var text = await reader.ReadToEndAsync();
            words = JsonUtility.FromJson<WordsToGuessData>(text).words;
        }

        return words;
    }
#endif

    private async Task LoadWordsLibrary(string pathToFile)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        await LoadWordsLibraryFromWebAsync(_pathToFile);
#else
        await LoadWordsLibraryDebugData(_pathToFile);        
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private async Task LoadWordsLibraryFromWebAsync(string pathToFile)
    {
        bool isGetFileData = false;
        WebFileLoader.Instance.GetFileData(pathToFile, (text) =>
        {
            var groups = JsonUtility.FromJson<WordsLibraryData>(text).groups;
            foreach(var group in groups)            
                _wordsGroups.Add(group.name, group);
            
            isGetFileData = true;
        });

        while (!isGetFileData)
            await Task.Yield();
    }
#endif

#if UNITY_EDITOR
    private async Task LoadWordsLibraryDebugData(string pathToFile)
    {
        var webGLTemplatePath = UnityEditor.PlayerSettings.WebGL.template.Replace("PROJECT:", "");
        var localPath = $"Assets/WebGLTemplates/{webGLTemplatePath}/WordsLibrary.json";
        var fullPath = Path.GetFullPath(localPath);

        if (File.Exists(fullPath))
        {
            using StreamReader reader = new StreamReader(fullPath);
            var text = await reader.ReadToEndAsync();
            var groups = JsonUtility.FromJson<WordsLibraryData>(text).groups;
            foreach (var group in groups)
                _wordsGroups.Add(group.name, group);
        }
    }
#endif
}
