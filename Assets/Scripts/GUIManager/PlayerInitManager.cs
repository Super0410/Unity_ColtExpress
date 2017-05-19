using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitManager : MonoBehaviour
{
	[SerializeField] PlayerInitUIManager playerInitUI;
	CharacterInfo[] availableCharacterInfoArr;
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

		availableCharacterInfoArr = GameManager.Instance.gamePlayManager.BasicCharacterInfoArr;

		setNextPlayer ();
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
			setNextPlayer ();
		} else {
			playerInitUI.OnFinish ();
			GameManager.Instance.SetAllPlayer (allPlayerList.ToArray ());
			GameManager.Instance.SetProgressType (GameManager.ProgressType.RandomScene);
		}
	}

	void setNextPlayer ()
	{
		availableCharacterInfoArr = getAvailableCharacterList ();
		playerInitUI.PrepareOnePlayer (allPlayerList.Count, availableCharacterInfoArr);
	}

	void onProgressChange (GameManager.ProgressType targetType)
	{
		if (targetType != GameManager.ProgressType.InitPlayer) {
		}
	}

	CharacterInfo[] getAvailableCharacterList ()
	{
		List<CharacterInfo> availableCharacterList = new List<CharacterInfo> ();
		for (int i = 0; i < availableCharacterInfoArr.Length; i++) {
			if (allPlayerList.Count == 0) {
				availableCharacterList.Add (availableCharacterInfoArr [i]);
			} else {
				if (!isCharacterExist (availableCharacterInfoArr [i]))
					availableCharacterList.Add (availableCharacterInfoArr [i]);
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
