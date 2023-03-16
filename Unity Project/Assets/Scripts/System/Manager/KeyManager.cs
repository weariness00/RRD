using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
	static KeyManager instance = null;
	public static KeyManager Instance { get { return instance; } }

	public enum KeyToAction
	{
		MoveFront,
		MoveBack,
	}

	//[HideInInspector] public Dictionary<KeyToAction, Action> keyAcitionDictionary = new Dictionary<KeyToAction, Action>();
	//[HideInInspector] public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();
	[HideInInspector] public Dictionary<KeyToAction, KeyCode> keyDictionary = new Dictionary<KeyToAction, KeyCode>();

	[SerializeField] KeySettingData keyData = null;

    // Init�� ����� ���ֳ�
    // Update ������ ������ Dictionary ����������� �ʱ�ȭ �ǰ��ִ�.
    //  private void Update()
    //  {
    //// key�� �������� action�� ������ ����
    //foreach (KeyToAction key in Enum.GetValues(typeof(KeyToAction)))
    //{
    //          if (!keyDictionary.ContainsKey(key))	// �� Ű�� ����Ǿ��ִ���
    //		continue;
    //	if (Input.GetKeyDown(keyDictionary[key]))
    //		keyAcitionDictionary[key]?.Invoke();
    //}
    //  }

    private void Start()
    {
        keyData = Resources.Load("Data/KeySettingData") as KeySettingData;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            instance.DefulatKeySetting();
        }  
    }

    public void DefulatKeySetting()
	{
		GameObject scrollContants = GameObject.Find("KeyScrollView").transform.GetChild(0).gameObject;	// ������ �ϱ����� ������Ʈ �ε�
        
        for (int i = 0; i < keyData.Default.Count; i++)
		{
			keyDictionary.Add(keyData.Default[i].Action, keyData.Default[i].Key);
			//keyDictionary.Add(keyData.Default[i].Action, keyData.Default[i].Key);
			//keyAcitionDictionary.Add(keyData.Default[i].Action, null);

			GameObject obj = Resources.Load("Prefabs/UI/KeyField") as GameObject;
			obj.name = Enum.GetName(typeof(KeyToAction), keyData.Default[i].Action);
			obj.transform.GetChild(0).name = obj.name;
            Instantiate(obj, scrollContants.transform);

            Bind<TMP_Text>(obj, new string[] { "Action" });	// ����
        }
		Bind<Button>(scrollContants, typeof(KeyToAction));  // ����


        // �̺κ��� AddListener�� �ȵ�
        int keyIndex = 0;
        for (int i = 0; i < 2; i++)
        {
            int temp = keyIndex++;
            Button button = Get<Button>(i);
            button.onClick.AddListener(() => OnKeyIndex(temp));
            button.onClick.AddListener(() => OnKeyButton(button.GetComponentInChildren<TMP_Text>()));
        }
        //foreach (Button button in Gets<Button>())
        //{
        //    button.onClick.AddListener(() => OnKeyButton(button.GetComponentInChildren<TMP_Text>()));
        //    button.onClick.AddListener(() => OnKeyIndex(keyIndex++));
        //}
    }

    //public void AddKeyAction(KeyToAction key, Action action)
    //{
    //    if (!keyAcitionDictionary.ContainsKey(key))
    //    {
    //        keyAcitionDictionary.Add(key, null);
    //        keyDictionary.Add(key, KeyCode.None);

    //        Debug.Log($"{key}�� key�� �������� �ʾҽ��ϴ�.");
    //    }

    //    keyAcitionDictionary[key] -= action;
    //    keyAcitionDictionary[key] += action;
    //}

    //public void RemoveKeyAction(KeyToAction key, Action action)
    //{
    //    keyAcitionDictionary[key] -= action;
    //}


    /// <summary>
    /// ��ư�� �������� � ��ư�� ���������� ���� �޼��� index�� KeyToAction�� ������ ġȯ�� ���̴�.
    /// </summary>
    int currentKeyIndex = -1;
    public void OnKeyIndex(int keyIndex) { currentKeyIndex = keyIndex; }

    // ��ư�� �������� �ڷ�ƾ�� ���� input ���۰� �ִ��� Ž��
    public void OnKeyButton(TMP_Text text)
    {
        StopCoroutine(ChangeKey(text));
        StartCoroutine(ChangeKey(text));
    }

    IEnumerator ChangeKey(TMP_Text text)
	{
		KeyCode keyEvenet;
		while (true)
		{
			yield return null;
			keyEvenet = Event.current.keyCode;
            if (Enum.IsDefined(typeof(KeyCode), keyEvenet))
			{
                keyDictionary[(KeyToAction)currentKeyIndex] = keyEvenet;
                text.text = keyEvenet.ToString();

                currentKeyIndex = -1;
                break;
            }

            //if (Enum.IsDefined(typeof(KeyCode), keyEvenet))
            //{
            //	KeyToAction action;
            //	if (keyDictionary.ContainsKey(keyEvenet))
            //	{
            //		action = keyDictionary[keyEvenet];
            //		keyDictionary.Remove(keyEvenet);
            //                 keyDictionary.Add(keyEvenet, action);
            //           }
            //	break;
            //}
        }
	}
}