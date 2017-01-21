using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get { return _instance; }
    }
    private static UIManager _instance;

    [SerializeField]
    private BasicPattern _basicPattern;
    [SerializeField]
    private GameObject _startText;
    [SerializeField]
    private Text _lastScoreText;
    [SerializeField]
    private Text _highestScoreText;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _startText.activeSelf)
        {
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        GameManager.Instance.ResetGame();
        _startText.SetActive(false);

        yield return new WaitForEndOfFrame();

        _basicPattern.OnStartGame();
    }

    public void OnLossGame()
    {
        _startText.SetActive(true);
    }

    public void OnScoreChanged(int score)
    {
        _lastScoreText.text = score.ToString();
    }
}
