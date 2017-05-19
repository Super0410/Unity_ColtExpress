using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardManager : MonoBehaviour
{
	[SerializeField] GameObject Panel_PlayCard;
	[SerializeField] Text text_RoundInfo;
	[SerializeField] GameObject Mask_PlayerIdentity;
	[SerializeField] Text text_PlayerInfo;
	[SerializeField] Transform layout_DealCardHolder;
	[SerializeField] Transform layout_PlayedCardHolder;
	[SerializeField] CardHolder cardPrefab;
	[SerializeField] int cardCountPerRound = 6;

	bool thisRoundIsUp;
	int thisPlayerIndex;
	string thisPlayerInfoText;
	CardInfo[] thisPlayerGenerateCardArr;
	List<CardHolder> thisPlayerRoundCardList;

	public Queue<PlayerIndexCardHolder> holeGameCardQueue { get; private set; }

	public void NextGame ()
	{
		holeGameCardQueue = new Queue<PlayerIndexCardHolder> ();
	}

	public void PlayCardFinish ()
	{
		Panel_PlayCard.SetActive (false);
	}

	public void SetNewRound (int curRound, int maxRound, bool isUp)
	{
		thisRoundIsUp = isUp;
		text_RoundInfo.text = "出牌" + "   第" + curRound + "轮/共" + maxRound + "轮" + "   牌面向" + (isUp ? "上" : "下");
	}

	public void PlayerPlay (int playerIndex, PlayerInfo playerInfo, CardInfo[] playerStoreCardArr)
	{
		if (!Panel_PlayCard.activeSelf)
			Panel_PlayCard.SetActive (true);
		
		Mask_PlayerIdentity.SetActive (true);

		thisPlayerIndex = playerIndex;

		thisPlayerInfoText = (playerIndex + 1).ToString () + "：" + playerInfo.playerName;
		text_PlayerInfo.text = "玩家" + thisPlayerInfoText;
		
		thisPlayerGenerateCardArr = playerStoreCardArr;
		shuffleCard ();
	}

	void shuffleCard ()
	{
		thisPlayerGenerateCardArr = Utillity.ShuffleArray (thisPlayerGenerateCardArr);

		dealCard ();
	}

	void dealCard ()
	{
		if (layout_DealCardHolder.childCount > 0) {
			Image[] childImageArr = layout_DealCardHolder.GetComponentsInChildren<Image> ();
			for (int i = 0; i < childImageArr.Length; i++) {
				if (childImageArr [i] != null)
					DestroyImmediate (childImageArr [i].gameObject);
			}
		}
		thisPlayerRoundCardList = new List<CardHolder> ();
		for (int i = 0; i < cardCountPerRound; i++) {
			GameObject newCardGObj = Instantiate (cardPrefab.gameObject);
			newCardGObj.transform.SetParent (layout_DealCardHolder, false);
			CardHolder newCardHolder = newCardGObj.GetComponent<CardHolder> ();
			newCardHolder.SetCard (thisPlayerGenerateCardArr [i], i);
			thisPlayerRoundCardList.Add (newCardHolder);

			if (newCardHolder.Card.cardType == CardType.Bullet || newCardHolder.Card.cardType == CardType.UselessBullet)
				continue;

			newCardGObj.GetComponent<Button> ().onClick.AddListener (delegate {
				onCardClick (newCardHolder);
			});

			newCardHolder.Btn_PlayCard.onClick.AddListener (delegate {
				onCardPlay (newCardHolder);
			});

		}
	}

	void onCardClick (CardHolder targetCardHolder)
	{
		for (int i = 0; i < thisPlayerRoundCardList.Count; i++) {
			if (thisPlayerRoundCardList [i].CardID == targetCardHolder.CardID) {
				thisPlayerRoundCardList [i].SetSelected (true);
			} else {
				thisPlayerRoundCardList [i].SetSelected (false);
			}
		}
	}

	void onCardPlay (CardHolder targetCardHolder)
	{
		for (int i = 0; i < thisPlayerRoundCardList.Count; i++) {
			if (thisPlayerRoundCardList [i].CardID == targetCardHolder.CardID) {
				thisPlayerRoundCardList [i].transform.SetParent (layout_PlayedCardHolder, false);
				thisPlayerRoundCardList [i].transform.localScale = Vector3.one * 0.8f;
				thisPlayerRoundCardList [i].GetComponent<Button> ().onClick.RemoveAllListeners ();
				thisPlayerRoundCardList [i].Btn_PlayCard.onClick.RemoveAllListeners ();
				thisPlayerRoundCardList [i].Btn_PlayCard.GetComponentInChildren<Text> ().text = thisPlayerInfoText;
				if (!thisRoundIsUp) {
					thisPlayerRoundCardList [i].GetComponentInChildren<Text> ().enabled = false;
				}
				break;
			}
		}

		holeGameCardQueue.Enqueue (new PlayerIndexCardHolder (thisPlayerIndex, targetCardHolder));
		GameManager.Instance.gamePlayManager.OnePlayerFinishPlay (thisPlayerIndex);
	}
}
