using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	int playerIndex;
	[SerializeField] PlayerInfo m_Player;
	PlayerInfoHolder playerInfoManager;
	[SerializeField] List<CardInfo> storeCardList = new List<CardInfo> ();
	PlayCardManager playCardManager;

	[SerializeField] int totalHealth = 3;
	[SerializeField] int curHealth;

	public TrainConnection m_trainPosition{ get; private set; }

	public void Init (int targetIndex, PlayerInfo playerIdentity, PlayerInfoHolder targetPlayerInfoHolder, List<CardInfo> targetCardList, PlayCardManager targetManager)
	{
		playerIndex = targetIndex;
		m_Player = playerIdentity;
		playerInfoManager = targetPlayerInfoHolder;
		storeCardList = targetCardList;
		playCardManager = targetManager;

		curHealth = totalHealth;
	}

	public void PlayCard ()
	{
		playCardManager.PlayerPlay (playerIndex, m_Player, storeCardList.ToArray ());
	}

	public void Move (TrainConnection targetTrainConnection)
	{
		m_trainPosition = targetTrainConnection;
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