using System;
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

    [Header("UI")]
    [SerializeField]
    private GameObject _startText;
    [SerializeField]
    private Text _lastScoreText;
    [SerializeField]
    private Text _highestScoreText;
    [SerializeField]
	private Button _settingButton;
	[SerializeField]
	private Button _rankingButton;

    [Header("Panels")]
    [SerializeField]
    private GameObject _mainGamePanel;
    [SerializeField]
	private GameObject _settingPanel;
	[SerializeField]
	private GameObject _rankingPanel;

    private void Awake()
    {
        _instance = this;
		_settingButton.onClick.AddListener(OnSettingClicked);
		_rankingButton.onClick.AddListener(OnRankingClicked);
    }

    private void Start()
    {
        _highestScoreText.text = AchieveManager.Instance.GetHightestScore().ToString();
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
	}

	private void OnRankingClosed()
	{
		_rankingPanel.SetActive (false);
	}

    private IEnumerator StartGame()
    {
        GameManager.Instance.GameReset();
        _startText.SetActive(false);
        _settingButton.gameObject.SetActive(false);
		_rankingButton.gameObject.SetActive (false);

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
        yield return new WaitForSeconds(1.0f);

        _startText.SetActive(true);
		_settingButton.gameObject.SetActive(true);
		_rankingButton.gameObject.SetActive (true);
        _highestScoreText.text = AchieveManager.Instance.GetHightestScore().ToString();
    }

    public void OnScoreChanged(int score)
    {
        _lastScoreText.text = score.ToString();
    }
}
