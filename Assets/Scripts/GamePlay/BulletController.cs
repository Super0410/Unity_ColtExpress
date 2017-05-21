using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField] int maxBulletCount;
	[SerializeField] int curBulletCount;

	PlayerInfoHolder playerUIManager;


	public int BulletCount { get { return curBulletCount; } }

	public void Init (int targetMaxBulletCount, PlayerInfoHolder targetPlayerUIManager)
	{
		maxBulletCount = targetMaxBulletCount;
		curBulletCount = maxBulletCount;
		playerUIManager = targetPlayerUIManager;

		playerUIManager.UpdateBulletCount (curBulletCount, maxBulletCount);
	}

	public void RemoveOneBullet ()
	{
		curBulletCount--;
		playerUIManager.UpdateBulletCount (curBulletCount, maxBulletCount);
	}

}
