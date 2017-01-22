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
        if (!PlayerPrefs.HasKey(AUDIO_ON))
        {
            PlayerPrefs.SetInt(AUDIO_ON, 1);
            PlayerPrefs.Save();
        }

        UpdateImage();
	}

	private bool IsAudioOn()
	{
		return (PlayerPrefs.GetInt (AUDIO_ON) == 1);
	}

    private void UpdateImage()
	{
        _AudioOn.SetActive (IsAudioOn());
        _AudioOff.SetActive (!IsAudioOn());

        AudioManager.Instance.UpdateVolumn();
	}

	public void OnAudioClicked()
	{
        PlayerPrefs.SetInt (AUDIO_ON, PlayerPrefs.GetInt(AUDIO_ON) == 1 ? 0 : 1);
        PlayerPrefs.Save();

        UpdateImage();
	}
}
