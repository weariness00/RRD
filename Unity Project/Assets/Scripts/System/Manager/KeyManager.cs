using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    KeyManager,

    Skill_Q,
}

/// <summary>
/// 키 변경을 해도 똑같은 행동(Action)이 나오게 하기
/// </summary>
public class KeyManager : UIUtil
{
    public GameObject keyObject;
	public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();

    public KeyManager(GameObject keyobj)
    {
        keyObject = keyobj;
        Managers.Instance.StartCall += DefulatKeySetting;
        Managers.Instance.OnGUICall += OnGUI;
        Managers.Instance.UpdateCall += OnOff;
    }

    public void OnOff()
    {
        if (Input.GetKeyDown(InputAction(KeyToAction.KeyManager)))
            keyObject.SetActive(!keyObject.activeSelf);
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
        keyObject.SetActive(true);

        GameObject keyFieldObject = Resources.Load("Prefabs/UI/KeyField") as GameObject;
        KeySettingData keyData = Resources.Load("Data/KeySettingData") as KeySettingData;

        GameObject scrollContants = GameObject.Find("KeyScrollView").transform.GetChild(0).gameObject;  // 맵핑을 하기위한 오브젝트 로드

        for (int i = 0; i < keyData.Default.Count; i++)
        {
            keyDictionary.Add(keyData.Default[i].Action, keyData.Default[i].Key);

            GameObject obj = Util.Instantiate(keyFieldObject, scrollContants.transform);
            obj.name = keyData.Default[i].Action.ToString();
            obj.transform.GetChild(0).name = obj.name;

            Bind<TMP_Text>(obj, new string[] { "Action" });	// 맵핑
            Bind<TMP_Text>(obj, new string[] { "Key" });	// 맵핑
            Get<TMP_Text>(i * 2).text = keyData.Default[i].Action.ToString();   
            Get<TMP_Text>(i * 2 + 1).text = keyData.Default[i].Key.ToString();
        }
		Bind<Button>(scrollContants, typeof(KeyToAction));  // 맵핑

        for (int i = 0; i < keyData.Default.Count; i++)
        {
            int temp = i;
            Button button = Get<Button>(i);
            button.onClick.AddListener(() => OnKeyIndex(temp));
            button.onClick.AddListener(() => OnKeyButton(button.GetComponentInChildren<TMP_Text>()));
        }

        keyObject.SetActive(false);
    }

    public KeyCode InputAction(KeyToAction action) { return keyDictionary[action]; }

    /// <summary>
    /// 버튼을 눌렀을때 어떤 버튼을 눌렀는지에 대한 메서드 index는 KeyToAction을 정수로 치환한 것이다.
    /// </summary>
    int currentKeyIndex = -1;
    TMP_Text buttonText = null;
    public void OnKeyIndex(int keyIndex) { currentKeyIndex = keyIndex; Debug.Log(keyIndex); }
    public void OnKeyButton(TMP_Text text) { buttonText = text; }
}