using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance = null;
    public static Managers Instance { get { Init(); return instance; } }

    DamageManager damageManager;
    KeyManager keyManager;
    SceneManagerEx sceneManagerEx;
    public static DamageManager Damage { get { return Instance.damageManager; } }
    public static KeyManager Key { get { return Instance.keyManager; } }
    public static SceneManagerEx Scene { get { return Instance.sceneManagerEx; } }


    public Action StartCall;
    public Action UpdateCall;
    public Action LateUpdateCall;
    public Action OnGUICall;

    private void Start()
    {
        damageManager = new DamageManager();
        keyManager = new KeyManager();
        sceneManagerEx = new SceneManagerEx();

        StartCall?.Invoke();
    }

    private void Update()
    {
        UpdateCall?.Invoke();
    }

    private void LateUpdate()
    {
        LateUpdateCall?.Invoke();
    }

    private void OnGUI()
    {
        OnGUICall?.Invoke();
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
            {
                obj = Resources.Load("Prefabs/Managers") as GameObject;
                obj = Instantiate(obj);
            }

            DontDestroyOnLoad(obj);
            instance = Util.GetORAddComponet<Managers>(obj);
        }
    }
}