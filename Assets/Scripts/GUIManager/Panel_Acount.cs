using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Acount : MonoBehaviour
{
	[SerializeField] Text text_RoundInfo;
	[SerializeField] Text text_PlayerAction;
	[SerializeField] Text text_Reaction;
	[SerializeField] GameObject panel_Reaction;

	public void OnBegin ()
	{
		panel_Reaction.SetActive (false);
	}

	public void UpdatePlayerAction (int playerIndex, string playerName, string actionDescription)
	{
		text_PlayerAction.text = "玩家" + (playerIndex + 1) + ":" + playerName + " " + actionDescription;
	}

	public void SetActionFail (CardType actionType)
	{
		switch (actionType) {
		case CardType.Up:

			StartCoroutine ("delaySubmitPanel", "移动失败，已经在车顶，无法向上移动");

			break;
		case CardType.Down:
			
			StartCoroutine ("delaySubmitPanel", "移动失败，已经在车厢内，无法向下移动");

			break;
		case CardType.Left:

			StartCoroutine ("delaySubmitPanel", "移动失败，已经在车尾，无法向左移动");

			break;
		case CardType.Right:

			StartCoroutine ("delaySubmitPanel", "移动失败，已经在车头，无法向右移动");

			break;
		case CardType.Pick:

			StartCoroutine ("delaySubmitPanel", "拾取失败，附件没有可拾取物品");

			break;
		case CardType.Punch:

			StartCoroutine ("delaySubmitPanel", "攻击失败，附件没有其他玩家");

			break;
		case CardType.Shot:
			
			StartCoroutine ("delaySubmitPanel", "攻击失败，瞄准范围内没有玩家");

			break;
		}
	}

	public void SetActionSuccess ()
	{
		StartCoroutine ("delaySubmitPanel", "成功");
	}

	IEnumerator delaySubmitPanel (string infoText)
	{
		yield return new WaitForSeconds (0.3f);

		panel_Reaction.SetActive (true);
		text_Reaction.text = infoText;
	}
}
