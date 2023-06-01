using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MuryotaisuAnimationControl : MonoBehaviour
{
	Muryotaisu player;

    private void Start()
    {
        player = GetComponent<Muryotaisu>();
    }

    void Attack()
	{
        switch (player.equipment.main_Weapon.type)
        {
            case Define.WeaponType.OneHandSword:
            case Define.WeaponType.TwoHandSword:
                player.equipment.main_Weapon.OnEffect();
                player.equipment.main_Weapon.GetComponent<BoxCollider>().enabled = true;
                Managers.Sound.Play(player.equipment.main_Weapon.attackSound, SoundType.Effect, 1.5f);
                StartCoroutine("StopAttack");
                break;
            case Define.WeaponType.Bow:
                break;
            case Define.WeaponType.Wand:
                player.equipment.main_Weapon.OnEffect();
                Managers.Sound.Play(player.equipment.main_Weapon.attackSound, SoundType.Effect, 1.5f);
                GameObject obj = player.equipment.main_Weapon.GetComponentInChildren<FireBall>().CreateFireBallObject();
                obj.transform.rotation = player.LookTransform.rotation;
                break;
        }
    }

    void EndAttack()
    {
        switch (player.equipment.main_Weapon.type)
        {
            case Define.WeaponType.OneHandSword:
            case Define.WeaponType.TwoHandSword:
                player.equipment.main_Weapon.GetComponent<BoxCollider>().enabled = false;
                StopCoroutine("StopAttack");
                break;
            case Define.WeaponType.Bow:
                break;
            case Define.WeaponType.Wand:
                break;
        }

    }

    WaitForSeconds stopAttackTime = new WaitForSeconds(0.1f);
    IEnumerator StopAttack()
    {
        yield return stopAttackTime;
        player.equipment.main_Weapon.GetComponent<BoxCollider>().enabled = false;
    }
}
