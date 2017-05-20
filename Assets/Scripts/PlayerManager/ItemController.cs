using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	PlayerInfoHolder playerUIManager;
	Stack<ItemHolder> holdItemHolderStack = new Stack<ItemHolder> ();

	public void Init (PlayerInfoHolder targetPlayerUIManager)
	{
		playerUIManager = targetPlayerUIManager;
	}

	public void StoreItem (ItemHolder targetItemInfo)
	{
		targetItemInfo.gameObject.SetActive (false);
		holdItemHolderStack.Push (targetItemInfo);

		playerUIManager.UpdateItem (holdItemHolderStack.ToArray ());
	}

	public ItemHolder GetLastStoreItem ()
	{
		ItemHolder lastItemHolder = holdItemHolderStack.Pop ();
		if (lastItemHolder != null)
			lastItemHolder.gameObject.SetActive (true);

		playerUIManager.UpdateItem (holdItemHolderStack.ToArray ());
		return lastItemHolder;
	}
}
