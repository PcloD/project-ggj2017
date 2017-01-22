using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour 
{
	
	[SerializeField]
	private GameObject _AudioOn;
	[SerializeField]
	private GameObject _AudioOff;

	private static readonly string AUDIO_ON = "AUDIO_ON";
	public void Start()
	{
		if (PlayerPrefs.HasKey (AUDIO_ON)) 
		{
			PlayerPrefs.SetInt (AUDIO_ON, 1);
		}

		SetImage (IsAudioOn());
	}

	private bool IsAudioOn()
	{
		return (PlayerPrefs.GetInt (AUDIO_ON) == 1);
	}

	private void SetAudioOn( bool IsOn )
	{
		PlayerPrefs.SetInt (AUDIO_ON, IsOn?1:0);
	}

	private void SetImage( bool IsOn )
	{
		_AudioOn.SetActive (IsOn);
		_AudioOff.SetActive (!IsOn);	
	}

	public void OnAudioClicked()
	{
		bool audioOn = IsAudioOn ();
		SetImage (audioOn);
		SetAudioOn (!audioOn);
	}
}
