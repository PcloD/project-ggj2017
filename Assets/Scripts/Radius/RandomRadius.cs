using System.Collections;
using System.Collections.Generic;
using System;

public abstract class RandomRadius 
{
	protected float RedMinScale;
	protected float RedMaxScale;
	protected float GreenMinScale;
	protected float GreenMaxScale;

	private Random Rand = new Random();

	public RandomRadius()
	{
		RedMinScale = 2.0f;
		RedMaxScale = 2.0f;
		GreenMinScale = 1.0f;
		GreenMaxScale = 1.0f;

	}

	/**取得下次紅圓圈的scale*/
	public float GetRedScale()
	{
		return RandomFloat( RedMaxScale - RedMinScale ) + RedMinScale;
	}

	/**取得下次綠圓圈的scale*/
	public float GetGreenScale()
	{
		return 1.0f;
	}

	private float RandomFloat(float Range)
	{
		double Seed = Rand.NextDouble ()*Range;

		return Convert.ToSingle(Seed);
	}
}
