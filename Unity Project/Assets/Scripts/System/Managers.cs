using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	Managers instance;
	public static Managers Instance { get { return Instance; } }

    public DamageManager damageManager = new DamageManager();

    public void Awake()
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