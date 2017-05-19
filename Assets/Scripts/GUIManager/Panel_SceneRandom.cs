using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_SceneRandom : MonoBehaviour
{
	[SerializeField] Animator anim;
	[SerializeField] PlayerPreviewHolder playerPreviewPrefab;
	[SerializeField] Transform layout_PlayerPreviewParent;
	SceneInfo[] allSceneInfoArr;

	void Awake ()
	{
		GameManager.Instance.OnProgressChange += onProgressChange;

		allSceneInfoArr = GameManager.Instance.gamePlayManager.BasicSceneInfoArr;
	}

	public void ShowPlayerPreview (PlayerInfo[] targetPlayerInfo)
	{
		if (!gameObject.activeSelf) {
			gameObject.SetActive (true);
			UIManager.Instance.SwitchUI.OpenPanel (anim);
		}

		GUIHelper.Instance.DestroyChildImmediatly<PlayerPreviewHolder> (layout_PlayerPreviewParent);

		PlayerPreviewHolder[] newPlayerPreviewHolderArr = GUIHelper.Instance.InstantiateTUnderParent<PlayerPreviewHolder, PlayerInfo>
			(targetPlayerInfo, playerPreviewPrefab, layout_PlayerPreviewParent);
		for (int i = 0; i < newPlayerPreviewHolderArr.Length; i++) {
			newPlayerPreviewHolderArr [i].Init (i, targetPlayerInfo [i]);
		}

		allSceneInfoArr = Utillity.ShuffleArray (allSceneInfoArr);
	}

	public void OnEnterGame ()
	{
		GameManager.Instance.SetScene (allSceneInfoArr);
		GameManager.Instance.SetProgressType (GameManager.ProgressType.GameBegin);
	}

	void onProgressChange (GameManager.ProgressType targetType)
	{
		if (targetType == GameManager.ProgressType.RandomScene) {
			UIManager.Instance.SwitchUI.OpenPanel (anim);
		} else
			UIManager.Instance.SwitchUI.CloseCurrent (anim);
	}
}
