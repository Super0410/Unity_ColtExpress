using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_GameSetting : MonoBehaviour
{
	[SerializeField] Slider slider_PlayerCounter;
	[SerializeField] Text text_PlayerCount;

	int targetPlayerCount;

	void Start ()
	{
		OnPlayerCountChange ();
	}

	public void OnPlayerCountChange ()
	{
		targetPlayerCount = (int)slider_PlayerCounter.value;
		text_PlayerCount.text = targetPlayerCount.ToString ();
	}

	public void OnSubmitPlayerCount ()
	{
		print (GameManager.Instance);
		GameManager.Instance.SetPlayerCount (targetPlayerCount);
		GameManager.Instance.SetProgressType (GameManager.ProgressType.InitPlayer);
	}
}
