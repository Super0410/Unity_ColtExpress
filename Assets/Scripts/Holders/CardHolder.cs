using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
	[SerializeField] CardInfo cardInfo;
	[SerializeField] Button btn_PlayCard;
	[SerializeField] Image image_Bg;

	public CardInfo Card { get { return cardInfo; } }

	public int CardID { get; private set; }

	public Button Btn_PlayCard{ get { return btn_PlayCard; } }

	void Start ()
	{
		SetSelected (false);
	}

	public void SetCard (CardInfo targetCard, int targetID)
	{
		cardInfo = targetCard;
		CardID = targetID;
		updateInfo ();
	}

	[ContextMenu ("UpdateInfo")]
	void updateInfo ()
	{
		string decrition = cardInfo.cardName + "\n\n" + cardInfo.description;
		GetComponentInChildren<Text> ().text = decrition;

		if (cardInfo.bgUrl != null)
			image_Bg.sprite = Resources.Load<Sprite> (cardInfo.bgUrl);
	}

	public void SetSelected (bool isSelected)
	{
		if (isSelected) {
			btn_PlayCard.gameObject.SetActive (true);
		} else {
			btn_PlayCard.gameObject.SetActive (false);
		}
	}

	//		switch (cardInfo.cardType) {
	//		case CardType.Up:
	//			decrition += "向上移动";
	//			break;
	//		case CardType.Down:
	//			decrition += "向下移动";
	//			break;
	//		case CardType.Left:
	//			decrition += "向左移动";
	//			break;
	//		case CardType.Right:
	//			decrition += "向右移动";
	//			break;
	//		case CardType.Pick:
	//			decrition += "捡起掉落品";
	//			break;
	//		case CardType.Punch:
	//			decrition += "出拳\n可以攻击同车厢/车顶的敌人";
	//			break;
	//		case CardType.Shot:
	//			decrition += "开枪\n在车顶时，可攻击车顶上所有敌人\n在车厢时，可攻击左右车厢的敌人";
	//			break;
	//		case CardType.Police:
	//			decrition += "控制警察移动";
	//			break;
	//		case CardType.Bullet:
	//			decrition += "子弹，开枪时消耗子弹";
	//			break;
	//		case CardType.UselessBullet:
	//			decrition += "废弃的子弹，你不能使用它";
	//			break;
	//		}
}
