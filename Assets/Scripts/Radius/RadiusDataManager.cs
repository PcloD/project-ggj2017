using System;
using UnityEngine;

public class RadiusDataManager : MonoBehaviour
{
    public static RadiusDataManager Instance
    {
        get { return _instance; }
    }
    private static RadiusDataManager _instance;

    [SerializeField] private RadiusData[] _radiusData;

    private float _curGreenRadius;
    private System.Random _random = new System.Random();
    private RadiusData _currentRadiusData;

    private void Awake()
    {
        _instance = this;
        SetLevel(0);
    }

    public void SetLevel(int level)
    {
        level = Mathf.Min(level, _radiusData.Length - 1);
        _currentRadiusData = _radiusData[level];
    }

    public Vector3 GetScales()
    {
        float r = GetRedScale(_currentRadiusData);
        float g = GetGreenScale(_currentRadiusData);
        float w = GetWhiteScale(_currentRadiusData);

        return new Vector3(r, g, w);
    }

    private float GetGreenScale(RadiusData radiusData)
    {
        _curGreenRadius = RandomFloat(radiusData.m_greenMaxScale - radiusData.m_greenMinScale) + radiusData.m_greenMinScale;
        return _curGreenRadius;
    }

    private float GetRedScale(RadiusData radiusData)
    {
        return RandomFloat(radiusData.m_redMaxScale - radiusData.m_redMinScale) + radiusData.m_redMinScale;
    }

    private float GetWhiteScale(RadiusData radiusData)
    {
        if (_curGreenRadius <= 0.0f) 
        {
            return 0.25f;
        }

        float LessScale =  RandomFloat(radiusData.m_lessGreenScale) + 0.25f;
        float result = _curGreenRadius - LessScale;
        _curGreenRadius = 0.0f;

        return result;
    }

    private float RandomFloat(float Range)
    {
        double Seed = _random.NextDouble () * Range;

        return Convert.ToSingle(Seed);
    }
}
