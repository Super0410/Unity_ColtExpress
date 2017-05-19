using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TrainConnection))]
public class TrainManager : MonoBehaviour
{
	[SerializeField] bool isRoof;
	[SerializeField] bool isHead;
	TrainPropertiesManager trainProperties;

	List<ItemHolder> allItemList;

	public bool IsRoof { get { return isRoof; } }

	public bool IsHead { get { return isHead; } }

	public void InitItem (TrainPropertiesManager targetTrainProperties)
	{
		trainProperties = targetTrainProperties;
		allItemList = new List<ItemHolder> ();

		if (isRoof)
			return;

		if (isHead) {
			ItemInfo largePackageInfo = trainProperties.itemInfoArr [2];
			GameObject newItem = Instantiate (trainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
			newItem.name = largePackageInfo.itemName;
			ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
			newItemHolder.SetItemInfo (largePackageInfo);
			LoadItem (newItemHolder);
		} else {
			ItemInfo packageInfo = trainProperties.itemInfoArr [0];
			for (int i = 0; i < trainProperties.packageCountPerTrain; i++) {
				GameObject newItem = Instantiate (trainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
				newItem.name = packageInfo.itemName;
				ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
				newItemHolder.SetItemInfo (packageInfo);
				LoadItem (newItemHolder);
			}

			ItemInfo diamondInfo = trainProperties.itemInfoArr [1];
			for (int i = 0; i < trainProperties.demondCountPerTrain; i++) {
				GameObject newItem = Instantiate (trainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
				newItem.name = diamondInfo.itemName;
				ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
				newItemHolder.SetItemInfo (diamondInfo);
				LoadItem (newItemHolder);
			}
		}
	}

	public void LoadItem (ItemHolder targetItem)
	{
		allItemList.Add (targetItem);
		targetItem.transform.parent = transform;

		ItemHolder[] allChildItemArr = GetComponentsInChildren<ItemHolder> ();
		if (allChildItemArr != null) {
			for (int i = 0; i < allChildItemArr.Length; i++) {
				allChildItemArr [i].transform.localPosition = Vector3.zero + Vector3.right * 0.02f * (allChildItemArr.Length / 2);
				allChildItemArr [i].transform.localPosition += Vector3.left * 0.02f * i;
			}
		}
	}

	public List<ItemHolder> GetAllItem ()
	{
		return allItemList;
	}
}
