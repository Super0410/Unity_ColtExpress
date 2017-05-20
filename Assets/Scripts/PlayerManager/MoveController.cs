using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	public Vector3 standPos;
	PlayerManager playerManager;
	[SerializeField] TrainConnection trainConnection;

	public TrainConnection PlayerTrainConnection{ get { return trainConnection; } }

	public void Init (PlayerManager targetPlayerManager)
	{
		playerManager = targetPlayerManager;
	}

	public void Move (TrainConnection targetTrainConnection)
	{
		trainConnection = targetTrainConnection;
		targetTrainConnection.trainManager.StorePlayer (playerManager);
	}
}
