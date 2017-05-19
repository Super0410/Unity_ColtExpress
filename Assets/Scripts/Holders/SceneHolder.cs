using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class SceneHolder : MonoBehaviour
{
	[SerializeField] SceneInfo sceneInfo;

	public SceneInfo Scene { get { return sceneInfo; } }

	public void SetScene (SceneInfo targetScene)
	{
		sceneInfo = targetScene;
	}

	[ContextMenu ("UpdateInfo")]
	void updateInfo ()
	{
		StringBuilder sb = new StringBuilder ();
		sb.AppendFormat ("{0}\n", sceneInfo.sceneName);
		sb.AppendFormat ("每轮出牌数：{0}\n", sceneInfo.cardSideArr.Length.ToString ());
		for (int i = 0; i < sceneInfo.cardSideArr.Length; i++) {
			sb.AppendFormat (sceneInfo.cardSideArr [i] ? "-正" : "-反");
		}
		GetComponentInChildren<Text> ().text = sb.ToString ();
	}
}