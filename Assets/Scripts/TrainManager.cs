using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TrainConnection))]
public class TrainManager : MonoBehaviour
{
	TrainPropertiesManager trainProperties;

	void Awake ()
	{
		GameManager.Instance.OnProgressChange += onProgressChange;
	}

	void Start ()
	{
		trainProperties = GetComponentInParent<TrainPropertiesManager> ();
	}

	void onProgressChange (GameManager.ProgressType targetType)
	{
		if (targetType == GameManager.ProgressType.GameBegin) {

		}
	}
}
