using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    public ItemData iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plyaer")
        {
            Destroy(gameObject);

            iteminfo.amount++;
        }
    }

    public void ItemEffect()
    {
        //�޸��� �ӵ� ����
        //�긦 fsm�� ������ runspeed�� ������ �����
    }
}
