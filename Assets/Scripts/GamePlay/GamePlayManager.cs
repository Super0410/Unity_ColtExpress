using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
	public PlayerInGameManager playerInGameManager;
	public PlayCardManager playCardManager;
	[SerializeField] SpriteRenderer sceneBg;
	[SerializeField] Card[] basicCardArr;

	SceneInfo[] gameSceneArr;
	int maxNumOfGame;
	int curNumOfGame;
	int maxRound;
	int curRound;

	void Awake ()
	{
		GameManager.Instance.OnProgressChange += onProgressChange;
	}

	public void Init (PlayerInfo[] allPlayerArr, SceneInfo[] gameSceneArr)
	{
		// Card
		List<CardInfo> allStoreCardList = new List<CardInfo> ();
		for (int i = 0; i < basicCardArr.Length; i++) {
			for (int j = 0; j < basicCardArr [i].count; j++) {
				allStoreCardList.Add (basicCardArr [i].cardInfo);
			}
		}

		// Player
		playerInGameManager.Init (allPlayerArr, allStoreCardList, playCardManager);

		// Game
		this.gameSceneArr = gameSceneArr;
		maxNumOfGame = gameSceneArr.Length;
		curNumOfGame = 0;
		nextGame ();
	}

	public void OnePlayerFinishPlay (int thisPlayerIndex)
	{
		if (thisPlayerIndex < GameManager.Instance.PlayerCount - 1) {
			playerInGameManager.NextPlayerPlay ();
		} else {
			nextRound ();
		}
	}

	void nextGame ()
	{
		if (curNumOfGame >= maxNumOfGame)
			return;

		curNumOfGame++;
		sceneBg.sprite = Resources.Load<Sprite> (gameSceneArr [curNumOfGame - 1].bgUrl);

		maxRound = gameSceneArr [curNumOfGame - 1].cardSideArr.Length;
		curRound = 0;
		nextRound ();
	}

	void nextRound ()
	{
		if (curRound >= maxRound) {
			print ("AASDASF");
			return;
		}
		
		curRound++;
		playCardManager.SetNewRound (curRound, maxRound, gameSceneArr [curNumOfGame - 1].cardSideArr [curRound - 1]);
		playerInGameManager.NextRoundPlay ();
	}

	void onProgressChange (GameManager.ProgressType targetProgress)
	{
		if (targetProgress == GameManager.ProgressType.GameBegin) {
			gameObject.SetActive (true);
		} else {
			gameObject.SetActive (false);
		}
	}

	[System.Serializable]
	public struct Card
	{
		public CardInfo cardInfo;
		public int count;
	}
}
