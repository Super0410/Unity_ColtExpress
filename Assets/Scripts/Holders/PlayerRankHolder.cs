using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRankHolder : MonoBehaviour
{
	[SerializeField] Text text_Rank;
	[SerializeField] Text text_PlayerName;
	[SerializeField] Image image_PlayerProtrait;
	[SerializeField] Text text_MoneyCOunt;

	[SerializeField] PlayerRankInfo playerRankInfo;

	public void SetPlayerRankInfo (PlayerRankInfo targetPlayerRankInfo)
	{
		playerRankInfo = targetPlayerRankInfo;
		updateInfo ();
	}

	[ContextMenu ("UpdateInfo")]
	void updateInfo ()
	{
		text_Rank.text = playerRankInfo.rank.ToString ("D2");
		text_PlayerName.text = "玩家" + (playerRankInfo.playerIndex + 1) + playerRankInfo.playerInfo.playerName;
		image_PlayerProtrait.sprite = Resources.Load<Sprite> (playerRankInfo.playerInfo.character.portraitUrl);
		text_MoneyCOunt.text = playerRankInfo.moneyCount.ToString ();
	}
}
