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
/// 키 변경을 해도 똑같은 행동(Action)이 나오게 하기
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

    // Init은 제대로 해주나
    // Update 구문에 들어오면 Dictionary 멤버변수들이 초기화 되고있다.
    //  private void Update()
    //  {
    //// key가 눌렸을때 action이 있으면 실행
    //foreach (KeyToAction key in Enum.GetValues(typeof(KeyToAction)))
    //{
    //          if (!keyDictionary.ContainsKey(key))	// 이 키가 저장되어있는지
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
		GameObject scrollContants = GameObject.Find("KeyScrollView").transform.GetChild(0).gameObject;	// 맵핑을 하기위한 오브젝트 로드
        
        for (int i = 0; i < keyData.Default.Count; i++)
		{
			keyDictionary.Add(keyData.Default[i].Action, keyData.Default[i].Key);
			//keyDictionary.Add(keyData.Default[i].Action, keyData.Default[i].Key);
			//keyAcitionDictionary.Add(keyData.Default[i].Action, null);

			GameObject obj = Resources.Load("Prefabs/UI/KeyField") as GameObject;
			obj.name = Enum.GetName(typeof(KeyToAction), keyData.Default[i].Action);
			obj.transform.GetChild(0).name = obj.name;
            Instantiate(obj, scrollContants.transform);

            Bind<TMP_Text>(obj, new string[] { "Action" });	// 맵핑
        }
		Bind<Button>(scrollContants, typeof(KeyToAction));  // 맵핑


        // 이부분의 AddListener이 안됨
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

    //        Debug.Log($"{key}의 key가 정해지지 않았습니다.");
    //    }

    //    keyAcitionDictionary[key] -= action;
    //    keyAcitionDictionary[key] += action;
    //}

    //public void RemoveKeyAction(KeyToAction key, Action action)
    //{
    //    keyAcitionDictionary[key] -= action;
    //}


    /// <summary>
    /// 버튼을 눌렀을때 어떤 버튼을 눌렀는지에 대한 메서드 index는 KeyToAction을 정수로 치환한 것이다.
    /// </summary>
    int currentKeyIndex = -1;
    public void OnKeyIndex(int keyIndex) { currentKeyIndex = keyIndex; }

    // 버튼을 눌렀을때 코루틴을 돌며 input 버퍼가 있는지 탐색
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