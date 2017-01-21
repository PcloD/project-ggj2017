using System;
using UnityEngine;
using UnityEngine.UI;

public class NowDataTime : MonoBehaviour
{
    [SerializeField] private DaynamicText[] _dynamicTexts;

	private DateTime _dateTime;
    private float _timer;
    private float _updateTime = 1;
    private string _currentTime;

    private void Awake()
    {
        UpdateDateTime();
    }
	
    private void Update () 
	{
        _timer += Time.deltaTime;

        if (_timer >= _updateTime)
        {
            UpdateDateTime();
        }
	}

    private void UpdateDateTime()
    {
        _timer = 0;

        _dateTime = DateTime.Now;

        _currentTime = string.Format("{0:d2}{1:d2}{2:d2}", _dateTime.Hour, _dateTime.Minute, _dateTime.Second);

        for (int cnt = 0; cnt < _dynamicTexts.Length; cnt++)
        {
            _dynamicTexts[cnt].SetText(_currentTime[cnt].ToString());
        }
    }
}
