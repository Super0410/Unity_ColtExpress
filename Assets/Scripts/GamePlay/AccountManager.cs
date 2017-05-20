using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
	[SerializeField] Panel_Acount reactionManager;
	[SerializeField] GameObject panel_Account;

	Queue<PlayerIndexCardHolderMap> thisRoundPlayerIndexCardInfoQueue;

	CardInfo thisAccountCardInfo;
	CardHolder thisAccountCardHolder;
	PlayerManager thisAccountPlayerManager;
	TrainManager thisAccountPlayerStandTrainManager;
	List<ItemHolder> thisAccountItemHolderList;
	List<PlayerManager> thisAccountCanAttackPlayerManagerList;

	void Start ()
	{
		panel_Account.SetActive (false);
	}

	public void StartAccount ()
	{
		if (!panel_Account.activeSelf)
			panel_Account.SetActive (true);

		thisAccountCardHolder = null;
		thisAccountPlayerManager = null;
		thisAccountPlayerStandTrainManager = null;
		thisAccountItemHolderList = new List<ItemHolder> ();


		thisRoundPlayerIndexCardInfoQueue = GameManager.Instance.gamePlayManager.playCardManager.holeGameCardQueue;

		nextCard ();
	}

	public void FinishOneCard ()
	{
		Destroy (thisAccountCardHolder.gameObject);
		nextCard ();
	}

	public void OnPickUpItem (ItemHolder pickedItemHolder)
	{
		for (int i = 0; i < thisAccountItemHolderList.Count; i++) {
			thisAccountItemHolderList [i].SetCanPick (false);
		}
		thisAccountPlayerManager.PlayerItemController.StoreItem (pickedItemHolder);
		thisAccountPlayerStandTrainManager.PickUpItem (pickedItemHolder);
		reactionManager.SetActionSuccess ();
	}

	public void OnPunchPlayer (PlayerManager punchedPlayerManager)
	{
		for (int i = 0; i < thisAccountCanAttackPlayerManagerList.Count; i++) {
			thisAccountCanAttackPlayerManagerList [i].SetCanBeHit (false);
		}
		ItemHolder droppedItemHolder = punchedPlayerManager.PlayerItemController.GetLastStoreItem ();
		thisAccountPlayerStandTrainManager.StoreItem (droppedItemHolder);
		reactionManager.SetActionSuccess ();
	}

	void nextCard ()
	{
		reactionManager.OnBegin ();
		PlayerIndexCardHolderMap thisPlayerIndexCardInfo = thisRoundPlayerIndexCardInfoQueue.Dequeue ();

		thisAccountCardHolder = thisPlayerIndexCardInfo.cardHolder;
		thisAccountCardInfo = thisAccountCardHolder.Card;
		int playerIndex = thisPlayerIndexCardInfo.playerIndex;
		thisAccountPlayerManager = GameManager.Instance.gamePlayManager.playerInGameManager.GetAlivePlayerManagerByIndex (playerIndex);
		cardToPlayer (thisAccountCardInfo, thisAccountPlayerManager);

		reactionManager.UpdatePlayerAction (playerIndex, thisAccountPlayerManager.Player.playerName, thisAccountCardInfo.accountDescription);
	}

	void cardToPlayer (CardInfo card, PlayerManager thisPlayer)
	{
		TrainConnection thisPlayerTrainConnection = thisPlayer.PlayerMoveController.PlayerTrainConnection;
		thisAccountPlayerStandTrainManager = thisPlayerTrainConnection.trainManager;

		switch (card.cardType) {
		case CardType.Up:

			if (thisPlayerTrainConnection.nearbyTrain_Up != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerTrainConnection.nearbyTrain_Up);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Up);
			}


			break;
		case CardType.Down:

			if (thisPlayerTrainConnection.nearbyTrain_Down != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerTrainConnection.nearbyTrain_Down);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Down);
			}


			break;
		case CardType.Left:

			if (thisPlayerTrainConnection.nearbyTrain_Left != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerTrainConnection.nearbyTrain_Left);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Left);
			}

			break;
		case CardType.Right:

			if (thisPlayerTrainConnection.nearbyTrain_Right != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerTrainConnection.nearbyTrain_Right);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Right);
			}

			break;
		case CardType.Pick:

			thisAccountItemHolderList = thisAccountPlayerStandTrainManager.GetAllItemHolder ();
			if (thisAccountItemHolderList.Count > 0) {
				for (int i = 0; i < thisAccountItemHolderList.Count; i++) {
					thisAccountItemHolderList [i].SetCanPick (true);
				}
			} else
				reactionManager.SetActionFail (CardType.Pick);

			break;
		case CardType.Punch:

			thisAccountCanAttackPlayerManagerList = thisAccountPlayerStandTrainManager.GetAllPlayerManager ();
			if (thisAccountCanAttackPlayerManagerList.Count > 1) {
				for (int i = 0; i < thisAccountCanAttackPlayerManagerList.Count; i++) {
					if (thisAccountCanAttackPlayerManagerList [i] == thisPlayer)
						continue;
					thisAccountCanAttackPlayerManagerList [i].SetCanBeHit (true);
				}
			} else
				reactionManager.SetActionFail (CardType.Punch);

			break;
		case CardType.Shot:


			break;
		case CardType.Police:


			break;
		}
	}
}