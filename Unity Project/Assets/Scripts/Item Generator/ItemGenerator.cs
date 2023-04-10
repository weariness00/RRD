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
    public ItemList itemListScript;

    public GameObject AbilityNodeObject;
    [Space]

    public GameObject currentItemObject;
    public TMP_Text valueText;

    [Space]
    public TMP_InputField itemNameField;
    string currentItemName;

    [Space]
    public Dropdown tearDropdown;

    public string path = "Resources/Data/Item";

    Image[] abilityNodes;

    private void Awake()
    {
        abilityNodes = Util.GetChildren<Image>(AbilityNodeObject);

        foreach (var abilityNode in abilityNodes)
        {
            Util.GetORAddComponet<CanvasGroup>(abilityNode.gameObject);
            Util.GetORAddComponet<AbilityNode>(abilityNode.gameObject).canvas = GetComponent<Canvas>();
        }

        itemNameField.onValueChanged.AddListener((text) => { currentItemName = text; });
    }

    public Action<Item> createCall;
    public void CreateItem()
    {
        Item scriptableObject = ScriptableObject.CreateInstance<Item>();

        scriptableObject.id = itemListScript.itemList.Count;
        createCall?.Invoke(scriptableObject);
        SelectTear(scriptableObject);

        Status itemStatus = Util.GetORAddComponet<Status>(scriptableObject.prefab);
        scriptableObject.AbilityCall.AddListener(() => { Critical(itemStatus, 1.0f); }); // 임시

        string curPath = $"{path}/{currentItemName}.asset";

        // 스크립터블 오브젝트 저장
        AssetDatabase.CreateAsset(scriptableObject, curPath);

        itemListScript.itemList.Add(scriptableObject);

        Debug.Log($"Create Item : {curPath}");
    }

    public void SelectGameObect(Item item)
    {

    }

    public void SelectTear(Item item)
    {
        item.tear = (Define.ItemTear)tearDropdown.value;
    }

    public void Critical(Status status, float value)
    {
        status.criticalProbability += value;
    }
}