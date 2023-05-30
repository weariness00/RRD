using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Muryotaisu : PlayerController
{
    public MuryotaisuEquipment equipment;
    public AudioClip attack;

    protected override void Start()
    {
        base.Start();
        CreateWeapon();
    }

    protected override void Update()
    {
        base.Update();          
    }

    public void CreateWeapon()
    {
        int layer = (int)equipment.Equip(Instantiate(equipment.weapon), animator);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            float damage = status.damage.Cal() + equipment.weapon.status.damage.Cal();
            Managers.Damage.Attack(other.GetComponentInParent<Monster>(),damage);
        }
    }
}

[System.Serializable]
public class MuryotaisuEquipment
{
    public Weapon weapon;

    public GameObject leftHand;
    public GameObject rightHand;

    // 습득한 아이템의 애니메이션 layer로 변경하기 위해 웨폰 타입을 리턴해준다.
    public Define.WeaponType Equip(Weapon _Weapon, Animator animator)
    {
        for (int i = 1; i < animator.layerCount; i++)
        {
            if (animator.GetLayerName(i).Contains("Base"))
                animator.SetLayerWeight(i, 1);
            else
                animator.SetLayerWeight(i, 0);
        }

        weapon = _Weapon;
        switch (weapon.type)
        {
            case Define.WeaponType.None:
                break;
            case Define.WeaponType.Sword:
                weapon.transform.SetParent(leftHand.transform);
                break;
            case Define.WeaponType.TwoHandSword:
                for (int i = 0; i < animator.layerCount; i++)
                {
                    if (animator.GetLayerName(i).Contains("Two Hand Sword"))
                        animator.SetLayerWeight(i, 1);
                }
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
