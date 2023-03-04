using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class WebFileLoader : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetURLFromPage();

    private static WebFileLoader _instance;

    public void Init()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
        else
        {
            Debug.LogError("Must be only one instance of WebFileLoader");
            Destroy(gameObject);
        }
    }

    public static WebFileLoader Instance => _instance;

    private IEnumerator GetRequest(string uri, Action<string> onGetRequestData)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();

        string[] pages = uri.Split('/');
        int page = pages.Length - 1;

        if (!string.IsNullOrEmpty(webRequest.error))
        {
            Debug.LogError(pages[page] + ": Error: " + webRequest.error);
        }
        else
        {
            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            onGetRequestData?.Invoke(webRequest.downloadHandler.text);
        }
    }

    public void GetFileData(string filePath, Action<string> onGetFileData)
    {
        StartCoroutine(GetRequest($"{GetURLFromPage()}/{filePath}", onGetFileData));
    }
}