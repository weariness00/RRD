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
public class KeyManager : UIUtil
{
	public enum KeyToAction
	{
		MoveFront,
		MoveBack,
	}

	[HideInInspector] public Dictionary<KeyToAction, Action> keyAcitionDictionary = new Dictionary<KeyToAction, Action>();
	[HideInInspector] public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();

	public KeySettingData keyData;

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

    public void DefulatKeySetting()
	{
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