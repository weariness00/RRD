using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CautiousSlug : MonoBehaviour
{
    public ItemData iteminfo;

    private void Update()
    {
        //�÷��̾ ���� �������� �˻��� �����
        //if (GameManager.Instance.Player.fsm.CurrentState.Equals(PlayerFSM.State.Hit))
        //{
        //    StartCoroutine(ItemEffect());
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plyaer")
        {
            Destroy(gameObject);

            iteminfo.amount++;
        }
    }

    IEnumerator ItemEffect()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.Player.status.hp.value += 20 + (GameManager.Instance.Player.status.maxHp.value * 0.05f * iteminfo.amount);
    }
}
