using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
	[Header ("Managers")]
	public TrainCommander trainCommander;
	public PlayerInGameManager playerInGameManager;
	public PlayCardManager playCardManager;
	public AccountManager accountManager;

	[SerializeField] SpriteRenderer sceneBg;

	[Header ("Infos")]
	[SerializeField] CharacterInfo[] basicCharacterArr;
	[SerializeField] Card[] basicCardArr;
	[SerializeField] CardInfo uselessBulletCardInfo;
	[SerializeField] SceneInfo[] basicSceneInfoArr;
	[Header ("0:Package 1:Diamond 2:LargePackage 3:PlayerInit")]
	[SerializeField] ItemInfo[] basicItemInfoArr;

	#region Getter

	public CardInfo UselessBulletCardInfo{ get { return uselessBulletCardInfo; } }

	public CharacterInfo[] BasicCharacterInfoArr{ get { return basicCharacterArr; } }

	public SceneInfo[] BasicSceneInfoArr{ get { return basicSceneInfoArr; } }

	public ItemInfo[] BasicItemInfoArr{ get { return basicItemInfoArr; } }

	#endregion

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
		if (!gameObject.activeSelf)
			gameObject.SetActive (true);

		//Train
		trainCommander.InitAllManager ();

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
		playCardManager.NextGame ();

		maxRound = gameSceneArr [curNumOfGame - 1].cardSideArr.Length;
		curRound = 0;
		nextRound ();
	}

	void nextRound ()
	{
		if (curRound >= maxRound) {

			playCardManager.PlayCardFinish ();
			accountManager.StartAccount ();
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
