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
        player.equipment.weapon.OnEffect();
        player.equipment.weapon.GetComponent<BoxCollider>().enabled = true;
    }

    void EndAttack()
    {
        player.equipment.weapon.GetComponent<BoxCollider>().enabled = false;
    }
}
