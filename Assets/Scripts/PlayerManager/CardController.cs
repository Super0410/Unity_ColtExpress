using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
	[SerializeField] List<CardInfo> storeCardList = new List<CardInfo> ();

	int playerIndex;
	PlayerInfo playerInfo;
	PlayCardManager playCardManager;

	public void Init (int targetPlayerIndex, PlayerInfo targetPlayerInfo, List<CardInfo> targetBasicCardInfo, PlayCardManager targetPlayCardManager)
	{
		playerIndex = targetPlayerIndex;
		playerInfo = targetPlayerInfo;
		storeCardList = targetBasicCardInfo;
		playCardManager = targetPlayCardManager;
	}

	public void PlayCard ()
	{
		playCardManager.NextPlayerPlay (playerIndex, playerInfo, storeCardList.ToArray ());
	}
}
