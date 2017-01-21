using System;
using UnityEngine;
using UnityEngine.UI;

public class NowDataTime : MonoBehaviour
{
	private DateTime _dateTime;
    private Text _text;
    private float _timer;
    private float _updateTime = 1;

    private void Awake()
    {
        _text = GetComponent<Text>();

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

        _text.text = string.Format("{0:d2}:{1:d2}:{2:d2}", _dateTime.Hour, _dateTime.Minute, _dateTime.Second);
    }
}
