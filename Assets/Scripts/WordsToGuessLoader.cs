using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class WordsToGuessLoader
{
    public static string[] LoadWords(string pathToFile)
    {        
        if (File.Exists(pathToFile))
        {
            using StreamReader reader = new StreamReader(pathToFile);
            var text = reader.ReadToEnd();
            var words = JsonUtility.FromJson<WordsToGuessData>(text).words;
            return words;
        }

        return null;
    }

    public static async Task<string[]> LoadWordsFromWebAsync(string pathToFile)
    {
        string[] words = null;
        bool isGetFileData = false;
        WebFileLoader.Instance.GetFileData(pathToFile, (text) =>
        {
            words = JsonUtility.FromJson<WordsToGuessData>(text).words;
            isGetFileData = true;
        });

        while(!isGetFileData)
            await Task.Yield();        

        return words;
    }
}