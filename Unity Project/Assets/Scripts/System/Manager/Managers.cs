using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers instance = null;
	public static Managers Instance { get { Init(); return instance; } }

    DamageManager damageManager;
    public static DamageManager Damage { get { return Instance.damageManager; } } // �ӽ� �̸�

    void Awake()
    {
        Init();
    }

    /// <summary>
    /// Prefab���� Manager�� ����� �� ��ũ��Ʈ �ֱ� 
    /// </summary>
    static void Init()
    {
        if (instance == null)
        {
            GameObject obj = GameObject.Find("Managers");
            if (obj == null)
                obj = Resources.Load<GameObject>("Prefabs/Manager");

            //DontDestroyOnLoad(obj);
            instance = obj.GetComponent<Managers>();
            instance.damageManager = instance.FindManager<DamageManager>();
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