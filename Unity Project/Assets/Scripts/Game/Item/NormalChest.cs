using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{

    //모든 아이템에서 awake에 추가해줘야함
    public List<List<GameObject>> ItemDropList = new List<List<GameObject>>();

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F))     //임시
        {
            //상호작용
            Debug.Log("노말 상자");
            int tier = Random.Range(0, 1);
            int index = Random.Range(0, ItemDropList.Count);

            //up vector를 주면서 살짝 앞에 출현시켜야함 
            //Instantiate(ItemDropList[tier][index], transform.position + Vector3.up, Quaternion.identity);
            Instantiate(ItemDropList[0][0], transform.position + Vector3.up, Quaternion.identity);


        }
    }
}
