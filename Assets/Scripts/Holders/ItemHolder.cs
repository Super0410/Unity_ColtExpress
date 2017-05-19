using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
	[SerializeField] ItemInfo itemInfo;
	[SerializeField] SpriteRenderer sprite_apperance;
	[SerializeField] Text text_itemName;

	public void SetItemInfo (ItemInfo targetItemInfo)
	{
		itemInfo = targetItemInfo;
		updateInfo ();
	}

	[ContextMenu ("UpdateInfo")]
	void updateInfo ()
	{
		text_itemName.text = itemInfo.itemName [0].ToString ();

		if (itemInfo.itemUrl != null)
			sprite_apperance.sprite = Resources.Load<Sprite> (itemInfo.itemUrl);
	}
}
