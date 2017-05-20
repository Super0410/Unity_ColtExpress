using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour, IPointerClickHandler
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

	public void SetCanPick (bool canPick)
	{
		this.canPick = canPick;
		sprite_apperance.color = canPick ? Color.red : Color.white;
	}

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
