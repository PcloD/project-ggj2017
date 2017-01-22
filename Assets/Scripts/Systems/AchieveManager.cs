using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

// 分數統計
public class AchieveManager 
{
	private static readonly string HIGHTEST_SCORE = "HIGHTEST_SCORE";
//	private static readonly string LAST_SCORE = "LAST_SCORE";
	private static readonly string NICK_NAME = "NICK_NAME";

	private static AchieveManager _Instance = null;
	public static AchieveManager Instance
	{
		get 
		{
			if (_Instance == null) 
			{
				_Instance = new AchieveManager ();
			}

			return _Instance;
		}
	}

	private int HightestScore = 0;
//	private int LastScore = 0;
	private int _LowestRankScore=0;
	public int LowestRankScore
	{ 
		get
		{
			return _LowestRankScore;
		}
		set
		{
			_LowestRankScore = value;
		}
	}


	private AchieveManager()
	{
		if (PlayerPrefs.HasKey (HIGHTEST_SCORE)) 
		{
			HightestScore = PlayerPrefs.GetInt (HIGHTEST_SCORE);
		}

		 if (!PlayerPrefs.HasKey (NICK_NAME)) 
		{
			UIManager.Instance.ShowNickNamePanel ();
		}
	}

	public void SetHightestScore( int Score )
	{
		if (HightestScore >= Score) 
		{
			return;
		}

		HightestScore = Score;
		PlayerPrefs.SetInt (HIGHTEST_SCORE, HightestScore);
        PlayerPrefs.Save();
	}

	public int GetHightestScore()
	{
		return HightestScore;
	}

	public void SetNickName( string name )
	{
		string checkStr = new Regex (@"\w").Replace (name, "");
		if (!string.IsNullOrEmpty (checkStr)) 
		{
			name = "ILoveGGJ";
		}

		name = name.Substring (0, Math.Min(name.Length,8 ));
		PlayerPrefs.SetString (NICK_NAME, name);
	}

	public string GetNickName()
	{
		return PlayerPrefs.GetString (NICK_NAME);
	}

}