using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	PlayerInfoHolder playerUIManager;
	Stack<ItemHolder> holdItemHolderStack = new Stack<ItemHolder> ();
	int moneyCount;

	public int MoneyCount{ get { return moneyCount; } }

	public void Init (PlayerInfoHolder targetPlayerUIManager)
	{
		playerUIManager = targetPlayerUIManager;
	}

	public void StoreItem (ItemHolder targetItemInfo)
	{
		targetItemInfo.gameObject.SetActive (false);
		holdItemHolderStack.Push (targetItemInfo);

		playerUIManager.UpdateItem (holdItemHolderStack.ToArray ());

		calculateMoney (holdItemHolderStack.ToArray ());
	}

	public ItemHolder GetLastStoreItem ()
	{
		ItemHolder lastItemHolder = null;
		if (holdItemHolderStack.Count > 0) {
			lastItemHolder = holdItemHolderStack.Pop ();
			lastItemHolder.gameObject.SetActive (true);

			playerUIManager.UpdateItem (holdItemHolderStack.ToArray ());
		}
		StartCoroutine (animationTakePunch ());
		return lastItemHolder;
	}

	void calculateMoney (ItemHolder[] targetItemHolderArr)
	{
		moneyCount = 0;
		for (int i = 0; i < targetItemHolderArr.Length; i++) {
			moneyCount += targetItemHolderArr [i].ThisItemInfo.moneyCount;
		}
	}

	IEnumerator animationTakePunch ()
	{
		GetComponent<PlayerManager> ().isAnimating = true;
		float animTime = 0.3f;
		float animSpeed = 1 / animTime;
		float percent = 0;
		Vector3 prePos = transform.position;
		Vector3 animPos = transform.position;
		while (percent < 1) {
			percent += animSpeed * Time.deltaTime;
			animPos.y = Mathf.Sin (3.14f * percent) * 0.5f;
			transform.position = animPos;
			yield return null;
		}
		transform.position = prePos;
		GetComponent<PlayerManager> ().isAnimating = false;
	}
}
