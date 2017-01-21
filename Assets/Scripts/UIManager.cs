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

    private int _score = 0;

    private void Awake()
    {
        _instance = this;
        _startButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnStartGameButtonClicked()
    {
        _score = 0;
        _basicPattern.OnStartGame();
        _startButton.gameObject.SetActive(false);
    }

    public void OnLossGame()
    {
        _startButton.gameObject.SetActive(true);
    }

    public void OnGetScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
}
