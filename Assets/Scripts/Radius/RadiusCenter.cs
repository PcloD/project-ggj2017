using System.Collections;
using System.Collections.Generic;

public class RadiusCenter 
{
	private static RadiusCenter Instance = null;

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
	}

	public void SetLevelByScore(int score)
	{
		return;
	}

	public float GetRedRadius()
	{
		return 2.0f;
	}

	public float GetGreenRadius()
	{
		return 1.0f;
	}
}
