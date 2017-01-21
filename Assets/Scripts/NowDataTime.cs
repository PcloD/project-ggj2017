using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NowDataTime : MonoBehaviour {

	DateTime Now;
	void Start () 
	{

	}
	
	void Update () 
	{
		Now = DateTime.Now;

		Debug.Log (Now.Hour+":"+Now.Minute+":"+Now.Second);
		
	}
}
