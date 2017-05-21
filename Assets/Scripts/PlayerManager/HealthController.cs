using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	[SerializeField] int totalHealth = 3;
	[SerializeField] int curHealth;
	PlayerManager playerManager;
	PlayerInfoHolder playerInfoManager;

	public void Init (int targetTotalHealth, PlayerManager targetPlayerManager, PlayerInfoHolder targetPlayerInfoManager)
	{
		totalHealth = targetTotalHealth;
		curHealth = totalHealth;
		playerManager = targetPlayerManager;
		playerInfoManager = targetPlayerInfoManager;
	}

	public void TakeDamage ()
	{
		curHealth--;
		if (curHealth <= 0)
			playerDie ();

		StartCoroutine (animationTakeDamage ());
		playerInfoManager.UpdateHealth ((float)curHealth / totalHealth);
	}

	void playerDie ()
	{
		playerManager.PlayerDie ();
	}

	IEnumerator animationTakeDamage ()
	{
		GetComponent<PlayerManager> ().isAnimating = true;
		playerManager.PlayerRenderer.color = Color.red;
		float animTime = 0.3f;
		float animSpeed = 1 / animTime;
		float percent = 0;
		Vector3 prePos = transform.position;
		Vector3 animPos = transform.position;
		while (percent < 1) {
			percent += animSpeed * Time.deltaTime;
			animPos.x = Mathf.Sin (6.28f * percent) * 0.3f;
			transform.position = animPos;
			yield return null;
		}
		playerManager.PlayerRenderer.color = Color.white;
		transform.position = prePos;
		GetComponent<PlayerManager> ().isAnimating = false;
	}
}
