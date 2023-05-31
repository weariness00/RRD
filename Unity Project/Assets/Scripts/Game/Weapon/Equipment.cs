using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public Weapon weapon;

    public GameObject leftHand;
    public GameObject rightHand;

    // ������ �������� �ִϸ��̼� layer�� �����ϱ� ���� ���� Ÿ���� �������ش�.
    public Define.WeaponType Equip(Weapon _Weapon)
    {
        weapon = _Weapon;
        switch (weapon.type)
        {
            case Define.WeaponType.None:
                break;
            case Define.WeaponType.OneHandSword:
                weapon.transform.SetParent(leftHand.transform);
                break;
            case Define.WeaponType.TwoHandSword:
                weapon.transform.SetParent(rightHand.transform);
                break;
            case Define.WeaponType.Bow:
                break;
            case Define.WeaponType.Wand:
                break;
            default:
                break;
        }

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;
        return weapon.type;
    }
}
