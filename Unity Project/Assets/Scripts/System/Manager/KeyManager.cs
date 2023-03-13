using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 키 변경을 해도 똑같은 행동이 나오게 하기
/// </summary>
public class KeyManager : UIUtil
{
	[HideInInspector]public Dictionary<KeyCode, Action> keyActionDictionary = new Dictionary<KeyCode, Action>();

    // 변경할 키의 InputFiled컴포넌트를 가지고 있느 오브젝트의 이름
    enum KeySetting
    {
        Move_Front,
        Move_Back,
    }

    private void Start()
    {
        // 미완성 지금 AddLitener가 안됨
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