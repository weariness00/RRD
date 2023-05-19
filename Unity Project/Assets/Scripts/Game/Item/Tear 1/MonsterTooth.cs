using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MonsterTooth : MonoBehaviour
{
    UnityEvent ev;
    public ItemData iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plyaer")
        {
            Destroy(gameObject);

            iteminfo.amount++;
        }
    }

    public void HealingDrop()
    {
        //몬스터 죽음에 action추가해서 검사?



        RaycastHit hit;
        Physics.Raycast();


    }
}
