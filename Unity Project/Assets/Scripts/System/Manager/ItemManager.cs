using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    public List<GameObject> itemPrefab;

    public static ItemManager Instance;



    private void Awake()
    {
        Instance = this;
    }







    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //인벤토리에 추가
            Destroy(gameObject);
        }
    }*/
}
