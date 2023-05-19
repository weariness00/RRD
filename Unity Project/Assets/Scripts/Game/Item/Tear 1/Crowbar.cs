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

    public float ItemEffect(float damage)
    {
        //�������� ���
        damage *= (1 + 0.75f * iteminfo.amount);

        return damage;
    }
}
