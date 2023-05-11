using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TougherTimes : MonoBehaviour
{
    public Item iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);

            iteminfo.amount++;
            GameManager.Instance.Player.GetOrAddComponent<Status>().BlockChance.value += 15.0f;
        }
    }
}