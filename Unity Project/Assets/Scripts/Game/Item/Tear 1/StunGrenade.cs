using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    public ItemData iteminfo;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("그냥 충돌");
        if (other.tag == "Player")
        {
            iteminfo.amount++;
            GameManager.Instance.Player.ItemClassList.Add(name, gameObject);
            gameObject.SetActive(false);
        }
    }

    public void ItemEffect(Collider monster)
    {
        Debug.Log("아이템 효과 발동");
        if(Random.Range(1, 100) <= 5)
        {
            //스턴 -> 지속이라 코루틴으로
        }
    }

    IEnumerator Stun(Collider monster)
    {
        //이 함수가 의미가 있나
        float temp = monster.gameObject.GetComponent<Status>().speed.value;
        monster.gameObject.GetComponent<Status>().speed.value = 0;
        yield return new WaitForSeconds(2);
        monster.gameObject.GetComponent<Status>().speed.value = temp;
    }
}
