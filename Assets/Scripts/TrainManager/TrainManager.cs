using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TrainConnection))]
public class TrainManager : MonoBehaviour
{
	[SerializeField] bool isRoof;
	[SerializeField] bool isHead;
	[SerializeField] TrainConnection trainConnection;

	[SerializeField] List<ItemHolder> allItemHolderList;
	[SerializeField] List<PlayerManager> allPlayerManagerList;

	PoliceManager policeManager;

	#region Getter

	public bool IsRoof { get { return isRoof; } }

	public bool IsHead { get { return isHead; } }

	public TrainConnection ThisTrainConnection { get { return trainConnection; } }

	public List<ItemHolder> GetAllItemHolder { get { return allItemHolderList; } }

	public List<PlayerManager> GetAllPlayerManager{ get { return allPlayerManagerList; } }

	#endregion

	public void Init (TrainPropertiesInfo targetTrainProperties)
	{
		trainConnection.trainManager = this;
		allItemHolderList = new List<ItemHolder> ();
		allPlayerManagerList = new List<PlayerManager> ();

		if (isRoof)
			return;

		ItemInfo[] itemInfoArr = GameManager.Instance.gamePlayManager.BasicItemInfoArr;

		if (isHead) {
			//Item
			ItemInfo largePackageInfo = itemInfoArr [2];
			GameObject newItem = Instantiate (targetTrainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
			newItem.name = largePackageInfo.itemName;
			ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
			newItemHolder.SetItemInfo (largePackageInfo);
			StoreItem (newItemHolder);

			//Police
			GameObject newPolice = Instantiate (GameManager.Instance.gamePlayManager.PolicePrefab);
			newPolice.GetComponent<PoliceManager> ().Move (trainConnection);
		} else {
			ItemInfo packageInfo = itemInfoArr [0];
			for (int i = 0; i < targetTrainProperties.packageCountPerTrain; i++) {
				GameObject newItem = Instantiate (targetTrainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
				newItem.name = packageInfo.itemName;
				ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
				newItemHolder.SetItemInfo (packageInfo);
				StoreItem (newItemHolder);
			}

			ItemInfo diamondInfo = itemInfoArr [1];
			for (int i = 0; i < targetTrainProperties.demondCountPerTrain; i++) {
				GameObject newItem = Instantiate (targetTrainProperties.itemHolderPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
				newItem.name = diamondInfo.itemName;
				ItemHolder newItemHolder = newItem.GetComponent<ItemHolder> ();
				newItemHolder.SetItemInfo (diamondInfo);
				StoreItem (newItemHolder);
			}
		}
	}

	public void StoreItem (ItemHolder targetItem)
	{
		if (targetItem == null)
			return;
		
		allItemHolderList.Add (targetItem);
		targetItem.transform.parent = transform;

		if (allItemHolderList != null) {
			for (int i = 0; i < allItemHolderList.Count; i++) {
				allItemHolderList [i].transform.localPosition = Vector3.zero + Vector3.right * 0.02f * ((float)allItemHolderList.Count / 2);
				allItemHolderList [i].transform.localPosition += Vector3.left * 0.02f * i;
			}
		}
	}

	public void PickUpItem (ItemHolder pickedItem)
	{
		allItemHolderList.Remove (pickedItem);
	}

	public void StorePlayer (PlayerManager targetPlayerManager)
	{
		allPlayerManagerList.Add (targetPlayerManager);

		if (allPlayerManagerList != null) {
			for (int i = 0; i < allPlayerManagerList.Count; i++) {
				allPlayerManagerList [i].PlayerMoveController.standPos = transform.position + Vector3.right * 0.5f * ((float)allPlayerManagerList.Count / 2);
				allPlayerManagerList [i].PlayerMoveController.standPos += Vector3.left * 0.5f * i;
			}
		}

		if (policeManager != null)
			StorePolice (policeManager);
	}

	public void LeavePlayer (PlayerManager leavePlayerManager)
	{
		allPlayerManagerList.Remove (leavePlayerManager);
	}

	public void StorePolice (PoliceManager targetPoliceManager)
	{
		policeManager = targetPoliceManager;
		targetPoliceManager.standPos = transform.position + Vector3.right * 0.5f * ((float)allPlayerManagerList.Count / 2);
		targetPoliceManager.standPos += Vector3.left * 0.5f * allPlayerManagerList.Count;
	}

	public void LeavePolice ()
	{
		policeManager = null;
	}
}
