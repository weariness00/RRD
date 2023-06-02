using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Muryotaisu : PlayerController
{
    public PlayerInfoCanvas playerInfoCanvas;
    public MuryotaisuEquipment equipment;
    public WeaponSkillPack[] skillPack;

    protected override void Start()
    {
        base.Start();
        equipment.Init();
        CreateWeapon();
        skill_EnhanceAttack = skillPack[index].skill_EnhanceAttack;
        skill_Auxiliary = skillPack[index].skill_Auxiliary;
    }

    protected override void Update()
    {
        base.Update();
        if (Managers.Key.InputActionDown(KeyToAction.Skill_Ultimate))
            ChangeWeapon();
    }

    public void CreateWeapon()
    {
        equipment.Equip(equipment.weapons[index], animator);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            float damage = status.damage.Cal() + equipment.main_Weapon.status.damage.Cal();
            Managers.Damage.Attack(other.GetComponentInParent<Monster>(),damage);
        }
    }

    int index = 0;
    void ChangeWeapon()
    {
        playerInfoCanvas.skillUIIsActive[skill_EnhanceAttack.GetInstanceID()] = false;
        playerInfoCanvas.skillUIIsActive[skill_Auxiliary.GetInstanceID()] = false;

        if(++index == equipment.weapons.Length) index = 0;

        equipment.Equip(equipment.weapons[index], animator);
        skill_EnhanceAttack = skillPack[index].skill_EnhanceAttack;
        skill_Auxiliary = skillPack[index].skill_Auxiliary;

        playerInfoCanvas.Skill_EnhanceAttackNode.icon.sprite = skill_EnhanceAttack?.icon;
        playerInfoCanvas.Skill_AuxiliaryNode.icon.sprite = skill_Auxiliary?.icon;
            
        playerInfoCanvas.skillUIIsActive[skill_EnhanceAttack.GetInstanceID()] = true;
        playerInfoCanvas.skillUIIsActive[skill_Auxiliary.GetInstanceID()] = true;

        playerInfoCanvas.Skill_EnhanceAttackNode.CoolUI_Active(!skill_EnhanceAttack.isOn);
        playerInfoCanvas.Skill_AuxiliaryNode.CoolUI_Active(!skill_Auxiliary.isOn);
    }
    
    [System.Serializable]
    public class WeaponSkillPack
    {
        public Skill skill_EnhanceAttack;
        public Skill skill_Auxiliary;
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
