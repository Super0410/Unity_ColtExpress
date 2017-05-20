using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TrainConnection))]
public class TrainManager : MonoBehaviour
{
	[SerializeField] bool isRoof;
	[SerializeField] bool isHead;
	[SerializeField] TrainConnection trainConnection;
	TrainPropertiesManager trainProperties;

	[SerializeField] List<ItemHolder> allItemHolderList;
	[SerializeField] List<PlayerManager> allPlayerManagerList;

	#region Getter

	public bool IsRoof { get { return isRoof; } }

	public bool IsHead { get { return isHead; } }

	public TrainConnection ThisTrainConnection { get { return trainConnection; } }

	#endregion

	public void Init (TrainPropertiesManager targetTrainProperties)
	{
		trainConnection.trainManager = this;
		allItemHolderList = new List<ItemHolder> ();
		allPlayerManagerList = new List<PlayerManager> ();

		trainProperties = targetTrainProperties;

		if (isRoof)
			return;

		if (isHead) {
			ItemInfo largePackageInfo = trainProperties.itemInfoArr [2];
			GameObject newItem = Instantiate (trainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
			newItem.name = largePackageInfo.itemName;
			ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
			newItemHolder.SetItemInfo (largePackageInfo);
			StoreItem (newItemHolder);
		} else {
			ItemInfo packageInfo = trainProperties.itemInfoArr [0];
			for (int i = 0; i < trainProperties.packageCountPerTrain; i++) {
				GameObject newItem = Instantiate (trainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
				newItem.name = packageInfo.itemName;
				ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
				newItemHolder.SetItemInfo (packageInfo);
				StoreItem (newItemHolder);
			}

			ItemInfo diamondInfo = trainProperties.itemInfoArr [1];
			for (int i = 0; i < trainProperties.demondCountPerTrain; i++) {
				GameObject newItem = Instantiate (trainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
				newItem.name = diamondInfo.itemName;
				ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
				newItemHolder.SetItemInfo (diamondInfo);
				StoreItem (newItemHolder);
			}
		}
	}

	public void StoreItem (ItemHolder targetItem)
	{
		allItemHolderList.Add (targetItem);
		targetItem.transform.parent = transform;

		if (allItemHolderList != null) {
			for (int i = 0; i < allItemHolderList.Count; i++) {
				allItemHolderList [i].transform.localPosition = Vector3.zero + Vector3.right * 0.02f * (allItemHolderList.Count / 2);
				allItemHolderList [i].transform.localPosition += Vector3.left * 0.02f * i;
			}
		}
	}

	public void StorePlayer (PlayerManager targetPlayerManager)
	{
		allPlayerManagerList.Add (targetPlayerManager);

		if (allPlayerManagerList != null) {
			for (int i = 0; i < allPlayerManagerList.Count; i++) {
				allPlayerManagerList [i].PlayerMoveController.standPos = transform.position + Vector3.right * 0.5f * (allPlayerManagerList.Count / 2);
				allPlayerManagerList [i].PlayerMoveController.standPos += Vector3.left * 0.5f * i;
			}
		}
	}

	public List<ItemHolder> GetAllItem ()
	{
		return allItemHolderList;
	}
}
