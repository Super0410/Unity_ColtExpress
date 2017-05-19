using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoHolder : MonoBehaviour
{
	[SerializeField] Image image_Portrait;
	[SerializeField] Image image_HealthBar;
	[SerializeField] Text text_PlayerName;
	[SerializeField] Text text_BulletCount;

	public void Init (PlayerInfo playerInfo)
	{
		if (playerInfo.character.portraitUrl != null)
			image_Portrait.sprite = Resources.Load<Sprite> (playerInfo.character.portraitUrl);

		text_PlayerName.text = playerInfo.playerName;
		text_BulletCount.text = "1/6";
	}

	public void SetHealthFillAmount (float targetAmount)
	{
		image_HealthBar.fillAmount = targetAmount;
	}
}
