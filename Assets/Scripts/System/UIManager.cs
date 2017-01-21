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
    private Button _startButton;
    [SerializeField]
    private Text _scoreText;

    private void Awake()
    {
        _instance = this;
        _startButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnStartGameButtonClicked()
    {
        GameManager.Instance.ResetGame();
        _startButton.gameObject.SetActive(false);
        _basicPattern.OnStartGame();
    }

    public void OnLossGame()
    {
        _startButton.gameObject.SetActive(true);
    }

    public void OnScoreChanged(int score)
    {
        _scoreText.text = score.ToString();
    }
}
