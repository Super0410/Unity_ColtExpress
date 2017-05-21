using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
	[SerializeField] List<CardInfo> storeCardList = new List<CardInfo> ();

	int playerIndex;
	PlayerInfo playerInfo;
	PlayerManager playerManager;
	PlayerInfoHolder playerUIManager;
	PlayCardManager playCardManager;

	public void Init (int targetPlayerIndex, PlayerInfo targetPlayerInfo, PlayerManager targetPlayerManager, PlayerInfoHolder targetPlayerUIManager, List<CardInfo> targetBasicCardInfo, PlayCardManager targetPlayCardManager)
	{
		playerIndex = targetPlayerIndex;
		playerInfo = targetPlayerInfo;
		playerManager = targetPlayerManager;
		playerUIManager = targetPlayerUIManager;
		storeCardList = targetBasicCardInfo;
		playCardManager = targetPlayCardManager;
	}

	public void RemoveOneBulletCard ()
	{
		for (int i = 0; i < storeCardList.Count; i++) {
			if (storeCardList [i].cardType == CardType.Bullet) {
				storeCardList.Remove (storeCardList [i]);
				break;
			}
		}
		playerUIManager.UpdateBulletCount (BulletCardCount);
	}

	public void AddOneUselessBulletCard ()
	{
		CardInfo uselessBulletCardInfo = GameManager.Instance.gamePlayManager.UselessBulletCardInfo;
		storeCardList.Add (uselessBulletCardInfo);
	}

	public int BulletCardCount {
		get {
			int bulletCount = 0;
			for (int i = 0; i < storeCardList.Count; i++) {
				if (storeCardList [i].cardType == CardType.Bullet) {
					bulletCount++;
				}
			}
			return bulletCount;
		}
	}

	public void PlayCard ()
	{
		playCardManager.NextPlayerPlay (playerIndex, playerInfo, playerManager, storeCardList.ToArray ());
	}
}
