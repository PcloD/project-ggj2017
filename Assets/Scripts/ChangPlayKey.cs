using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangPlayKey : MonoBehaviour {


	public string _NowKey = "a";
	public bool _ResetKey = false;
	public KeyCode _toKeyCode;


	void Start () 
	{
		
	}
	
	void Update () 
	{

		if (Input.anyKeyDown && _ResetKey)
		{	
			foreach(KeyCode _k in Enum.GetValues(typeof(KeyCode)))
			{
				if(Input.GetKeyDown(_k))
				{
					_toKeyCode = _k;
					_ResetKey = false;
				}
			}
		}

		if(Input.GetKeyDown(_toKeyCode) && _ResetKey == false)
		{
			Debug.Log ("[NowKey]" + _NowKey);
		}
	}
}
