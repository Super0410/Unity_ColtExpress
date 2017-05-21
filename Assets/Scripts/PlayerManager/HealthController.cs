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

		playerInfoManager.UpdateHealth ((float)curHealth / totalHealth);
	}

	void playerDie ()
	{
		playerManager.PlayerDie ();
	}
}
