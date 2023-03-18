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
}

/// <summary>
/// Ű ������ �ص� �Ȱ��� �ൿ(Action)�� ������ �ϱ�
/// </summary>
public class KeyManager : UIUtil
{
	static KeyManager instance = null;
	public static KeyManager Instance { get { return instance; } }

	[HideInInspector] public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();
    [SerializeField] GameObject keyFieldObject = null;

    [SerializeField] KeySettingData keyData = null;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            instance.DefulatKeySetting();
        }  
    }

    private void OnGUI()
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
		GameObject scrollContants = GameObject.Find("KeyScrollView").transform.GetChild(0).gameObject;  // ������ �ϱ����� ������Ʈ �ε�

        for (int i = 0; i < keyData.Default.Count; i++)
        {
            keyDictionary.Add(keyData.Default[i].Action, keyData.Default[i].Key);

            GameObject obj = Instantiate(keyFieldObject, scrollContants.transform);
            obj.name = keyData.Default[i].Action.ToString();
            obj.transform.GetChild(0).name = obj.name;

            Bind<TMP_Text>(obj, new string[] { "Action" });	// ����
            Bind<TMP_Text>(obj, new string[] { "Key" });	// ����
            Get<TMP_Text>(i * 2).text = keyData.Default[i].Action.ToString();   
            Get<TMP_Text>(i * 2 + 1).text = keyData.Default[i].Key.ToString();
        }
		Bind<Button>(scrollContants, typeof(KeyToAction));  // ����

        for (int i = 0; i < Enum.GetValues(typeof(KeyToAction)).Length; i++)
        {
            int temp = i;
            Button button = Get<Button>(i);
            button.onClick.AddListener(() => OnKeyIndex(temp));
            button.onClick.AddListener(() => OnKeyButton(button.GetComponentInChildren<TMP_Text>()));
        }
    }

    public KeyCode InputAction(KeyToAction action) { return keyDictionary[action]; }

    /// <summary>
    /// ��ư�� �������� � ��ư�� ���������� ���� �޼��� index�� KeyToAction�� ������ ġȯ�� ���̴�.
    /// </summary>
    int currentKeyIndex = -1;
    TMP_Text buttonText = null;
    public void OnKeyIndex(int keyIndex) { currentKeyIndex = keyIndex; Debug.Log(keyIndex); }
    // ��ư�� �������� �ڷ�ƾ�� ���� input ���۰� �ִ��� Ž��
    public void OnKeyButton(TMP_Text text) { buttonText = text; }
}