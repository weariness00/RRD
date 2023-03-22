using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers instance = null;
	public static Managers Instance { get { return instance; } }

    DamageManager damageManager;
    KeyManager keyManager;
    public static DamageManager Damage { get { return Instance.damageManager; } }
    public static KeyManager Key { get { return Instance.keyManager; } }

    public Action StartCall;
    public Action UpdateCall;
    public Action LateUpdateCall;
    public Action OnGUICall;

    private void Awake()
    {
        Init();
        damageManager = new DamageManager();
        keyManager = new KeyManager(Util.FindChild(gameObject, "KeyManager"));
    }

    private void Start()
    {
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
    void Init()
    {
        if (instance == null)
        {
            GameObject obj = GameObject.Find("Managers");

            if (obj == null)
                obj = Instantiate(new GameObject { name = "Managers" });

            DontDestroyOnLoad(obj);
            instance = Util.GetORAddComponet<Managers>(obj);
        }
    }
}