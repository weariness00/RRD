using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;

public class ItemGenerator : MonoBehaviour
{
    static public ItemGenerator Instance;

    public ItemList itemListScript;
    public ItemGeneratorEditor editor;
    [Space]

    [HideInInspector] public Canvas canvas;
    public GameObject currentItemObject;
    public TMP_Text valueText;

    [Space]
    public TMP_InputField itemNameField;
    string currentItemName;

    [Space]
    public TMP_Dropdown tearDropdown;

    [Space]
    public string path = "Resources/Data/Item";


    private void Awake()
    {
        Instance = this;

        canvas = transform.root.GetComponent<Canvas>();
        itemNameField.onValueChanged.AddListener((text) => { currentItemName = text; });
    }

    public Action<ItemData> createCall;
    public void CreateItem()
    {
        ItemData scriptableObject = ScriptableObject.CreateInstance<ItemData>();

        scriptableObject.id = itemListScript.itemList.Count;
        createCall?.Invoke(scriptableObject);
        SelectTear(scriptableObject);

        Status itemStatus = Util.GetORAddComponet<Status>(scriptableObject.prefab);
        //scriptableObject.AbilityCall.AddListener(() => { Critical(itemStatus, 1.0f); }); // 임시

        string curPath = $"{path}/{currentItemName}.asset";

#if UNITY_EDITOR
        // 스크립터블 오브젝트 저장
        AssetDatabase.CreateAsset(scriptableObject, curPath);
#endif
        itemListScript.itemList.Add(scriptableObject);

        Debug.Log($"Create Item : {curPath}");
    }

    public void SelectGameObect(ItemData itemData)
    {

    }

    public void SelectTear(ItemData itemData)
    {
        itemData.tear = (Define.ItemTear)tearDropdown.value;
    }
}