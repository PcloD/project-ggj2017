using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankScore : MonoBehaviour {

	[SerializeField]
	private Text[] _serverScoreList;
	[SerializeField]
	private Text _myScore;

	public void ClearNameAndScore()
	{
		foreach (Text txt in _serverScoreList) 
		{
			txt.text = "";
		}
		_myScore.text = "";
	}

	public void SetNameAndScore( object serverData )
	{
		Dictionary<string,object>[] RankInfo = serverData as Dictionary<string,object>[];
		if (RankInfo == null) 
		{
			return;
		}

		int size = Mathf.Min(_serverScoreList.Length, RankInfo.Length);
		for (int index = 0; index < size; index++) 
		{
			_serverScoreList [index].text = RankInfo [index] ["score"].ToString ();
		}

		_myScore.text = AchieveManager.Instance.GetHightestScore().ToString();
	}
}
