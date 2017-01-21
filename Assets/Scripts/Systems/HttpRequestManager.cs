using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpRequestManager : MonoBehaviour
{
    public static HttpRequestManager Instance
    {
        get { return _instance; }
    }
    private static HttpRequestManager _instance;

    private const string URL = "https://pi-dot-blisstest-1064.appspot.com/";

    private void Awake()
    {
        _instance = this;
    }

    public void Upload(string name, string score)
    {
        StartCoroutine(UploadScore(name, score));
    }

    private IEnumerator UploadScore(string name, string score)
    {
        WWWForm data = new WWWForm();
        data.AddField("name", name);
        data.AddField("score", score);

        WWW www = new WWW(URL, data);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
			UIManager.Instance.UpdateRankingPanel(www.text);
        }
    }

    public void Download()
    {
        StartCoroutine(DownloadLeaderboard());
    }

    private IEnumerator DownloadLeaderboard()
    {
        WWW www = new WWW(URL);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            UIManager.Instance.UpdateRankingPanel(www.text);
        }
    }
}
