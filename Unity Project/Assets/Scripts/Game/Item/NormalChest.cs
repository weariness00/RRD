using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{

    //��� �����ۿ��� awake�� �߰��������
    public List<List<GameObject>> ItemDropList = new List<List<GameObject>>();

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F))     //�ӽ�
        {
            //��ȣ�ۿ�
            Debug.Log("�븻 ����");
            int tier = Random.Range(0, 1);
            int index = Random.Range(0, ItemDropList.Count);

            //up vector�� �ָ鼭 ��¦ �տ� �������Ѿ��� 
            //Instantiate(ItemDropList[tier][index], transform.position + Vector3.up, Quaternion.identity);
            Instantiate(ItemDropList[0][0], transform.position + Vector3.up, Quaternion.identity);


        }
    }
}
