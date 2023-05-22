using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
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

    public void ItemEffect(Collider monster)
    {
        if(Random.Range(1, 100) <= 5)
        {
            //���� -> �����̶� �ڷ�ƾ����
        }
    }

    IEnumerator Stun(Collider monster)
    {
        //�� �Լ��� �ǹ̰� �ֳ�
        float temp = monster.gameObject.GetComponent<Status>().speed.value;
        monster.gameObject.GetComponent<Status>().speed.value = 0;
        yield return new WaitForSeconds(2);
        monster.gameObject.GetComponent<Status>().speed.value = temp;
    }
}
