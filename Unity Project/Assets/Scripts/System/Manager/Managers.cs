using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers instance = null;
	public static Managers Instance { get { Init(); return instance; } }

    DamageManager damageManager = new DamageManager();
    public static DamageManager Damage { get { return Instance.damageManager; } } // 임시 이름

    private void LateUpdate()
    {
        damageManager.LateUpdate();
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
                obj = Instantiate(new GameObject { name = "Managers" });

            //DontDestroyOnLoad(obj);
            instance = Util.GetORAddComponet<Managers>(obj);
        }
    }
}