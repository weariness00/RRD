using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustlingFungus : MonoBehaviour
{
    public ItemData iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            iteminfo.amount++;


            gameObject.SetActive(false);
        }

        
    }

    private void Update()
    {
        /*if (gameObject.GetComponent == true)
            StartCoroutine(ItemEffect());

        else
            StopCoroutine(ItemEffect());*/

    }

    IEnumerator ItemEffect()
    {
        //��ų�� �ð��� ����Ʈ�� ���⿡

        //��ų ȿ���� 1�ʿ� 1���� �۵�
        while (true)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.Player.status.hp.value += GameManager.Instance.Player.status.maxHp.value * (0.045f + (0.0225f * iteminfo.amount));
        }
    }
}


