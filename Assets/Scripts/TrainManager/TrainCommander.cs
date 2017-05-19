using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCommander : MonoBehaviour
{
	[SerializeField] TrainPropertiesManager trainPropertiesManager;
	[SerializeField] TrainManager[] trainManagerArr;
	List<TrainConnection> trainPlayerStartList = new List<TrainConnection> ();

	void Awake ()
	{
		for (int i = 0; i < trainManagerArr.Length; i++) {
			if (!trainManagerArr [i].IsRoof && !trainManagerArr [i].IsHead) {
				trainPlayerStartList.Add (trainManagerArr [i].GetComponent<TrainConnection> ());
			}
		}
	}

	public void InitAllManager ()
	{
		for (int i = 0; i < trainManagerArr.Length; i++) {
			trainManagerArr [i].InitItem (trainPropertiesManager);
		}
	}

	public TrainConnection GetRandomPlayerStartTrainConnection ()
	{
		int randomIndex = Random.Range (0, trainPlayerStartList.Count);
		return trainPlayerStartList [randomIndex];
	}
}
