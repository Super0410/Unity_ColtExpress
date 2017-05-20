using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(HealthController))]
[RequireComponent (typeof(CardController))]
[RequireComponent (typeof(MoveController))]
public class PlayerManager : MonoBehaviour
{
	[Header ("Identity")]
	[SerializeField] int playerIndex;
	[SerializeField] PlayerInfo playerInfo;
	[SerializeField] Transform playerRenderer;

	[Header ("Health")]
	[SerializeField] HealthController healthController;
	[SerializeField] int totalHealth = 3;

	[Header ("Card")]
	[SerializeField] CardController cardController;

	[Header ("Move")]
	[SerializeField] MoveController moveController;

	#region Getter

	public PlayerInfo Player { get { return playerInfo; } }

	public HealthController PlayerHealthController{ get { return healthController; } }

	public CardController PlayerCardController{ get { return cardController; } }

	public MoveController PlayerMoveController{ get { return moveController; } }

	#endregion

	void Update ()
	{
		if (moveController.PlayerTrainConnection != null)
			playerRenderer.position = Vector3.Lerp (playerRenderer.position, moveController.standPos, 5f * Time.deltaTime);
	}

	public void Init (int targetIndex, PlayerInfo targetPlayerInfo, PlayerInfoHolder targetPlayerInfoHolder, List<CardInfo> targetCardList, PlayCardManager targetPlayCardManager)
	{
		playerIndex = targetIndex;
		playerInfo = targetPlayerInfo;

		healthController.Init (totalHealth, targetPlayerInfoHolder);
		cardController.Init (playerIndex, playerInfo, targetCardList, targetPlayCardManager);
		moveController.Init (this);
	}

}