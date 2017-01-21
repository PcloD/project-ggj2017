using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 分數統計
public class AchieveManager 
{
	private static readonly string HIGHTEST_SCORE = "HIGHTEST_SCORE";
//	private static readonly string LAST_SCORE = "LAST_SCORE";

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

	private AchieveManager()
	{
		if (PlayerPrefs.HasKey (HIGHTEST_SCORE)) 
		{
			HightestScore = PlayerPrefs.GetInt (HIGHTEST_SCORE);
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
	}

	public int GetHightestScore()
	{
		return HightestScore;
	}
}
