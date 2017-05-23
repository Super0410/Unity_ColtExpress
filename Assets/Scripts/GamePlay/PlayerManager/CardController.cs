using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
	[SerializeField] List<CardInfo> storeCardList = new List<CardInfo> ();

	int playerIndex;
	PlayerInfo playerInfo;
	PlayerManager playerManager;
	PlayCardManager playCardManager;

	public void Init (int targetPlayerIndex, PlayerInfo targetPlayerInfo, PlayerManager targetPlayerManager, List<CardInfo> targetBasicCardInfo, PlayCardManager targetPlayCardManager)
	{
		playerIndex = targetPlayerIndex;
		playerInfo = targetPlayerInfo;
		playerManager = targetPlayerManager;
		storeCardList = targetBasicCardInfo;
		playCardManager = targetPlayCardManager;
	}

	public void AddOneUselessBulletCard ()
	{
		CardInfo uselessBulletCardInfo = GameManager.Instance.gamePlayManager.UselessBulletCardInfo;
		storeCardList.Add (uselessBulletCardInfo);
	}

	public void PlayCard ()
	{
		playCardManager.NextPlayerPlay (playerIndex, playerInfo, playerManager, storeCardList.ToArray ());
	}
}
