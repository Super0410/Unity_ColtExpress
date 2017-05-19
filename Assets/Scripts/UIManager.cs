using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	[SerializeField] SwitchUIManager switchUIManager;
	[SerializeField] Animator anim_panel_Bg;
	[SerializeField] GameObject panel_Menu;

	public SwitchUIManager SwitchUI{ get { return switchUIManager; } }

	void Awake ()
	{
		GameManager.Instance.OnProgressChange += onProgressChange;
	}

	void onProgressChange (GameManager.ProgressType targetProgressType)
	{
		switch (targetProgressType) {
		case GameManager.ProgressType.OnStart:
			panel_Menu.SetActive (true);
			switchUIManager.OpenPanel (anim_panel_Bg);
			break;
		case GameManager.ProgressType.InitPlayer:

			break;
		case GameManager.ProgressType.RandomScene:

			break;

		case GameManager.ProgressType.GameBegin:
			switchUIManager.CloseCurrent (anim_panel_Bg);
			break;
		}
		
	}
}
