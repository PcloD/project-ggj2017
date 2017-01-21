using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/**隨機scale管理器*/
public class RadiusCenter 
{
	private static RadiusCenter _Instance = null;
	public static RadiusCenter Instance
	{
		get 
		{
			if (_Instance == null) 
			{
				_Instance = new RadiusCenter ();
			}

			return _Instance;
		}
	}

	/**隨機難度層級*/
	private enum LEVEL
	{
		ONE,
		TWO,
		THREE,
		FOUR,
		FIVE,

		MAX,
	}
	/**level對應演算法*/
	private Dictionary<LEVEL, RandomRadius> LevelInfo = null;
	/**當前難度層級*/
	private RandomRadius CurrentLevel = null;

	/**singleton建構子*/
	private RadiusCenter()
	{
		LevelInfo = new Dictionary<LEVEL, RandomRadius> ();

		LevelInfo.Add (LEVEL.ONE, new Level1 ());

		SetLevel(0);
	}

	/**設定難度層級*/
	public void SetLevel( int Level )
	{
		int _Level = Math.Max (0, Level);
		_Level = Math.Min ( (int)LEVEL.MAX-1, _Level );

		foreach( KeyValuePair<LEVEL, RandomRadius> KVPair in LevelInfo )
		{
			if ((int)KVPair.Key != _Level) 
			{
				continue;
			}

			CurrentLevel = KVPair.Value;
			return;
		}

		CurrentLevel = LevelInfo[LEVEL.ONE];
	}

	/**取得三個圈圈的scale (紅, 綠, 白)*/
	public Vector3 GetScales()
	{
		Vector3 result = new Vector3 ( GetRedScale(), GetGreenScale(), GetWhiteScale() );
		return result;
	}

	/**取得紅圈圈的scale*/
	private float GetRedScale()
	{
		return CurrentLevel.GetRedScale();
	}

	/**取得綠圈圈的scale*/
	private float GetGreenScale()
	{
		return CurrentLevel.GetGreenScale();
	}

	/**取得白圈圈的scale
	*  因為白的要比綠的小 所以要先拿綠的*/
	private float GetWhiteScale()
	{
		return CurrentLevel.GetWhiteScale();
	}

}
