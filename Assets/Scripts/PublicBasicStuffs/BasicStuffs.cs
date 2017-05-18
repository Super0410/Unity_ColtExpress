using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerInfo
{
	public string playerName;
	public CharacterInfo character;

	public PlayerInfo (string _name, CharacterInfo _character)
	{
		playerName = _name;
		character = _character;
	}
}

[System.Serializable]
public struct CharacterInfo
{
	public string characterName;
	public string spriteUrl;
	public string portraitUrl;

	public static bool operator == (CharacterInfo c1, CharacterInfo c2)
	{
		return c1.characterName == c2.characterName;
	}

	public static bool operator != (CharacterInfo c1, CharacterInfo c2)
	{
		return c1.characterName != c2.characterName;
	}
}

[System.Serializable]
public struct SceneInfo
{
	public string sceneName;
	public bool[] cardSideArr;
	public string bgUrl;
}

[System.Serializable]
public struct CardInfo
{
	public string cardName;
	public CardType cardType;
	public string description;
	public string bgUrl;
}

[System.Serializable]
public struct PlayerIndexCardInfo
{
	public int playerIndex;
	public CardInfo cardInfo;

	public PlayerIndexCardInfo (int targetIndex, CardInfo targetCardInfo)
	{
		playerIndex = targetIndex;
		cardInfo = targetCardInfo;
	}
}

[System.Serializable]
public struct ItemInfo
{
	public string itemName;
	public string moneyCount;
	public string itemUrl;
}

public enum CardType
{
	Up,
	Down,
	Left,
	Right,
	Pick,
	Punch,
	Shot,
	Police,
	Bullet,
	UselessBullet
}

public enum MoveType
{
	Up,
	Down,
	Left,
	Right
}
