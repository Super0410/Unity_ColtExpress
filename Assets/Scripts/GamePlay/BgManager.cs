using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
	public SpriteRenderer[] bgTransArr;
	public Vector2 posXStartEnd;
	public float moveSpeed = 3;

	void Update ()
	{
		for (int i = 0; i < bgTransArr.Length; i++) {
			bgTransArr [i].transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
			if (bgTransArr [i].transform.position.x < posXStartEnd.y) {
				bgTransArr [i].transform.position = new Vector3 (posXStartEnd.x, bgTransArr [i].transform.position.y, bgTransArr [i].transform.position.z);
			}
		}
	}

	public void UpdateBg (Sprite targetSprite)
	{
		for (int i = 0; i < bgTransArr.Length; i++) {
			bgTransArr [i].sprite = targetSprite;
		}
	}
}
