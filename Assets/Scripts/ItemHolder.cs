using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
	[SerializeField] ItemInfo itemInfo;
	[SerializeField] Image image_apperance;

	[ContextMenu ("UpdateInfo")]
	void updateInfo ()
	{
		GetComponentInChildren<Text> ().text = itemInfo.itemName;

		if (itemInfo.itemUrl != null)
			image_apperance.sprite = Resources.Load<Sprite> (itemInfo.itemUrl);
	}
}
