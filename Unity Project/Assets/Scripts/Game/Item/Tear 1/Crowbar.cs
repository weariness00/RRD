using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour
{
    public ItemData iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            iteminfo.amount++;
        }
    }

    public float ItemEffect(Monster monster, float damage)
    {
        if (monster.status.hp.value >= monster.status.maxHp.value * 0.9f)
        {
            //증폭으로 계산
            damage *= (1 + 0.75f * iteminfo.amount);
        }

        return damage;
    }
}
