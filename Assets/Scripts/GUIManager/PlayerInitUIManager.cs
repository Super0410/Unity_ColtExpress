using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInitUIManager : MonoBehaviour
{
	[SerializeField] PlayerInitManager playerIniter;
	[SerializeField] Text text_PlayerNum;
	[SerializeField] InputField inputField_PlayerNameInput;
	[SerializeField] Text text_PlayerNameWarning;
	[SerializeField] Transform layout_CharacterHolder;

	List<CharacterHolder> nowCharacterList;
	CharacterInfo selecedCharacter;

	public void PrepareOnePlayer (int playerIndex, GameObject[] characterAvailableArr)
	{
		text_PlayerNum.text = (playerIndex + 1).ToString ();
		inputField_PlayerNameInput.text = "";
		text_PlayerNameWarning.enabled = false;
		nowCharacterList = new List<CharacterHolder> ();

		if (layout_CharacterHolder.childCount > 0) {
			Image[] childImageArr = layout_CharacterHolder.GetComponentsInChildren<Image> ();
			for (int i = 0; i < childImageArr.Length; i++) {
				DestroyImmediate (childImageArr [i].gameObject);
			}
		}
		for (int i = 0; i < characterAvailableArr.Length; i++) {
			Transform newCharacter = Instantiate (characterAvailableArr [i]).transform;
			newCharacter.SetParent (layout_CharacterHolder, false);
			newCharacter.GetComponent<Button> ().onClick.AddListener (delegate {
				onCharacterClick (newCharacter.GetComponent<CharacterHolder> ());
			});

			nowCharacterList.Add (newCharacter.GetComponent<CharacterHolder> ());
		}
		nowCharacterList [0].GetComponent<Button> ().onClick.Invoke ();
	}

	public void OnSubmit ()
	{
		string playerName = inputField_PlayerNameInput.text;

		if (playerName == "") {
			text_PlayerNameWarning.text = "名字不允许为空";
			text_PlayerNameWarning.enabled = true;
			return;
		} 

		if (playerIniter.IsPlayerNameUsed (playerName)) {
			text_PlayerNameWarning.text = "名字已被使用";
			text_PlayerNameWarning.enabled = true;
			return;
		}
		PlayerInfo newPlayer = new PlayerInfo (playerName, selecedCharacter);
		playerIniter.AddOnePlayer (newPlayer);
	}

	void onCharacterClick (CharacterHolder targetCharacter)
	{
		selecedCharacter = targetCharacter.Character;

		for (int i = 0; i < nowCharacterList.Count; i++) {
			if (nowCharacterList [i].Character.characterName == targetCharacter.Character.characterName) {
				nowCharacterList [i].SetSelected (true);
			} else {
				nowCharacterList [i].SetSelected (false);
			}
		}
	}
}
