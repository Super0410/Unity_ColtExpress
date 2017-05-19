using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchUIManager : MonoBehaviour
{
	string animOpenStr = "IsOpen";
	string animOpenTrigger = "Open";
	string animCloseTrigger = "Close";

	public void OpenPanel (Animator anim)
	{
		anim.gameObject.SetActive (true);
		anim.SetBool (animOpenStr, true);
	}

	public void OpenPanelByTrigger (Animator anim)
	{
		anim.gameObject.SetActive (true);
		anim.SetTrigger (animOpenTrigger);
	}

	public void CloseCurrent (Animator anim)
	{
		anim.SetBool (animOpenStr, false);
		StartCoroutine (DisablePanelDeleyed (anim));
	}

	public void CloseCurrentByTrigger (Animator anim)
	{
		anim.SetTrigger (animCloseTrigger);
		StartCoroutine (DisablePanelDeleyed (anim));
	}

	public void CloseByTargetAnimByTrigger (Animator anim, string triggerStr)
	{
		anim.SetTrigger (triggerStr);
		StartCoroutine (DisablePanelDeleyed (anim));
	}

	IEnumerator DisablePanelDeleyed (Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose) {
			if (!anim.IsInTransition (0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo (0).IsName ("Closed");
           
			yield return new WaitForEndOfFrame ();
		}

		if (wantToClose)
			anim.gameObject.SetActive (false);
	}
}
