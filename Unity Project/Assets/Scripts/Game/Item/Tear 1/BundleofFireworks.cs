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

    //��ü�� �������ְ� ���� �̻��ϸ��� ���� ������ ����ϳ�
}
