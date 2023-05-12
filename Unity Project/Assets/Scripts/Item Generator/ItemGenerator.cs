using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
        //scriptableObject.AbilityCall.AddListener(() => { Critical(itemStatus, 1.0f); }); // �ӽ�

        string curPath = $"{path}/{currentItemName}.asset";

        // ��ũ���ͺ� ������Ʈ ����
        AssetDatabase.CreateAsset(scriptableObject, curPath);

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