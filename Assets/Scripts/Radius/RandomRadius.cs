using System.Collections;
using System.Collections.Generic;
using System;

public abstract class RandomRadius 
{
	protected float RedMinScale;
	protected float RedMaxScale;
	protected float GreenMinScale;
	protected float GreenMaxScale;
	protected float LessGreenScale;

	private float CurGreenScale;

	private Random Rand = new Random();

	public RandomRadius()
	{
		RedMinScale = 2.0f;
		RedMaxScale = 2.0f;
		GreenMinScale = 1.0f;
		GreenMaxScale = 1.0f;

		LessGreenScale = 0.0f;
		CurGreenScale = 0.0f;
	}

	/**取得下次紅圓圈的scale*/
	public float GetRedScale()
	{
		return RandomFloat( RedMaxScale - RedMinScale ) + RedMinScale;
	}

	/**取得下次綠圓圈的scale*/
	public float GetGreenScale()
	{
		CurGreenScale = RandomFloat( GreenMaxScale - GreenMinScale ) + GreenMinScale;
		return CurGreenScale;
	}

	/**取得下次白圓圈的scale*/
	public float GetWhiteScale()
	{
		if (CurGreenScale <= 0.0f) 
		{
			return 0.25f;
		}

		// 算出要比綠圈小多少
		float LessScale =  RandomFloat( LessGreenScale ) + 0.25f;
		float result = CurGreenScale - LessScale;
		CurGreenScale = 0.0f;

		return result;
	}

	private float RandomFloat(float Range)
	{
		double Seed = Rand.NextDouble ()*Range;

		return Convert.ToSingle(Seed);
	}
}
