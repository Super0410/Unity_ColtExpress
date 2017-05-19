using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitManager : MonoBehaviour
{
	[SerializeField] CharacterHolder[] allCharacterArr;
	[SerializeField] PlayerInitUIManager playerInitUI;
	int playerCount;
	List<PlayerInfo> allPlayerList = new List<PlayerInfo> ();

	void Awake ()
	{
		GameManager.Instance.OnProgressChange += onProgressChange;
	}

	public void Init (int playerCount)
	{
		this.playerCount = playerCount;
		allPlayerList.Clear ();

		GameObject[] availableCharacterGObjArr = getAvailableCharacterList ();
		playerInitUI.PrepareOnePlayer (0, availableCharacterGObjArr);
	}

	public bool IsPlayerNameUsed (string nameToCheck)
	{
		for (int i = 0; i < allPlayerList.Count; i++) {
			if (allPlayerList [i].playerName == nameToCheck)
				return true;
		}
		return false;
	}

	public void AddOnePlayer (PlayerInfo targetPlayer)
	{
		allPlayerList.Add (targetPlayer);

		if (allPlayerList.Count < playerCount) {
			GameObject[] availableCharacterGObjArr = getAvailableCharacterList ();
			playerInitUI.PrepareOnePlayer (allPlayerList.Count, availableCharacterGObjArr);
		} else {
			playerInitUI.OnFinish ();
			GameManager.Instance.SetAllPlayer (allPlayerList.ToArray ());
			GameManager.Instance.SetProgressType (GameManager.ProgressType.RandomScene);
		}
	}

	void onProgressChange (GameManager.ProgressType targetType)
	{
		if (targetType != GameManager.ProgressType.InitPlayer) {
		}
	}

	GameObject[] getAvailableCharacterList ()
	{
		List<GameObject> availableCharacterList = new List<GameObject> ();
		for (int i = 0; i < allCharacterArr.Length; i++) {
			if (allPlayerList.Count == 0) {
				availableCharacterList.Add (allCharacterArr [i].gameObject);
			} else {
				if (!isCharacterExist (allCharacterArr [i].Character))
					availableCharacterList.Add (allCharacterArr [i].gameObject);
			}
		}
		return availableCharacterList.ToArray ();
	}

	bool isCharacterExist (CharacterInfo characterToCheck)
	{
		for (int i = 0; i < allPlayerList.Count; i++) {
			if (allPlayerList [i].character == characterToCheck)
				return true;
		}
		return false;
	}
}
