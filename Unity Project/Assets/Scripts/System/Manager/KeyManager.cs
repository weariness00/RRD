using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Ű ������ �ص� �Ȱ��� �ൿ�� ������ �ϱ�
/// </summary>
public class KeyManager : UIUtil
{
	[HideInInspector]public Dictionary<KeyCode, Action> keyActionDictionary = new Dictionary<KeyCode, Action>();

    // ������ Ű�� InputFiled������Ʈ�� ������ �ִ� ������Ʈ�� �̸�
    enum KeySetting
    {
        Move_Front,
        Move_Back,
    }

    private void Start()
    {
        // �̿ϼ� ���� AddLitener�� �ȵ�
        Bind<InputField>(typeof(KeySetting));

        foreach (int keyIndex in Enum.GetValues(typeof(KeySetting)))
        {
            InputField inputFiled = GetInputFiled(keyIndex);
            inputFiled?.onValueChanged.AddListener(OnChangeKey);
        }
    }

    public void OnChangeKey(string text)
    {
        SetKeyEvent((KeyCode)int.Parse(text), () => { Debug.Log("Click"); });
    }

    public void SetKeyEvent(KeyCode keyCode, Action action)
	{
		keyActionDictionary.Remove(keyCode);
		keyActionDictionary[keyCode] = action;
	}

}