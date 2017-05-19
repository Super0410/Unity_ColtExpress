using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	int playerIndex;

	public PlayerInfo m_Player{ get ; private set; }

	[SerializeField] Transform playerRenderer;
	PlayCardManager playCardManager;
	PlayerInfoHolder playerInfoManager;
	[SerializeField] List<CardInfo> storeCardList = new List<CardInfo> ();

	[SerializeField] int totalHealth = 3;
	[SerializeField] int curHealth;

	public TrainConnection m_trainPosition{ get; private set; }

	void Update ()
	{
		if (m_trainPosition != null)
			playerRenderer.position = Vector3.Lerp (playerRenderer.position, m_trainPosition.transform.position, 5f * Time.deltaTime);
	}

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