using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum QuestAction
{
	Kill,
	Dead,

	Monster,
}
public class QuestManager : UIUtil
{
	static public QuestManager Instance { get; private set; }
	public Transform NodeParentTransform;
    public GameObject uiNode;

    int nodeLenth = 0;
    public List<Quest> questList;

    private void Awake()
    {
		Instance = this;

		foreach (var item in questList)
		{
            GameObject uiObj = Instantiate(uiNode, NodeParentTransform);
            AddNode(uiObj, item);
        }
    }

    private void Start()
    {
		GameManager.Instance.UpdateCall.AddListener(OnOff);
    }

    void OnOff()
	{
		if (Managers.Key.InputActionDown(KeyToAction.Quest_UI)) 
			gameObject.SetActive(!gameObject.activeSelf);
	}

    public void AddNode(GameObject ui, Quest quest)
    {
        Bind<TMP_Text>(ui, new string[] { "Title" });
        Bind<TMP_Text>(ui, new string[] { "Text" });
        Bind<TMP_Text>(ui, new string[] { "Goal" });

        UpdateNode(quest, nodeLenth);
        ++nodeLenth;
    }

    public void UpdateNode(Quest quest, int index)
    {
        Get<TMP_Text>(index * 2).text = quest.title;
        Get<TMP_Text>(index * 2 + 1).text = quest.text;
        Get<TMP_Text>(index * 2 + 2).text = $"{quest.count} / {quest.golaCount}";
    }

    public void AddQuest(Quest node)
	{
		Array.Sort(node.Progress);
		questList.Add(node);

        questList.OrderBy(q => q.id);

		GameObject uiObj = Instantiate(uiNode, NodeParentTransform);
		AddNode(uiObj, node);
    }

	public void SendQeustEvent(QuestAction[] actions)
	{
		Array.Sort(actions);

		int count = 0;
		questList.ForEach(quest => {
			if (!quest.CheckProgress(actions)) return;
            UpdateNode(quest, count);
			++count;
        });
    }

	public void CreateObject(GameObject obj)
	{
		Instantiate(obj);
	}
}
