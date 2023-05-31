using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

[System.Serializable]
public class KeyDataInfo
{
	public KeyCode Key;
	public KeyToAction Action;
}

[System.Serializable]
public enum KeyToAction
{
    MoveFront = 0,
    MoveBack,
    MoveLeft,
    MoveRight,

    Attack,
    Run,

    Inventory,
    Interact,

    Setting_UI,
    KeyManager,
    WaveGenerator_UI,
    Quest_UI,

    Skill_Q,
    Skill_E,
}

/// <summary>
/// 키 변경을 해도 똑같은 행동(Action)이 나오게 하기
/// </summary>
public class KeyManager : UIUtil
{
	public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();

    public KeyManager()
    {
        Managers.Instance.StartCall += DefulatKeySetting;
        Managers.Instance.OnGUICall += OnGUI;
    }

    public void OnGUI()
    {
        if (currentKeyIndex == -1)
            return;

        Event keyEvenet = Event.current;
        if (!keyEvenet.isKey)
            return;
        if (Enum.IsDefined(typeof(KeyCode), keyEvenet.keyCode))
        {
            keyDictionary[(KeyToAction)currentKeyIndex] = keyEvenet.keyCode;
            buttonText.text = keyEvenet.keyCode.ToString();

            Debug.Log($"{(KeyToAction)currentKeyIndex}의 키를 {keyEvenet.keyCode}로 설정");
            currentKeyIndex = -1;
        }
    }

    /// <summary>
    /// KeySettingData 엑셀에 저장된 Defulat Data를 불러온다.
    /// </summary>
    public void DefulatKeySetting()
	{
        GameObject keyFieldObject = Resources.Load("Prefabs/UI/KeyField") as GameObject;
        KeySettingData keyData = Resources.Load<ScriptableObject>("Data/Key/KeySettingData") as KeySettingData;

        //RectTransform viewRect = GameObject.Find("KeyScrollView").GetComponent<RectTransform>();
        ScrollRect viewRect = Util.FindChild(Util.FindChild(Managers.Instance.gameObject, "Setting Canvas"), "Key UI").GetComponent<ScrollRect>();
        RectTransform contentsRect = viewRect.content;  // 맵핑을 하기위한 오브젝트 로드

        for (int i = 0; i < keyData.Default.Count; i++)
        {
            KeyCode key = keyData.Default[i].Key;
            KeyToAction action = keyData.Default[i].Action;

            keyDictionary.Add(action, key);

            GameObject obj = Util.Instantiate(keyFieldObject, contentsRect.transform);
            obj.name = action.ToString();
            obj.transform.GetChild(0).name = obj.name;

            Bind<TMP_Text>(obj, new string[] { "Action" });	// 맵핑
            Bind<TMP_Text>(obj, new string[] { "Key" });	// 맵핑
            Get<TMP_Text>(i * 2).text = action.ToString();
            Get<TMP_Text>(i * 2 + 1).text = key.ToString();
            Bind<Button>(contentsRect.gameObject, new string[] { action.ToString() });  // 맵핑
        }

        // 버튼을 동적 할당 했기에 사이즈를 조정해줘야함
        contentsRect.sizeDelta = new Vector2(
            contentsRect.rect.width,
            (keyData.Default.Count + 1) * keyFieldObject.GetComponent<RectTransform>().sizeDelta.y - viewRect.GetComponent<RectTransform>().rect.height
            );

        for (int i = 0; i < keyData.Default.Count; i++)
        {
            int temp = i;
            Button button = Get<Button>(i);
            button.onClick.AddListener(() => OnKeyIndex(temp));
            button.onClick.AddListener(() => OnKeyButton(button.GetComponentInChildren<TMP_Text>()));
        }
    }

    public bool InputActionDown(KeyToAction action)
    {
        if (!keyDictionary.ContainsKey(action))
            return false;

        return Input.GetKeyDown(keyDictionary[action]);
    }

    public bool InputAction(KeyToAction action)
    {
        if (!keyDictionary.ContainsKey(action))
            return false;

        return Input.GetKey(keyDictionary[action]);
    }
    public bool InputAnyKey 
    {
        get
        {
            foreach (KeyCode key in keyDictionary.Values)
            {
                if (Input.GetKey(key))
                    return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 버튼을 눌렀을때 어떤 버튼을 눌렀는지에 대한 메서드 index는 KeyToAction을 정수로 치환한 것이다.
    /// </summary>
    int currentKeyIndex = -1;
    TMP_Text buttonText = null;
    public void OnKeyIndex(int keyIndex) { currentKeyIndex = keyIndex; Debug.Log(keyIndex); }
    public void OnKeyButton(TMP_Text text) { buttonText = text;}
}