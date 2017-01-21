using System.Collections;
using System.Collections.Generic;
using System;

/**隨機scale管理器*/
public class RadiusCenter 
{
	private static RadiusCenter Instance = null;

	/**隨機難度層級*/
	private enum LEVEL
	{
		ONE,
		TWO,
		THREE,

		MAX,
	}
	/**level對應演算法*/
	private Dictionary<LEVEL, RandomRadius> LevelInfo = null;
	/**當前難度層級*/
	private RandomRadius CurrentLevel = null;

	public static RadiusCenter GetInstance()
	{
		if (Instance == null) 
		{
			Instance = new RadiusCenter ();
		}

		return Instance;
	}

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

		CurrentLevel = LevelInfo [LEVEL.ONE];
	}

	/**取得紅圈圈的scale*/
	public float GetRedScale()
	{
		return CurrentLevel.GetRedScale();
	}

	/**取得綠圈圈的scale*/
	public float GetGreenScale()
	{
		return CurrentLevel.GetGreenScale();
	}
}
