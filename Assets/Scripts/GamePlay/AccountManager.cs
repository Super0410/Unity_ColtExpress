using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
	[SerializeField] Panel_Acount reactionManager;
	[SerializeField] GameObject panel_Account;

	Queue<PlayerIndexCardHolderMap> thisRoundPlayerIndexCardInfoQueue;

	[SerializeField] CardInfo thisAccountCardInfo;
	CardHolder thisAccountCardHolder;
	PlayerManager thisAccountPlayerManager;
	TrainManager thisAccountPlayerStandTrainManager;
	List<ItemHolder> thisAccountItemHolderList;
	List<PlayerManager> thisAccountCanAttackPlayerManagerList;

	PoliceManager policeManager;

	void Start ()
	{
		panel_Account.SetActive (false);
	}

	public void StartThisRoundAccount ()
	{
		if (!panel_Account.activeSelf)
			panel_Account.SetActive (true);

		thisRoundPlayerIndexCardInfoQueue = GameManager.Instance.gamePlayManager.playCardManager.holeGameCardQueue;
		print ("StartAccount");
		nextCard ();
	}

	public void FinishThisRoundAccount ()
	{
		panel_Account.SetActive (false);
	}

	public void FinishOneCard ()
	{
		thisAccountPlayerManager.SetPlay (false);
		Destroy (thisAccountCardHolder.gameObject);

		print (thisRoundPlayerIndexCardInfoQueue.Count);
		if (thisRoundPlayerIndexCardInfoQueue.Count > 0) {
			nextCard ();
		} else {
			GameManager.Instance.gamePlayManager.OneRoundFinish ();
		}
	}

	public void OnPickUpItem (ItemHolder pickedItemHolder)
	{
		SetMarkableMark (thisAccountItemHolderList.ToArray (), false);

		thisAccountPlayerManager.PlayerItemController.StoreItem (pickedItemHolder);
		thisAccountPlayerStandTrainManager.PickUpItem (pickedItemHolder);

		reactionManager.SetActionSuccess ();
	}

	public void OnPlayerBeHit (PlayerManager beHitPlayerManager)
	{
		SetMarkableMark (thisAccountCanAttackPlayerManagerList.ToArray (), false);

		if (thisAccountCardInfo.cardType == CardType.Punch) {
			
			ItemHolder droppedItemHolder = beHitPlayerManager.PlayerItemController.GetLastStoreItem ();
			thisAccountPlayerStandTrainManager.StoreItem (droppedItemHolder);

		} else if (thisAccountCardInfo.cardType == CardType.Shot) {

			thisAccountPlayerManager.PlayerBulletController.RemoveOneBullet ();

			beHitPlayerManager.PlayerHealthController.TakeDamage ();
			beHitPlayerManager.PlayerCardController.AddOneUselessBulletCard ();

		} else {
			print ("Wrong in PlayerHit");
		}
		reactionManager.SetActionSuccess ();
	}

	public void OnPoliceMoveFinish ()
	{
		reactionManager.SetActionSuccess ();
	}

	void nextCard ()
	{
		thisAccountCardHolder = null;
		thisAccountPlayerManager = null;
		thisAccountPlayerStandTrainManager = null;
		thisAccountItemHolderList = new List<ItemHolder> ();
		thisAccountCanAttackPlayerManagerList = new List<PlayerManager> ();

		reactionManager.OnBegin ();
		PlayerIndexCardHolderMap thisPlayerIndexCardInfo = thisRoundPlayerIndexCardInfoQueue.Dequeue ();

		thisAccountCardHolder = thisPlayerIndexCardInfo.cardHolder;
		thisAccountCardInfo = thisAccountCardHolder.Card;
		int playerIndex = thisPlayerIndexCardInfo.playerIndex;
		thisAccountPlayerManager = GameManager.Instance.gamePlayManager.playerInGameManager.GetPlayerManagerByIndex (playerIndex);

		GameManager.Instance.gamePlayManager.cameraController.ForcusOnPos (thisAccountPlayerManager.PlayerRenderer.transform.position);
		cardToPlayer (thisAccountCardInfo, thisAccountPlayerManager);
		reactionManager.UpdatePlayerAction (playerIndex, thisAccountPlayerManager.ThisPlayerInfo.playerName, thisAccountCardInfo.accountDescription);
	}

	void cardToPlayer (CardInfo card, PlayerManager thisPlayer)
	{
		TrainConnection thisPlayerStandTrainConnection = thisPlayer.PlayerMoveController.PlayerTrainConnection;
		thisAccountPlayerStandTrainManager = thisPlayerStandTrainConnection.trainManager;

		if (thisPlayer.IsDie) {
			reactionManager.SetPlayerDie ();
			return;
		}
		thisAccountPlayerManager.SetPlay (true);

		switch (card.cardType) {
		case CardType.Up:

			if (thisPlayerStandTrainConnection.nearbyTrain_Up != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerStandTrainConnection.nearbyTrain_Up);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Up);
			}
				
			break;
		case CardType.Down:

			if (thisPlayerStandTrainConnection.nearbyTrain_Down != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerStandTrainConnection.nearbyTrain_Down);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Down);
			}

			break;
		case CardType.Left:

			if (thisPlayerStandTrainConnection.nearbyTrain_Left != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerStandTrainConnection.nearbyTrain_Left);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Left);
			}

			break;
		case CardType.Right:

			if (thisPlayerStandTrainConnection.nearbyTrain_Right != null) {
				thisPlayer.PlayerMoveController.Move (thisPlayerStandTrainConnection.nearbyTrain_Right);
				reactionManager.SetActionSuccess ();
			} else {
				reactionManager.SetActionFail (CardType.Right);
			}

			break;
		case CardType.Pick:

			thisAccountItemHolderList = thisAccountPlayerStandTrainManager.GetAllItemHolder;
			if (thisAccountItemHolderList.Count > 0) {
				SetMarkableMark (thisAccountItemHolderList.ToArray (), true);
			} else
				reactionManager.SetActionFail (CardType.Pick);

			break;
		case CardType.Punch:

			thisAccountCanAttackPlayerManagerList = thisAccountPlayerStandTrainManager.GetAllPlayerManager;
			if (thisAccountCanAttackPlayerManagerList.Count > 1) {
				SetMarkableMark (thisAccountCanAttackPlayerManagerList.ToArray (), thisPlayer, true);
			} else
				reactionManager.SetActionFail (CardType.Punch);

			break;
		case CardType.Shot:

			if (thisAccountPlayerManager.PlayerBulletController.BulletCount <= 0) {
				reactionManager.SetNoBullet ();
				return;
			}

			if (thisAccountPlayerStandTrainManager.IsRoof) {
				List<TrainManager> roofTrainManagerList = GameManager.Instance.gamePlayManager.trainCommander.RoofTrainManagerList;

				for (int i = 0; i < roofTrainManagerList.Count; i++) {
					List<PlayerManager> thisRoofPlayerManagerList = roofTrainManagerList [i].GetAllPlayerManager;
					thisAccountCanAttackPlayerManagerList.AddRange (thisRoofPlayerManagerList);
				}
			} else {
				if (thisPlayerStandTrainConnection.nearbyTrain_Left != null) {
					TrainManager leftTrainManager = thisPlayerStandTrainConnection.nearbyTrain_Left.trainManager;
					if (leftTrainManager != null && leftTrainManager.GetAllPlayerManager.Count != 0) {
						thisAccountCanAttackPlayerManagerList.AddRange (leftTrainManager.GetAllPlayerManager);
					}
				}
				if (thisPlayerStandTrainConnection.nearbyTrain_Right != null) {
					TrainManager rightTrainManager = thisPlayerStandTrainConnection.nearbyTrain_Right.trainManager;
					if (rightTrainManager != null && rightTrainManager.GetAllPlayerManager.Count != 0) {
						thisAccountCanAttackPlayerManagerList.AddRange (rightTrainManager.GetAllPlayerManager);
					}
				}
			}
			if (thisAccountCanAttackPlayerManagerList.Count > 0) {
				SetMarkableMark (thisAccountCanAttackPlayerManagerList.ToArray (), thisPlayer, true);
			} else {
				reactionManager.SetActionFail (CardType.Shot);
			}

			break;
		case CardType.Police:

			if (policeManager == null)
				policeManager = FindObjectOfType<PoliceManager> ();

			GameManager.Instance.gamePlayManager.cameraController.ForcusOnPos (policeManager.PolicePosition);
			policeManager.ShowPath ();

			break;
		}
	}

	void SetMarkableMark (IMarkable[] targetMarkArr, bool isMarked)
	{
		for (int i = 0; i < targetMarkArr.Length; i++) {
			targetMarkArr [i].SetMark (isMarked);
		}
	}

	void SetMarkableMark (IMarkable[]  targetMarkArr, IMarkable exceptMark, bool isMarked)
	{
		for (int i = 0; i < targetMarkArr.Length; i++) {
			if (targetMarkArr [i] == exceptMark)
				continue;
			targetMarkArr [i].SetMark (isMarked);
		}
	}
}