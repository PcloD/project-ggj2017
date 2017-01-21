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
        set
        {
            _score = value;
            UIManager.Instance.OnScoreChanged(_score);
        }
    }
    private int _score;

    public int CurStage
    {
        get { return _curStage; }
        set
        {
            _curStage = value;
            RadiusDataManager.Instance.SetLevel(_curStage);
        }
    }
    private int _curStage;

    public KeyCode TriggerKey
    {
        get { return _triggerKey; }
        set { _triggerKey = value; }
    }
    private KeyCode _triggerKey = KeyCode.Space;

    [SerializeField] private int[] _stageScore;
    [SerializeField] private AnimationCurve[] _stagePatterns;

    public void AddScore()
    {
        Score++;

        if (Score >= _stageScore[CurStage])
        {
            if(CurStage != _stageScore.Length - 1)
            {
                CurStage++;
            }
        }

        UIManager.Instance.OnScoreChanged(Score);
    }

    public AnimationCurve GetCurrentPattern()
    {
        return _stagePatterns[CurStage];
    }

    public void GameReset()
    {
        CurStage = 0;
        Score = 0;
    }
}
