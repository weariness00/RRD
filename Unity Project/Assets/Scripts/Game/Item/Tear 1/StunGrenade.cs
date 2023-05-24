using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    public ItemData iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�׳� �浹");
        if (other.tag == "Player")
        {
            iteminfo.amount++;
            GameManager.Instance.Player.ItemClassList.Add(name, gameObject);
            gameObject.SetActive(false);
        }
    }

    public void ItemEffect(Collider monster)
    {
        Debug.Log("������ ȿ�� �ߵ�");
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
