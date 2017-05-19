using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreviewHolder : MonoBehaviour
{
	[SerializeField] Text text_title;
	[SerializeField] Text text_PlayerName;
	[SerializeField] Text text_CharacterName;
	[SerializeField] Image image_CharacterIcon;

	int playerIndex;
	PlayerInfo playerInfo;

	void Start ()
	{
		float scale = Random.Range (0.3f, 0.5f);
		transform.localScale = Vector3.one * scale;
		StartCoroutine (animationScale ());
	}

	public void Init (int targetPlayerIndex, PlayerInfo targetPlayerInfo)
	{
		playerIndex = targetPlayerIndex;
		playerInfo = targetPlayerInfo;

		updateUI ();
	}

	void updateUI ()
	{
		text_title.text = "玩家：" + (playerIndex + 1);
		text_PlayerName.text = playerInfo.playerName;
		text_CharacterName.text = playerInfo.character.characterName;
		if (playerInfo.character.portraitUrl != null) {
			image_CharacterIcon.sprite = Resources.Load<Sprite> (playerInfo.character.portraitUrl);
		}
	}

	IEnumerator animationScale ()
	{
		float waitForTime = Random.Range (0.3f, 0.5f);
		yield return new WaitForSeconds (waitForTime);
		float scaleTime = Random.Range (0.5f, 1f);
		float scaleSpeed = 1 / scaleTime;
		float percent = 0;
		while (percent < 1) {
			percent += scaleSpeed * Time.deltaTime;
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one, percent);
			yield return null;
		}
	}

}
