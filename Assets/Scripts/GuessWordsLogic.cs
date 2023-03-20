using System.Collections;
using System.Collections.Generic;

public class GuessWordsLogic
{
    private System.Random _random;

    private LinkedList<string> _wordsToGuess;
    private LinkedListNode<string> _prevWordToGuess;
    private string _currentGuessingWord = string.Empty;

    public int wordsToGuessCount => _wordsToGuess.Count;

    public string currentGuessingWord => _currentGuessingWord;

    public GuessWordsLogic()
    {
        _random = new System.Random();
    }

    public void StartToGuess(string[] words)
    {
        _prevWordToGuess = null;
        _wordsToGuess = new LinkedList<string>(words);
    }

    public string GetNextWord()
    {
        string nextWord = null;
        if (_wordsToGuess.Count == 0)
        {
            _currentGuessingWord = string.Empty;
            return nextWord;
        }
        else if (_wordsToGuess.Count == 1)
        {
            nextWord = _wordsToGuess.First.Value;
            _wordsToGuess.RemoveFirst();
            _currentGuessingWord = nextWord;
            return nextWord;
        }

        _prevWordToGuess ??= _wordsToGuess.First;
        bool isMoveForward = true;
        var steps = _random.Next(_wordsToGuess.Count - 1);

        var currentWordToGuess = _prevWordToGuess;
        for (int i = 0; i < steps; ++i)
        {
            if (isMoveForward)
            {
                if (currentWordToGuess.Next == null)
                {
                    isMoveForward = false;
                    currentWordToGuess = currentWordToGuess.Previous;
                }
                else
                {
                    currentWordToGuess = currentWordToGuess.Next;
                }
            }
            else
            {
                if (currentWordToGuess.Previous == null)
                {
                    isMoveForward = true;
                    currentWordToGuess = currentWordToGuess.Next;
                }
                else
                {
                    currentWordToGuess = currentWordToGuess.Previous;
                }
            }
        }

        _prevWordToGuess = currentWordToGuess.Next == null ? currentWordToGuess.Previous : currentWordToGuess.Next;
        nextWord = currentWordToGuess.Value;
        _wordsToGuess.Remove(currentWordToGuess);
        _currentGuessingWord = nextWord;
        return nextWord;
    }
}
