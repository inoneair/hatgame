using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SFB;

public class ChooseWordsFileView : MonoBehaviour
{
    [SerializeField] private Button _chooseFileButton;
    [SerializeField] private TMP_Text _fileNameText;

    private string _filePath;

    private event Action<string> _onChooseFile;

    public string filePath
    {
        get => _filePath;
        set
        {
            if (_filePath == value)
                return;

            SetFilePathWihoutNotify(value);
            _onChooseFile?.Invoke(_filePath);
        }
    }

    private void Awake()
    {
        _chooseFileButton.onClick.AddListener(OnClickChooseFileButtonHandler);
    }

    private void OnDestroy()
    {
        _chooseFileButton.onClick.RemoveListener(OnClickChooseFileButtonHandler);
    }

    public void SetFilePathWihoutNotify(string filePath)
    {
        _filePath = filePath;
        _fileNameText.text = Path.GetFileName(_filePath);
    }

    public void SubscribeOnFileChosen(Action<string> handler)
    {
        _onChooseFile += handler;
    }

    private void OnClickChooseFileButtonHandler()
    {
        string title = string.Empty;
        string directory = string.Empty;
        if (!string.IsNullOrWhiteSpace(_filePath))
        {
            title = Path.GetFileName(_filePath);
            directory = Path.GetDirectoryName(_filePath);
        }

        var files = StandaloneFileBrowser.OpenFilePanel(title, directory, string.Empty, false);
        if (files.Length > 0)        
            filePath = files[0];
    }
}
