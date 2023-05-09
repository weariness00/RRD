using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public Weapon equipmentWeapon;

    public GameObject leftHand;
    public GameObject rightHand;

    // 습득한 아이템의 애니메이션 layer로 변경하기 위해 웨폰 타입을 리턴해준다.
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
