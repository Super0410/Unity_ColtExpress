using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceManager : MonoBehaviour
{
	[HideInInspector] public Vector3 standPos;
	[SerializeField] Transform playerTrans;
	[SerializeField] Canvas panel_MoveBtn;
	[SerializeField] Button btn_Up;
	[SerializeField] Button btn_Down;
	[SerializeField] Button btn_Left;
	[SerializeField] Button btn_Right;

	[SerializeField] TrainConnection trainConnection;

	public TrainConnection PlayerTrainConnection{ get { return trainConnection; } }

	void Update ()
	{
		if (trainConnection != null)
			playerTrans.position = Vector3.Lerp (playerTrans.position, standPos, 5f * Time.deltaTime);
	}

	public void Move (TrainConnection targetTrainConnection)
	{
		if (trainConnection != null)
			trainConnection.trainManager.LeavePolice ();
		trainConnection = targetTrainConnection;
		trainConnection.trainManager.StorePolice (this);

		List<PlayerManager> thisTrainPlayerManagerList = trainConnection.trainManager.GetAllPlayerManager;
		for (int i = 0; i < thisTrainPlayerManagerList.Count; i++) {
			thisTrainPlayerManagerList [i].PlayerHealthController.TakeDamage ();
			thisTrainPlayerManagerList [i].PlayerCardController.AddOneUselessBulletCard ();
		}
	}

	public void ShowPath ()
	{
		bool canUp = !(trainConnection.nearbyTrain_Up == null);
		bool canDown = !(trainConnection.nearbyTrain_Down == null);
		bool canLeft = !(trainConnection.nearbyTrain_Left == null);
		bool canRight = !(trainConnection.nearbyTrain_Right == null);

		panel_MoveBtn.gameObject.SetActive (true);
		btn_Up.gameObject.SetActive (canUp);
		btn_Down.gameObject.SetActive (canDown);
		btn_Left.gameObject.SetActive (canLeft);
		btn_Right.gameObject.SetActive (canRight);
	}

	public void OnClickMove (string targetDir)
	{
		panel_MoveBtn.gameObject.SetActive (false);

		switch (targetDir) {
		case "Up":
			Move (trainConnection.nearbyTrain_Up);
			break;
		case "Down":
			Move (trainConnection.nearbyTrain_Down);
			break;
		case "Left":
			Move (trainConnection.nearbyTrain_Left);
			break;
		case "Right":
			Move (trainConnection.nearbyTrain_Right);
			break;
		}

		GameManager.Instance.gamePlayManager.accountManager.OnPoliceMoveFinish ();
	}
}
