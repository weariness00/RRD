using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallChest : MonoBehaviour
{
    static SmallChest instance;
    public static SmallChest Instance { get { Init(); return instance; } }

    //모든 아이템에서 awake에 추가해줘야함
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
            //상호작용
            int tier = Random.Range(0, 1);
            int index = Random.Range(0, ItemDropList.Count);

            //up vector를 주면서 살짝 앞에 출현시켜야함 
            Instantiate(ItemDropList[tier][index]);
            
            
        }
    }
}
