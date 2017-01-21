using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankName : MonoBehaviour {

	[SerializeField]
	private Text[] _serverNameList;
	[SerializeField]
	private Text _myName;

	public void ClearNameAndScore()
	{
		foreach (Text txt in _serverNameList) 
		{
			txt.text = "";
		}
		_myName.text = "";
	}

	public void SetNameAndScore( object serverData )
	{
		
	}
}
