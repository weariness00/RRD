using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LensMarksGlasses : MonoBehaviour
{
    public ItemData iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plyaer")
        {
            Destroy(gameObject);

            iteminfo.amount++;
            GameManager.Instance.Player.GetOrAddComponent<Status>().criticalProbability.value += 10f;
        }
    }
}
