using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustlingFungus : MonoBehaviour
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
        if (gameObject.GetComponent<PlayerFSM.Idle>().itemCheck == true)
            StartCoroutine(ItemEffect());

        else
            StopCoroutine(ItemEffect());

    }

    IEnumerator ItemEffect()
    {
        //스킬의 시각적 이펙트는 여기에

        //스킬 효과가 1초에 1번씩 작동
        while (true)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.Player.status.hp.value += GameManager.Instance.Player.status.maxHp.value * (0.045f + (0.0225f * iteminfo.amount));
        }
    }
}


