using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour 
{
	[SerializeField]
	private Text _inputText;
	[SerializeField]
	private Button _inputBtn;

	private void Awake()
	{
		_inputBtn.onClick.AddListener(OnClick);
	}

	public void OnClick()
	{
		string name = _inputText.text;
		AchieveManager.Instance.SetNickName (name);
		UIManager.Instance.HideNickNamePanel ();
	}
}
