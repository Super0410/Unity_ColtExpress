using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	public Vector3 standPos;
	PlayerManager playerManager;
	TrainConnection trainConnection;

	public TrainConnection PlayerTrainConnection{ get { return trainConnection; } }

	public void Init (PlayerManager targetPlayerManager)
	{
		playerManager = targetPlayerManager;
	}

	public void Move (TrainConnection targetTrainConnection)
	{
		if (trainConnection != null)
			trainConnection.trainManager.LeavePlayer (playerManager);
		trainConnection = targetTrainConnection;
		targetTrainConnection.trainManager.StorePlayer (playerManager);
	}
}
