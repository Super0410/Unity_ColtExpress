using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public enum ProgressType
	{
		OnStart,
		InitPlayer,
		RandomScene,
		GameBegin
	}

	public event System.Action<ProgressType> OnProgressChange;

	[SerializeField] ProgressType progressType;

	[Header ("PlayerPrepare")]
	public PlayerInitManager playerIniter;
	public Panel_SceneRandom sceneRandom;

	[Header ("GameBegin")]
	public GamePlayManager gamePlayManager;

	public int PlayerCount { get; private set; }

	PlayerInfo[] allPlayerArr;
	SceneInfo[] shuffledSceneArr;

	void Start ()
	{
		SetProgressType (ProgressType.OnStart);
	}

	public void SetPlayerCount (int targetCount)
	{
		PlayerCount = targetCount;
	}

	public void SetAllPlayer (PlayerInfo[] targetPlayerArr)
	{
		allPlayerArr = targetPlayerArr;
	}

	public void SetScene (SceneInfo[] targetSceneArr)
	{
		shuffledSceneArr = targetSceneArr;
	}

	public void SetProgressType (ProgressType targetProgress)
	{
		progressType = targetProgress;
		if (OnProgressChange != null)
			OnProgressChange (progressType);
		
		switch (progressType) {
		case ProgressType.InitPlayer:
			playerIniter.Init (PlayerCount);
			break;
		case ProgressType.RandomScene:
			sceneRandom.DoRandom ();
			break;
		case ProgressType.GameBegin:
			gamePlayManager.Init (allPlayerArr, shuffledSceneArr);
			break;
		}
	}
}
