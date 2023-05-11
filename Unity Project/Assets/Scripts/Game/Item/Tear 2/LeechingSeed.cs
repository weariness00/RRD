using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeechingSeed : MonoBehaviour
{
    public Item iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);

            iteminfo.amount++;
        }
    }

    //�Ѿ˿��� ȣ���ؾ� ��
    public void DamageEffect()
    {
        GameManager.Instance.Player.GetOrAddComponent<Status>().hp.value += 1 * iteminfo.amount;
    }
}