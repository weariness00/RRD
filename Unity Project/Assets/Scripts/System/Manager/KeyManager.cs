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
/// Ű ������ �ص� �Ȱ��� �ൿ(Action)�� ������ �ϱ�
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
		// key�� �������� action�� ������ ����
		foreach (KeyToAction key in Enum.GetValues(typeof(KeyToAction)))
		{
			if (!keyDictionary.ContainsKey(key))	// �� Ű�� ����Ǿ��ִ���
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

			Debug.Log($"{key}�� key�� �������� �ʾҽ��ϴ�.");

		}

		keyAcitionDictionary[key] -= action;
		keyAcitionDictionary[key] += action;
    }

    public void RemoveKeyAction(KeyToAction key, Action action)
	{
		keyAcitionDictionary[key] -= action;
    }
}