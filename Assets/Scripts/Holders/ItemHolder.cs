using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour, IPointerClickHandler, IMarkable
{
	[SerializeField] ItemInfo itemInfo;
	[SerializeField] SpriteRenderer sprite_apperance;
	[SerializeField] Text text_itemName;

	bool canPick;

	public ItemInfo ThisItemInfo{ get { return itemInfo; } }

	public void SetItemInfo (ItemInfo targetItemInfo)
	{
		itemInfo = targetItemInfo;
		updateInfo ();
	}

	[ContextMenu ("UpdateInfo")]
	void updateInfo ()
	{
		if (text_itemName != null)
			text_itemName.text = itemInfo.itemName;

		Sprite targetSprite = Resources.Load<Sprite> (itemInfo.itemUrl);
		if (targetSprite != null)
			sprite_apperance.sprite = targetSprite;
	}

	#region IMarkable implementation

	public void SetMark (bool isMarked)
	{
		canPick = isMarked;
		sprite_apperance.color = canPick ? Color.red : Color.white;
	}

	#endregion


	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		print (name);
		if (canPick) {
			GameManager.Instance.gamePlayManager.accountManager.OnPickUpItem (this);
		}
	}

	#endregion

}
