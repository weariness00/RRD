using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalShieldGenerator : MonoBehaviour
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

    private void Update()
    {
        //�̰� ��� �˻����� �ʿ� ���� �� �����ѵ�
        ItemEffect();
    }

    public void ItemEffect()
    {
        if(GameManager.Instance.Player.outofcombat)
        {
            //GameManager.Instance.Player.status.shield.value += GameManager.Instance.Player.status.maxHp.value * 0.08f * iteminfo.amount;
        }
    }
}
