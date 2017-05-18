using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_GamePlay : MonoBehaviour
{

	void Awake ()
	{
		GameManager.Instance.OnProgressChange += onProgressChange;
	}

	void onProgressChange (GameManager.ProgressType targetType)
	{
		if (targetType == GameManager.ProgressType.GameBegin) {
			gameObject.SetActive (true);
		} else
			gameObject.SetActive (false);
	}
}
