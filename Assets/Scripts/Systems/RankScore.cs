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

	}
}
