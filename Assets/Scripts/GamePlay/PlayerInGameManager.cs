﻿using System.Collections;
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
	int curPlayerIndex;

	public Dictionary<int, PlayerManager> AllPlayerDict { get { return allPlayerDict; } }

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
			//new card list;
			List<CardInfo> newCardInfoList = new List<CardInfo> ();
			newCardInfoList.AddRange (allStoreCardList);

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
			newPlayerManager.Init (i, allPlayerArr [i], newPlayerInfoHolder, newCardInfoList, playCardManager
				, newStartTrainConnection, newItemHolder);

			allPlayerDict.Add (i, newPlayerManager);

		}

	}

	public PlayerManager GetPlayerManagerByIndex (int targetPlayerIndex)
	{
		if (allPlayerDict.ContainsKey (targetPlayerIndex)) {
			return allPlayerDict [targetPlayerIndex];
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
		if (allPlayerDict [curPlayerIndex].IsDie) {
			GameManager.Instance.gamePlayManager.OnePlayerFinishPlay (curPlayerIndex);
		} else
			allPlayerDict [curPlayerIndex].PlayerCardController.PlayCard ();
	}

	public void CheckAlivePlayer ()
	{
		int alivePlayerCount = allPlayerDict.Count;
		foreach (PlayerManager playerManager in allPlayerDict.Values) {
			if (playerManager.IsDie)
				alivePlayerCount--;
		}
		if (alivePlayerCount <= 1) {
			GameManager.Instance.gamePlayManager.OnGameOver ();
		}
	}
}