using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	Stack<ItemInfo> holdItemInfoStack = new Stack<ItemInfo> ();

	public void StoreItem (ItemInfo targetItemInfo)
	{
		holdItemInfoStack.Push (targetItemInfo);
	}

	public ItemInfo GetLastStoreItem ()
	{
		ItemInfo lastItemInfo = holdItemInfoStack.Pop ();
		return lastItemInfo;
	}
}
