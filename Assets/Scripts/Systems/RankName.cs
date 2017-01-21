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
		Dictionary<string,object>[] RankInfo = serverData as Dictionary<string,object>[];
		if (RankInfo == null) 
		{
			return;
		}

		int size = Mathf.Min(_serverNameList.Length, RankInfo.Length);
		for (int index = 0; index < size; index++) 
		{
			_serverNameList [index].text = RankInfo [index] ["name"].ToString ();
		}

		_myName.text = AchieveManager.Instance.GetNickName();
	}
}
