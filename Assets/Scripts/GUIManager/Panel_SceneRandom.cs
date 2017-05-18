using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_SceneRandom : MonoBehaviour
{
	[SerializeField] Transform layout_SceneParent;
	SceneHolder[] allSceneHolderArr;

	void Awake ()
	{
		GameManager.Instance.OnProgressChange += onProgressChange;

		allSceneHolderArr = layout_SceneParent.GetComponentsInChildren<SceneHolder> ();
	}

	public void DoRandom ()
	{
		if (!gameObject.activeSelf)
			gameObject.SetActive (true);

		allSceneHolderArr = Utillity.ShuffleArray (allSceneHolderArr);
		for (int i = 0; i < allSceneHolderArr.Length; i++) {
			allSceneHolderArr [i].transform.SetSiblingIndex (i);
		}
	}

	public void OnEnterGame ()
	{
		SceneInfo[] sceneInfoArr = new SceneInfo[allSceneHolderArr.Length];
		for (int i = 0; i < sceneInfoArr.Length; i++) {
			sceneInfoArr [i] = allSceneHolderArr [i].Scene;
		}
		GameManager.Instance.SetScene (sceneInfoArr);
		GameManager.Instance.SetProgressType (GameManager.ProgressType.GameBegin);
	}

	void onProgressChange (GameManager.ProgressType targetType)
	{
		if (targetType == GameManager.ProgressType.RandomScene) {
			gameObject.SetActive (true);
		} else
			gameObject.SetActive (false);
	}

}
