using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuryotaisuAnimationControl : MonoBehaviour
{
	Muryotaisu player;

    private void Start()
    {
        player = GetComponent<Muryotaisu>();
    }

    void Attack()
	{
        player.equipment.main_Weapon.OnEffect();
        player.equipment.main_Weapon.GetComponent<BoxCollider>().enabled = true;
        Managers.Sound.Play(player.attack, SoundType.Effect, 1.5f);
        StartCoroutine(StopAttack());
    }

    void EndAttack()
    {
        player.equipment.main_Weapon.GetComponent<BoxCollider>().enabled = false;
        StopCoroutine(StopAttack());
    }

    WaitForSeconds stopAttackTime = new WaitForSeconds(0.1f);
    IEnumerator StopAttack()
    {
        yield return stopAttackTime;
        player.equipment.main_Weapon.GetComponent<BoxCollider>().enabled = false;
    }
}
