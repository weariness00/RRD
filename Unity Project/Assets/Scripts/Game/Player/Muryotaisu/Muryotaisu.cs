using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Muryotaisu : PlayerController
{
    public MuryotaisuEquipment equipment;

    protected override void Start()
    {
        base.Start();
        equipment.Init();
        CreateWeapon();
    }

    protected override void Update()
    {
        base.Update();          
    }

    public void CreateWeapon()
    {
        equipment.Equip(equipment.weapons[0], animator);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            float damage = status.damage.Cal() + equipment.main_Weapon.status.damage.Cal();
            Managers.Damage.Attack(other.GetComponentInParent<Monster>(),damage);
        }
    }
}

[System.Serializable]
public class MuryotaisuEquipment
{
    public Weapon[] weapons;
    public Weapon main_Weapon;

    public GameObject leftHand;
    public GameObject rightHand;

    public void Init()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = MonoBehaviour.Instantiate(weapons[i]).GetComponent<Weapon>();
            weapons[i].gameObject.SetActive(false);
        }
    }

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

        main_Weapon?.gameObject.SetActive(false);
        _Weapon.gameObject.SetActive(true);
        main_Weapon = _Weapon;

        string animatorLayerName = "None";
        switch (main_Weapon.type)
        {
            case Define.WeaponType.None:
                break;
            case Define.WeaponType.OneHandSword:
                animatorLayerName = "One Hand Sword";
                main_Weapon.transform.SetParent(rightHand.transform);
                break;
            case Define.WeaponType.TwoHandSword:
                animatorLayerName = "Two Hand Sword";
                main_Weapon.transform.SetParent(rightHand.transform);
                break;
            case Define.WeaponType.Bow:
                break;
            case Define.WeaponType.Wand:
                animatorLayerName = "Wizard";
                main_Weapon.transform.SetParent(rightHand.transform);
                break;
            default:
                break;
        }
        for (int i = 0; i < animator.layerCount; i++)
        {
            if (animator.GetLayerName(i).Contains(animatorLayerName))
                animator.SetLayerWeight(i, 1);
        }

        main_Weapon.transform.localPosition = Vector3.zero;
        main_Weapon.transform.localRotation = Quaternion.identity;
        main_Weapon.transform.localScale = Vector3.one;
        return main_Weapon.type;
    }
}
