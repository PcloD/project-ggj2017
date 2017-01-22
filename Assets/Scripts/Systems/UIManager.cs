using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding.Serialization.JsonFx;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get { return _instance; }
    }
    private static UIManager _instance;

    [SerializeField]
    private BasicPattern _basicPattern;

    [Header("UI")]
    [SerializeField]
    private GameObject _startText;
    [SerializeField]
    private DynamicTextGroup _lastDynamicScoreGroup;
    [SerializeField]
    private DynamicTextGroup _highestDynamicScoreGroup;
    [SerializeField]
	private Button _settingButton;
	[SerializeField]
	private Button _rankingButton;
    [SerializeField]
    private Button _introButton;

    [Header("Panels")]
    [SerializeField]
    private GameObject _mainGamePanel;
    [SerializeField]
	private GameObject _settingPanel;
	[SerializeField]
	private GameObject _rankingPanel;
	[SerializeField]
	private GameObject _namePanel;

    [SerializeField]
    private GameOverEffect _gameOverEffect;
    private void Awake()
    {
        _instance = this;
		_settingButton.onClick.AddListener(OnSettingClicked);
		_rankingButton.onClick.AddListener(OnRankingClicked);

        _settingButton.gameObject.AddComponent<RectTransformScaleShowHide>();
        _rankingButton.gameObject.AddComponent<RectTransformScaleShowHide>();
        _startText.gameObject.AddComponent<RectTransformScaleShowHide>();
        _introButton.gameObject.AddComponent<RectTransformScaleShowHide>();
        _gameOverEffect.onEffectCompleteCallback = OnGameOverEffectComplete;
    }

    private void Start()
    {
        _lastDynamicScoreGroup.SetText("0");
        _highestDynamicScoreGroup.SetText(AchieveManager.Instance.GetHightestScore().ToString());
    }

    private void Update()
    {
        if (_settingPanel.activeInHierarchy)
        {
            if (Input.anyKeyDown)
            {   
                foreach(KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKeyDown(keycode))
                    {
                        GameManager.Instance.TriggerKey = keycode;
                        OnSettingClosed();
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(GameManager.Instance.TriggerKey) && _startText.activeSelf)
			{
				OnRankingClosed ();
                StartCoroutine(StartGame());
            }
        }
    }

    private void OnSettingClicked()
    {
        _settingPanel.SetActive(true);
        _mainGamePanel.SetActive(false);
    }

    private void OnSettingClosed()
    {
        _settingPanel.SetActive(false);
        _mainGamePanel.SetActive(true);
    }

	private void OnRankingClicked()
	{
		_rankingPanel.SetActive (true);
		_rankingPanel.SendMessage ("ClearNameAndScore");
		HttpRequestManager.Instance.Download ();
	}

	public void UpdateRankingPanel( string RankingStr )
	{
		Debug.logger.Log (RankingStr);
		Dictionary<string,object>[] RankInfo = JsonReader.Deserialize<Dictionary<string,object>[]> (RankingStr);
		int LowestScore = 0;
		if (RankInfo.Length >= 3) 
		{
			LowestScore = int.MaxValue;
			foreach (Dictionary<string,object> Info in RankInfo) {
				int Score = (int)Info ["score"];
				LowestScore = Math.Min (LowestScore, Score);
			}
		}
		AchieveManager.Instance.LowestRankScore = LowestScore;

		if (!_rankingPanel.activeInHierarchy) 
		{
			return;
		}
		_rankingPanel.SendMessage ( "SetNameAndScore", RankInfo );
	}

	public void OnRankingClosed()
	{
		_rankingPanel.SetActive (false);
	}

	public void ShowNickNamePanel()
	{
		_namePanel.SetActive (true);
	}

	public void HideNickNamePanel()
	{
		_namePanel.SetActive (false);
	}

    private IEnumerator StartGame()
    {
        _gameOverEffect.ResetGameOverEffect();
        GameManager.Instance.GameReset();

        _startText.gameObject.GetComponent<AbsRectTransformShowHideAction>().Hide();
        _settingButton.gameObject.GetComponent<AbsRectTransformShowHideAction>().Hide();
        _rankingButton.gameObject.GetComponent<AbsRectTransformShowHideAction>().Hide();
        _introButton.gameObject.GetComponent<AbsRectTransformShowHideAction>().Hide();

        yield return new WaitForEndOfFrame();

        AudioManager.Instance.OnStartButtonClicked();
        _basicPattern.OnStartGame();
    }

    public void OnLossGame()
    {
        StartCoroutine(LossGame());
    }

    private IEnumerator LossGame()
    {
        _gameOverEffect.StartGameOverEffect();
        yield return new WaitForSeconds(1.0f);

        _startText.gameObject.GetComponent<AbsRectTransformShowHideAction>().Show();
        _settingButton.gameObject.GetComponent<AbsRectTransformShowHideAction>().Show();
        _rankingButton.gameObject.GetComponent<AbsRectTransformShowHideAction>().Show();
        _introButton.gameObject.GetComponent<AbsRectTransformShowHideAction>().Show();

        _highestDynamicScoreGroup.SetText(AchieveManager.Instance.GetHightestScore().ToString());
    }

    public void OnScoreChanged(int score)
    {
        _lastDynamicScoreGroup.SetText(score.ToString());
    }

    private void OnGameOverEffectComplete()
    {
        _gameOverEffect.ResetGameOverEffect();
    }
}
