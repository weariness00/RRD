using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldiersSyringe : MonoBehaviour
{
    public Item itemInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //아이템 습득 애니메이션
            Destroy(gameObject);
            //
            itemInfo.amount++;
            GameManager.Instance.Player.GetOrAddComponent<Status>().attackSpeed.percent += 0.12f;
        }
    }


}
