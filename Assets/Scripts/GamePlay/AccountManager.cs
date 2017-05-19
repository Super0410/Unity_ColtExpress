using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
	[SerializeField] AccountReactionManager reactionManager;
	[SerializeField] GameObject panel_Account;
	[SerializeField] Text text_RoundInfo;
	[SerializeField] Text text_PlayerAction;
	[SerializeField] GameObject panel_Reaction;
	[SerializeField] Text text_Reaction;

	Queue<PlayerIndexCardHolder> thisRoundPlayerIndexCardInfoQueue;

	CardHolder thisAccountCardHolder;

	void Start ()
	{
		panel_Account.SetActive (false);
	}

	public void StartAccount ()
	{
		if (!panel_Account.activeSelf)
			panel_Account.SetActive (true);

		thisRoundPlayerIndexCardInfoQueue = GameManager.Instance.gamePlayManager.playCardManager.holeGameCardQueue;

		nextCard ();
	}

	public void FinishOneCard ()
	{
		panel_Reaction.SetActive (false);
		Destroy (thisAccountCardHolder.gameObject);
		nextCard ();
	}

	void nextCard ()
	{
		PlayerIndexCardHolder thisPlayerIndexCardInfo = thisRoundPlayerIndexCardInfoQueue.Dequeue ();

		thisAccountCardHolder = thisPlayerIndexCardInfo.cardHolder;
		CardInfo cardInfo = thisAccountCardHolder.Card;
		int playerIndex = thisPlayerIndexCardInfo.playerIndex;

		PlayerManager thisPlayerManager = GameManager.Instance.gamePlayManager.playerInGameManager.GetAlivePlayerManagerByIndex (playerIndex);
		text_PlayerAction.text = "玩家" + playerIndex + ":" + thisPlayerManager.m_Player.playerName + " " + cardInfo.accountDescription;

		cardToPlayer (cardInfo, thisPlayerManager);
	}

	void cardToPlayer (CardInfo card, PlayerManager player)
	{
		TrainConnection thisPlayerTrainConnection = player.m_trainPosition;

		switch (card.cardType) {
		case CardType.Up:

			if (thisPlayerTrainConnection.nearbyTrain_Up != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Up);
				StartCoroutine ("delaySubmitPanel", "移动完成");
			} else {
				StartCoroutine ("delaySubmitPanel", "移动失败");

			}


			break;
		case CardType.Down:

			if (thisPlayerTrainConnection.nearbyTrain_Down != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Down);
				StartCoroutine ("delaySubmitPanel", "移动完成");
			} else {
				StartCoroutine ("delaySubmitPanel", "移动失败");
			}


			break;
		case CardType.Left:

			if (thisPlayerTrainConnection.nearbyTrain_Left != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Left);
				StartCoroutine ("delaySubmitPanel", "移动完成");
			} else {
				StartCoroutine ("delaySubmitPanel", "移动失败");
			}

			break;
		case CardType.Right:

			if (thisPlayerTrainConnection.nearbyTrain_Right != null) {
				player.Move (thisPlayerTrainConnection.nearbyTrain_Right);
				StartCoroutine ("delaySubmitPanel", "移动完成");
			} else {
				StartCoroutine ("delaySubmitPanel", "移动失败");
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

	IEnumerator delaySubmitPanel (string infoText)
	{
		yield return new WaitForSeconds (0.3f);
		
		panel_Reaction.SetActive (true);
		text_Reaction.text = infoText;
	}

}
