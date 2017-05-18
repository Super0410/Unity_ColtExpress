using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHolder : MonoBehaviour
{
	[SerializeField] CharacterInfo character;
	[SerializeField] Image image_apperance;

	Outline selecedSymbol;

	public CharacterInfo Character { get { return character; } }

	public void SetCharacter (CharacterInfo targetCharacter)
	{
		character = targetCharacter;
	}

	public void SetSelected (bool isSelected)
	{
		if (selecedSymbol == null)
			selecedSymbol = GetComponent<Outline> ();
		
		if (isSelected) {
			selecedSymbol.enabled = true;
		} else {
			selecedSymbol.enabled = false;
		}
	}

	[ContextMenu ("UpdateInfo")]
	void updateInfo ()
	{
		GetComponentInChildren<Text> ().text = character.characterName;

		if (character.spriteUrl != null)
			image_apperance.sprite = Resources.Load<Sprite> (character.spriteUrl);
	}
}
