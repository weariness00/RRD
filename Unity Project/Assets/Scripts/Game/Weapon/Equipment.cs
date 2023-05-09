using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public Weapon equipmentWeapon;

    public GameObject leftHand;
    public GameObject rightHand;

    // ������ �������� �ִϸ��̼� layer�� �����ϱ� ���� ���� Ÿ���� �������ش�.
    public Define.WeaponType Equip(Weapon weapon)
    {
        switch (weapon.type)
        {
            case Define.WeaponType.None:
                break;
            case Define.WeaponType.Sword:
                weapon.transform.parent = leftHand.transform;
                break;
            case Define.WeaponType.TwoHandSword:
                weapon.transform.parent = rightHand.transform;
                break;
            case Define.WeaponType.Bow:
                break;
            case Define.WeaponType.Wand:
                break;
            default:
                break;
        }

        equipmentWeapon = weapon;
        return weapon.type;
    }
}
