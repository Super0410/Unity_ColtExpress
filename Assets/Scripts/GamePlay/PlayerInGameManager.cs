using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInGameManager : MonoBehaviour
{
	[SerializeField] Transform layout_PlayerInfoParent;
	[SerializeField] PlayerInfoHolder playerInfoHolderPrefab;
	[SerializeField] PlayerManager playerPrefab;
	[SerializeField] ItemHolder itemHolderPrefab;

	Dictionary<int, PlayerManager> allPlayerDict = new Dictionary<int, PlayerManager> ();
	Dictionary<int, PlayerManager> alivePlayerDict = new Dictionary<int, PlayerManager> ();
	int curPlayerIndex;

	public void Init (PlayerInfo[] allPlayerArr, List<CardInfo> allStoreCardList, PlayCardManager playCardManager)
	{
		// PlayerInfoHolder
		GUIHelper.Instance.DestroyChildImmediatly<PlayerInfoHolder> (layout_PlayerInfoParent);

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

			//new train position
			TrainConnection newStartTrainConnection = GameManager.Instance.gamePlayManager.trainCommander.GetRandomPlayerStartTrainConnection ();
			//new item
			ItemInfo newItemInfo = GameManager.Instance.gamePlayManager.BasicItemInfoArr [3];
			ItemHolder newItemHolder = Instantiate (itemHolderPrefab);
			newItemHolder.SetItemInfo (newItemInfo);

			PlayerManager newPlayerManager = newPlayerGObj.GetComponent<PlayerManager> ();
			newPlayerManager.Init (i, allPlayerArr [i], newPlayerInfoHolder, allStoreCardList, playCardManager
				, newStartTrainConnection, newItemHolder);

			allPlayerDict.Add (i, newPlayerManager);
			alivePlayerDict.Add (i, newPlayerManager);

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