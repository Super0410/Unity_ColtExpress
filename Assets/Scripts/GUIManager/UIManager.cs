using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
	[SerializeField] SwitchUIManager switchUIManager;
	[SerializeField] Animator anim_panel_Bg;
	[SerializeField] Animator anim_panel_Menu;
	[SerializeField] Animator anim_panelSceneRandom;
	[SerializeField] Animator anim_GamePlay;
	[SerializeField] GameObject panel_GamePlay;

	public SwitchUIManager SwitchUI{ get { return switchUIManager; } }

	public void SetMenuUI (GameManager.ProgressType targetProgressType)
	{
		switch (targetProgressType) {
		case GameManager.ProgressType.OnStart:
			anim_panel_Menu.gameObject.SetActive (true);
			panel_GamePlay.SetActive (false);
			switchUIManager.OpenPanel (anim_panel_Bg);
			switchUIManager.OpenPanel (anim_panel_Menu);
			break;
		case GameManager.ProgressType.InitPlayer:

			break;
		case GameManager.ProgressType.RandomScene:
			switchUIManager.OpenPanel (anim_panelSceneRandom);
			break;

		case GameManager.ProgressType.GameBegin:
			switchUIManager.CloseCurrent (anim_panel_Bg);
			panel_GamePlay.SetActive (true);
			break;
		}
	}

	public void SetGamePlayUI (GamePlayManager.GamePlayProgressType targetProgress)
	{
		print (targetProgress);
		switch (targetProgress) {
		case GamePlayManager.GamePlayProgressType.PlayCard:
			anim_GamePlay.CrossFade ("PlayCard", 0.5f);
			break;

		case GamePlayManager.GamePlayProgressType.Account:
			anim_GamePlay.CrossFade ("Account", 0.5f);
			break;

		case GamePlayManager.GamePlayProgressType.GameOver:
			anim_GamePlay.CrossFade ("GameOver", 0.5f);
			break;

		}
	}

	public void OnRestart ()
	{
		SceneManager.LoadScene ("Main");
	}
}
