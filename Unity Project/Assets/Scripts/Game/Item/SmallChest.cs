using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallChest : MonoBehaviour
{
    static SmallChest instance;
    public static SmallChest Instance { get { Init(); return instance; } }

    //��� �����ۿ��� awake�� �߰��������
    public List<List<GameObject>> ItemDropList;

    private void Awake()
    {
        instance = this;
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject obj = GameObject.Find("SmallChest");
            if (obj == null)
                obj = new GameObject { name = "SmallChest" };

            instance = Util.GetORAddComponet<SmallChest>(obj);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //��ȣ�ۿ�
            int tier = Random.Range(0, 1);
            int index = Random.Range(0, ItemDropList.Count);

            //up vector�� �ָ鼭 ��¦ �տ� �������Ѿ��� 
            Instantiate(ItemDropList[tier][index]);
            
            
        }
    }
}
