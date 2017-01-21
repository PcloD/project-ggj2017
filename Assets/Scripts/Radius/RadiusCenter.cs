using System.Collections;
using System.Collections.Generic;

public class RadiusCenter 
{
	private static RadiusCenter Instance = null;

	private enum LEVEL
	{
		ONE,
		TWO,
		THREE,
	}

	private Dictionary<LEVEL, RandomRadius> LevelInfo = null;
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

		SetLevelByScore (0);
	}

	public void SetLevelByScore(int score)
	{
		CurrentLevel = LevelInfo [LEVEL.ONE];
	}

	public float GetRedScale()
	{
		return CurrentLevel.GetRedScale();
	}

	public float GetGreenScale()
	{
		return 1.0f;
	}
}
