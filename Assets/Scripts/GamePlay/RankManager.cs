using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankManager : MonoBehaviour
{
	[SerializeField] PlayerRankHolder playerRankPrefab;
	[SerializeField] Transform layout_PlayerRankParent;

	public void DoRank (Dictionary<int, PlayerManager> targetRankDict)
	{
		List <PlayerManager> allPlayerManagerList = new List<PlayerManager> ();
		foreach (PlayerManager value in targetRankDict.Values) {
			allPlayerManagerList.Add (value);
		}
		allPlayerManagerList = allPlayerManagerList.OrderByDescending (x => x.PlayerItemController.MoneyCount).ToList ();
		for (int i = 0; i < allPlayerManagerList.Count; i++) {
			print (allPlayerManagerList [i].PlayerItemController.MoneyCount);
		}

		GUIHelper.Instance.DestroyChildImmediatly<PlayerRankHolder> (layout_PlayerRankParent);
		PlayerRankHolder[] allPlayerRankeHolderArr = GUIHelper.Instance.InstantiateTUnderParent<PlayerRankHolder,PlayerManager>
			(allPlayerManagerList.ToArray (), playerRankPrefab, layout_PlayerRankParent);

		for (int i = 0; i < allPlayerRankeHolderArr.Length; i++) {
			allPlayerRankeHolderArr [i].SetPlayerRankInfo (
				new PlayerRankInfo ((i + 1), allPlayerManagerList [i].PlayerIndex
					, allPlayerManagerList [i].ThisPlayerInfo, allPlayerManagerList [i].PlayerItemController.MoneyCount));
		}
	}

}
