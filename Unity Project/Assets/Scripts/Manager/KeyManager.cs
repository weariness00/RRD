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

    Run,

    Attack,

    Inventory,

    KeyManager,
}

/// <summary>
/// Ű ������ �ص� �Ȱ��� �ൿ(Action)�� ������ �ϱ�
/// </summary>
public class KeyManager : UIUtil
{
    public GameObject keyObject;
	public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();

    public KeyManager()
    {
        keyObject = Util.FindChild(Managers.Instance.gameObject, "KeyManager");
        Managers.Instance.StartCall += DefulatKeySetting;
        Managers.Instance.OnGUICall += OnGUI;
        Managers.Instance.UpdateCall += OnOff;
    }

    public void OnOff()
    {
        if (InputAction(KeyToAction.KeyManager))
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

            Debug.Log($"{(KeyToAction)currentKeyIndex}�� Ű�� {keyEvenet.keyCode}�� ����");
            currentKeyIndex = -1;
        }
    }

    /// <summary>
    /// KeySettingData ������ ����� Defulat Data�� �ҷ��´�.
    /// </summary>
    public void DefulatKeySetting()
	{
        keyObject.SetActive(true);

        GameObject keyFieldObject = Resources.Load("Prefabs/UI/KeyField") as GameObject;
        KeySettingData keyData = Resources.Load("Data/KeySettingData") as KeySettingData;

        GameObject contents = GameObject.Find("KeyScrollView").transform.GetChild(0).GetChild(0).gameObject;  // ������ �ϱ����� ������Ʈ �ε�

        for (int i = 0; i < keyData.Default.Count; i++)
        {
            KeyCode key = keyData.Default[i].Key;
            KeyToAction action = keyData.Default[i].Action;

            keyDictionary.Add(action, key);

            GameObject obj = Util.Instantiate(keyFieldObject, contents.transform);
            obj.name = action.ToString();
            obj.transform.GetChild(0).name = obj.name;

            Bind<TMP_Text>(obj, new string[] { "Action" });	// ����
            Bind<TMP_Text>(obj, new string[] { "Key" });	// ����
            Get<TMP_Text>(i * 2).text = action.ToString();
            Get<TMP_Text>(i * 2 + 1).text = key.ToString();
            Bind<Button>(contents, new string[] { action.ToString() });  // ����
        }
        //Bind<Button>(contents, typeof(KeyToAction));  // ����

        for (int i = 0; i < keyData.Default.Count; i++)
        {
            int temp = i;
            Button button = Get<Button>(i);
            button.onClick.AddListener(() => OnKeyIndex(temp));
            button.onClick.AddListener(() => OnKeyButton(button.GetComponentInChildren<TMP_Text>()));
        }

        keyObject.SetActive(false);
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
    /// ��ư�� �������� � ��ư�� ���������� ���� �޼��� index�� KeyToAction�� ������ ġȯ�� ���̴�.
    /// </summary>
    int currentKeyIndex = -1;
    TMP_Text buttonText = null;
    public void OnKeyIndex(int keyIndex) { currentKeyIndex = keyIndex; Debug.Log(keyIndex); }
    public void OnKeyButton(TMP_Text text) { buttonText = text; }
}