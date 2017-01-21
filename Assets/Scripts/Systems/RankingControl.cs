using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingControl : MonoBehaviour {

	[SerializeField]
	private RankName _rankName;
	[SerializeField]
	private RankScore _rankScore;

	public void ClearNameAndScore()
	{
		_rankName.ClearNameAndScore ();
		_rankScore.ClearNameAndScore ();
	}

	public void SetNameAndScore( object serverData )
	{

	}
}
