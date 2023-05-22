using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleofFireworks : MonoBehaviour
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

    //객체를 생성해주고 각각 미사일마다 감지 범위를 줘야하나
}
