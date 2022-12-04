using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class WordsToGuessLoader
{ 
    public static string[] LoadWords(string pathToFile)
    {
        if(File.Exists(pathToFile))
        {
            using StreamReader reader = new StreamReader(pathToFile);
            var text = reader.ReadToEnd();
            var words = Regex.Matches(text, "\"([^\"]*)\"").OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray(); 
            return words;
        }

        return null;
    }
}
