using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
	public enum CharacterType
	{
		Warrier,
		Gunner,
	}

    public enum WeaponType
    {
		None,		// 주먹
		Sword,		// 한손검
		TwoHandSword, // 양손검
		Bow,		// 활
		Wand,		// 지팡이
    }

    public enum ItemTear
	{
		Tear1,
		Tear2,
		Tear3,
		Boss,
	}

	public enum CodeBlockType
	{
		CodeBlock,
		AddBlockSpace,
	}
}
