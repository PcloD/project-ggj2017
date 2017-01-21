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

    [Header("Panels")]
    [SerializeField]
    private GameObject _mainGamePanel;
    [SerializeField]
    private GameObject _settingPanel;

    private void Awake()
    {
        _instance = this;
        _settingButton.onClick.AddListener(OnSettingClicked);
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

    private IEnumerator StartGame()
    {
        GameManager.Instance.GameReset();
        _startText.SetActive(false);
        _settingButton.gameObject.SetActive(false);

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
        _highestScoreText.text = AchieveManager.Instance.GetHightestScore().ToString();
    }

    public void OnScoreChanged(int score)
    {
        _lastScoreText.text = score.ToString();
    }
}
