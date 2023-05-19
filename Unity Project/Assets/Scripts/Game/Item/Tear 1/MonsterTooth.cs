using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MonsterTooth : MonoBehaviour
{
    public ItemData iteminfo;
    GameObject temp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plyaer")
        {
            Destroy(gameObject);

            iteminfo.amount++;
        }
    }

    public void HealingDrop(Transform pos)
    {
        Instantiate(temp, pos);
    }
}
