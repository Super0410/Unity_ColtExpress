using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoHolder : MonoBehaviour
{
	[SerializeField] Image image_Portrait;
	[SerializeField] Image image_HealthBar;
	[SerializeField] Text text_PlayerName;
	[SerializeField] Text text_BulletCount;
	[SerializeField] Transform layout_ItemParent;
	[SerializeField] Image itemPrefab;

	public void Init (PlayerInfo playerInfo)
	{
		if (playerInfo.character.portraitUrl != null)
			image_Portrait.sprite = Resources.Load<Sprite> (playerInfo.character.portraitUrl);

		text_PlayerName.text = playerInfo.playerName;
		text_BulletCount.text = "6/6";
	}

	public void UpdateHealth (float targetAmount)
	{
		print (targetAmount);
		image_HealthBar.fillAmount = targetAmount;
	}

	public void UpdateItem (ItemHolder[] targetItemHolderArr)
	{
		GUIHelper.Instance.DestroyChildImmediatly<Image> (layout_ItemParent);
		Image[] newItemImageArr = GUIHelper.Instance.InstantiateTUnderParent<Image,ItemHolder> (targetItemHolderArr, itemPrefab, layout_ItemParent);
		for (int i = 0; i < newItemImageArr.Length; i++) {
			newItemImageArr [i].sprite = Resources.Load<Sprite> (targetItemHolderArr [i].ThisItemInfo.itemUrl);
		}
	}

	public void UpdateBulletCount (int ownBulletCount)
	{
		text_BulletCount.text = ownBulletCount + "/6";
	}
}
