using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(HealthController))]
[RequireComponent (typeof(CardController))]
[RequireComponent (typeof(MoveController))]
public class PlayerManager : MonoBehaviour, IPointerClickHandler, IMarkable
{
	[Header ("Identity")]
	[SerializeField] int playerIndex;
	[SerializeField] PlayerInfo playerInfo;
	[SerializeField] Transform playerTrans;

	[Header ("Health")]
	[SerializeField] HealthController healthController;
	[SerializeField] int totalHealth = 3;

	[Header ("Card")]
	[SerializeField] CardController cardController;

	[Header ("Move")]
	[SerializeField] MoveController moveController;

	[Header ("Item")]
	[SerializeField] ItemController itemController;

	SpriteRenderer playerRenderer;
	bool canBeHit;

	#region Getter

	public PlayerInfo Player { get { return playerInfo; } }

	public HealthController PlayerHealthController{ get { return healthController; } }

	public CardController PlayerCardController{ get { return cardController; } }

	public MoveController PlayerMoveController{ get { return moveController; } }

	public ItemController PlayerItemController{ get { return itemController; } }

	#endregion

	void Update ()
	{
		if (moveController.PlayerTrainConnection != null)
			playerTrans.position = Vector3.Lerp (playerTrans.position, moveController.standPos, 5f * Time.deltaTime);
	}

	public void Init (int targetIndex, PlayerInfo targetPlayerInfo, PlayerInfoHolder targetPlayerInfoHolder
		, List<CardInfo> targetCardList, PlayCardManager targetPlayCardManager, TrainConnection targetTrainConnection
		, ItemHolder targetItemHolder)
	{
		playerIndex = targetIndex;
		playerInfo = targetPlayerInfo;
		playerRenderer = playerTrans.GetComponent<SpriteRenderer> ();
		playerRenderer.sprite = Resources.Load<Sprite> (targetPlayerInfo.character.spriteUrl);

		healthController.Init (totalHealth, targetPlayerInfoHolder);
		cardController.Init (playerIndex, playerInfo, targetCardList, targetPlayCardManager);
		moveController.Init (this);
		moveController.Move (targetTrainConnection);
		itemController.Init (targetPlayerInfoHolder);
		itemController.StoreItem (targetItemHolder);
	}

	#region IMarkable implementation

	public void SetMark (bool isMarked)
	{
		canBeHit = isMarked;
		playerRenderer.color = canBeHit ? Color.red : Color.white;
	}

	#endregion

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		print (name);
		if (canBeHit) {
			GameManager.Instance.gamePlayManager.accountManager.OnPlayerBeHit (this);
		}
	}

	#endregion


}