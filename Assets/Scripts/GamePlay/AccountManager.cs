using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
	[SerializeField] Text text_PlayerInfo;
	[SerializeField] Text text_RoundInfo;

	Queue<PlayerIndexCardInfo> thisRoundPlayerIndexCardInfoQueue;

	public void StartAccount (Queue<PlayerIndexCardInfo> targetPlayerIndexCardInfo)
	{
		thisRoundPlayerIndexCardInfoQueue = GameManager.Instance.gamePlayManager.playCardManager.holeGameCardQueue;

		nextCard ();
	}

	void nextCard ()
	{
		PlayerIndexCardInfo thisPlayerIndexCardInfo = thisRoundPlayerIndexCardInfoQueue.Dequeue ();

		CardInfo cardInfo = thisPlayerIndexCardInfo.cardInfo;
		int playerIndex = thisPlayerIndexCardInfo.playerIndex;
		PlayerManager thisPlayerManager = GameManager.Instance.gamePlayManager.playerInGameManager.GetAlivePlayerManagerByIndex (playerIndex);
		cardToPlayer (cardInfo, thisPlayerManager);
	}

	void cardToPlayer (CardInfo card, PlayerManager player)
	{
		TrainConnection thisPlayerTrainConnection = player.m_trainPosition;

		switch (card.cardType) {
		case CardType.Up:

			if (thisPlayerTrainConnection.nearbyTrain_Up != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Up);
			}


			break;
		case CardType.Down:

			if (thisPlayerTrainConnection.nearbyTrain_Down != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Down);
			}


			break;
		case CardType.Left:

			if (thisPlayerTrainConnection.nearbyTrain_Left != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Left);
			}

			break;
		case CardType.Right:

			if (thisPlayerTrainConnection.nearbyTrain_Right != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Right);
			}

			break;
		case CardType.Pick:

			break;
		case CardType.Punch:

			break;
		case CardType.Shot:

			break;
		case CardType.Police:

			break;
		}
	}
}
