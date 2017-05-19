using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInitUIManager : MonoBehaviour
{
	[SerializeField] Animator anim_Switch;
	[SerializeField] PlayerInitManager playerIniter;
	[SerializeField] Text text_PlayerNum;
	[SerializeField] InputField inputField_PlayerNameInput;
	[SerializeField] Text text_PlayerNameWarning;
	[SerializeField] Transform layout_CharacterParent;
	[SerializeField] CharacterHolder characterHolderPrefab;

	List<CharacterHolder> nowCharacterList;
	CharacterInfo selecedCharacter;

	public void PrepareOnePlayer (int playerIndex, CharacterInfo[] availableCharacterInfoArr)
	{
		text_PlayerNum.text = (playerIndex + 1).ToString ();
		inputField_PlayerNameInput.text = "";
		text_PlayerNameWarning.enabled = false;
		nowCharacterList = new List<CharacterHolder> ();


		GUIHelper.Instance.DestroyChildImmediatly<CharacterHolder> (layout_CharacterParent);
		CharacterHolder[] availableCharacterHolderArr = GUIHelper.Instance.InstantiateTUnderParent<CharacterHolder,CharacterInfo>
			(availableCharacterInfoArr, characterHolderPrefab, layout_CharacterParent);

		for (int i = 0; i < availableCharacterHolderArr.Length; i++) {

			CharacterHolder thisCharacterHolder = availableCharacterHolderArr [i];

			thisCharacterHolder.SetCharacter (availableCharacterInfoArr [i]);

			thisCharacterHolder.GetComponent<Button> ().onClick.AddListener (delegate {
				onCharacterClick (thisCharacterHolder);
			});

			nowCharacterList.Add (thisCharacterHolder);
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

		anim_Switch.SetTrigger ("Switch");
		PlayerInfo newPlayer = new PlayerInfo (playerName, selecedCharacter);
		playerIniter.AddOnePlayer (newPlayer);
	}

	public void OnFinish ()
	{
		UIManager.Instance.SwitchUI.CloseByTargetAnimByTrigger (anim_Switch, "Finish");
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
