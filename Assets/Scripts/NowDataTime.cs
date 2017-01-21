using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowDataTime : MonoBehaviour {

	private DateTime Now;
	[SerializeField]private Text _DataTimeText;
	void Start () 
	{

	}
	
	void Update () 
	{
		Now = DateTime.Now;
		_DataTimeText.text = Now.Hour + ":" + Now.Minute + ":" + Now.Second;
		//Debug.Log (Now.Hour+":"+Now.Minute+":"+Now.Second);
	}
}
