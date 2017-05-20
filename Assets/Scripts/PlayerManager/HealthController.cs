using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	[SerializeField] int totalHealth = 3;
	[SerializeField] int curHealth;
	PlayerInfoHolder playerInfoManager;

	public void Init (int targetTotalHealth, PlayerInfoHolder targetPlayerInfoManager)
	{
		totalHealth = targetTotalHealth;
		curHealth = totalHealth;
		playerInfoManager = targetPlayerInfoManager;
	}

	public void TakeDamage ()
	{
		curHealth--;
		if (curHealth <= 0)
			playerDie ();

		playerInfoManager.SetHealthFillAmount ((float)curHealth / totalHealth);
	}

	void playerDie ()
	{

	}
}
