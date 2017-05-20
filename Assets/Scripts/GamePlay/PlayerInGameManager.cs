using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInGameManager : MonoBehaviour
{
	[SerializeField] Transform layout_PlayerInfoParent;
	[SerializeField] PlayerInfoHolder playerInfoHolderPrefab;
	[SerializeField] PlayerManager playerPrefab;

	Dictionary<int, PlayerManager> allPlayerDict = new Dictionary<int, PlayerManager> ();
	Dictionary<int, PlayerManager> alivePlayerDict = new Dictionary<int, PlayerManager> ();
	int curPlayerIndex;

	public void Init (PlayerInfo[] allPlayerArr, List<CardInfo> allStoreCardList, PlayCardManager playCardManager)
	{		
		// PlayerInfoHolder
		if (layout_PlayerInfoParent.childCount > 0) {
			PlayerInfoHolder[] childImageArr = layout_PlayerInfoParent.GetComponentsInChildren<PlayerInfoHolder> ();
			for (int i = 0; i < childImageArr.Length; i++) {
				if (childImageArr [i] != null)
					DestroyImmediate (childImageArr [i].gameObject);
			}
		}

		// Player
		string playerHolderName = "PlayerHolder";
		if (transform.FindChild (playerHolderName)) {
			DestroyImmediate (transform.FindChild (playerHolderName).gameObject);
		}

		Transform playerHolder = new GameObject (playerHolderName).transform;
		playerHolder.parent = transform;

		for (int i = 0; i < allPlayerArr.Length; i++) {
			//new player
			GameObject newPlayerGObj = Instantiate (playerPrefab.gameObject) as GameObject;
			newPlayerGObj.name = allPlayerArr [i].playerName;
			newPlayerGObj.transform.parent = playerHolder;

			//new playerInfoHolder
			GameObject newPlayerInfoHolderGObj = Instantiate (playerInfoHolderPrefab.gameObject) as GameObject;
			newPlayerInfoHolderGObj.name = allPlayerArr [i].playerName;
			newPlayerInfoHolderGObj.transform.SetParent (layout_PlayerInfoParent, false);
			PlayerInfoHolder newPlayerInfoHolder = newPlayerInfoHolderGObj.GetComponent<PlayerInfoHolder> ();
			newPlayerInfoHolder.Init (allPlayerArr [i]);

			PlayerManager newPlayerManager = newPlayerGObj.GetComponent<PlayerManager> ();
			newPlayerManager.Init (i, allPlayerArr [i], newPlayerInfoHolder, allStoreCardList, playCardManager);

			allPlayerDict.Add (i, newPlayerManager);
			alivePlayerDict.Add (i, newPlayerManager);

			//new train
			TrainConnection newStartTrainConnection = GameManager.Instance.gamePlayManager.trainCommander.GetRandomPlayerStartTrainConnection ();
			newPlayerManager.PlayerMoveController.Move (newStartTrainConnection);
		}

	}

	public PlayerManager GetAlivePlayerManagerByIndex (int targetPlayerIndex)
	{
		if (alivePlayerDict.ContainsKey (targetPlayerIndex)) {
			return alivePlayerDict [targetPlayerIndex];
		} else
			return null;
	}

	public void NextPlayerPlay ()
	{
		curPlayerIndex++;
		playerPlayInTurn ();
	}

	public void NextRoundPlay ()
	{
		curPlayerIndex = 0;
		playerPlayInTurn ();
	}

	void playerPlayInTurn ()
	{
		allPlayerDict [curPlayerIndex].PlayerCardController.PlayCard ();
	}
}