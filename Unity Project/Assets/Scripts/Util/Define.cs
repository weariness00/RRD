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
		None,		// �ָ�
		Sword,		// �Ѽհ�
		TwoHandSword, // ��հ�
		Bow,		// Ȱ
		Wand,		// ������
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
