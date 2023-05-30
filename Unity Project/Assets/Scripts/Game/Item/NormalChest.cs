using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{
    static NormalChest instance;
    public static NormalChest Instance { get { Init(); return instance; } }

    //��� �����ۿ��� awake�� �߰��������
    public List<List<GameObject>> ItemDropList;

    static void Init()
    {
        if (instance == null)
        {
            GameObject obj = GameObject.Find("NormalChest");
            if (obj == null)
                obj = new GameObject { name = "NormalChest" };

            instance = Util.GetORAddComponet<NormalChest>(obj);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))     //�ӽ�
        {
            //��ȣ�ۿ�
            int tier = Random.Range(0, 1);
            int index = Random.Range(0, ItemDropList.Count);

            //up vector�� �ָ鼭 ��¦ �տ� �������Ѿ��� 
            Instantiate(ItemDropList[tier][index]);


        }
    }
}
