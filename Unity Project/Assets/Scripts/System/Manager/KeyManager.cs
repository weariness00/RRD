using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class KeyDataInfo
{
	public KeyCode Key;
	public KeyManager.KeyToAction Action;
}

/// <summary>
/// 키 변경을 해도 똑같은 행동(Action)이 나오게 하기
/// </summary>
public class KeyManager : MonoBehaviour
{
	static KeyManager instance = null;
	public static KeyManager Instance { get { Init(); return instance; } }

	public enum KeyToAction
	{
		MoveFront,
		MoveBack,
	}

	[HideInInspector] public Dictionary<KeyToAction, Action> keyAcitionDictionary = new Dictionary<KeyToAction, Action>();
	[HideInInspector] public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();

	[SerializeField] KeySettingData keyData;

    // Init은 제대로 해주나
    // Update 구문에 들어오면 Dictionary 멤버변수들이 초기화 되고있다.
    private void Update()
    {
		// key가 눌렸을때 action이 있으면 실행
		foreach (KeyToAction key in Enum.GetValues(typeof(KeyToAction)))
		{
            if (!keyDictionary.ContainsKey(key))	// 이 키가 저장되어있는지
				continue;
			if (Input.GetKeyDown(keyDictionary[key]))
				keyAcitionDictionary[key]?.Invoke();
		}
    }

	static void Init()
	{
		if(instance == null)
		{
			GameObject obj = GameObject.Find("Manager");
			if(obj == null)
				obj = new GameObject("Manager");
			instance = obj.AddComponent<KeyManager>();

			instance.DefulatKeySetting();
		}
    }

    public void DefulatKeySetting()
	{
        keyData = Resources.Load("Data/KeySettingData") as KeySettingData;
        for (int i = 0; i < keyData.Default.Count; i++)
		{
			keyDictionary.Add(keyData.Default[i].Action, keyData.Default[i].Key);
			keyAcitionDictionary.Add(keyData.Default[i].Action, null);
        }
	}


    public void AddKeyAction(KeyToAction key, Action action)
	{
		if (!keyAcitionDictionary.ContainsKey(key))
		{
			keyAcitionDictionary.Add(key, null);
			keyDictionary.Add(key, KeyCode.None);

			Debug.Log($"{key}의 key가 정해지지 않았습니다.");
		}

		keyAcitionDictionary[key] -= action;
		keyAcitionDictionary[key] += action;
    }

    public void RemoveKeyAction(KeyToAction key, Action action)
	{
		keyAcitionDictionary[key] -= action;
    }
}