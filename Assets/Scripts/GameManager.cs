using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get { return _instance; }
    }
    private static GameManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    private int _score;

    public int CurStage { get { return _curStage; } }
    private int _curStage;

    [SerializeField] private int[] _stageScore;
    [SerializeField] private AnimationCurve[] _stagePatterns;

    public void AddScore()
    {
        _score++;

        if (_score >= _stageScore[_curStage])
        {
            _curStage++;

            if (_curStage > _stageScore.Length - 1)
            {
                _curStage = _stageScore.Length - 1;
            }
        }

        UIManager.Instance.OnScoreChanged(_score);
    }

    public AnimationCurve GetCurrentPattern()
    {
        return _stagePatterns[_curStage];
    }

    public void ResetGame()
    {
        _curStage = 0;
        _score = 0;

        UIManager.Instance.OnScoreChanged(_score);
    }
}
