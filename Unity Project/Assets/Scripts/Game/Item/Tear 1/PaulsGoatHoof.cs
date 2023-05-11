using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaulsGoatHoof : MonoBehaviour
{
    public Item iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);

            iteminfo.amount++;
            GameManager.Instance.Player.GetOrAddComponent<Status>().speed.value += 0.14f;
        }
    }
}
