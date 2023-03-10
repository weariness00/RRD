using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers instance;
	public static Managers Instance { get { Init(); return instance; } }

    DamageManager damageManager = new DamageManager();
    public static DamageManager Damage { get { return Instance.damageManager; } } // 임시 이름

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject obj = GameObject.Find("Managers");
            if (obj == null)
            {
                obj = new GameObject() { name = "Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<Managers>();
        }
    }
}