using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers instance;
	public static Managers Instance { get { Init(); return instance; } }

    DamageManager damageManager;
    KeyManager keyManager;
    public static DamageManager Damage { get { return Instance.damageManager; } } // 임시 이름
    public static KeyManager Key { get { return Instance.keyManager; } }

    void Awake()
    {
        Init();
    }

    /// <summary>
    /// Prefab으로 Manager을 만들고 이 스크립트 넣기 
    /// </summary>
    static void Init()
    {
        if (instance == null)
        {
            GameObject obj = GameObject.Find("Managers");
            if (obj == null)
                obj = Resources.Load<GameObject>("Prefabs/Manager"); 

            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<Managers>();
            instance.damageManager = instance.FindManager<DamageManager>();
            instance.keyManager = instance.FindManager<KeyManager>();

            instance.keyManager.DefulatKeySetting();
        }
    }

    T FindManager<T>() where T : UnityEngine.Component
    {
        T component = gameObject.GetComponent<T>();
        if(component == null)
            component = gameObject.AddComponent<T>();

        return component;
    }
}