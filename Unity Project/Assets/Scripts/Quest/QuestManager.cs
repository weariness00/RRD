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

public class Quest_UI : UIUtil
{
    int nodeLenth = 0;
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

}

public class QuestManager : MonoBehaviour
{
	static public QuestManager Instance { get; private set; }

    Quest_UI ui = new Quest_UI();

    public Transform NodeParentTransform;
    public GameObject uiNode;

    public List<Quest> questList;

    private void Awake()
    {
		Instance = this;

		foreach (var quest in questList)
		{
            GameObject uiObj = Instantiate(uiNode, NodeParentTransform);
            ui.AddNode(uiObj, quest);
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

    public void AddQuest(Quest node)
	{
		Array.Sort(node.Progress);
		questList.Add(node);

        questList.OrderBy(q => q.id);

		GameObject uiObj = Instantiate(uiNode, NodeParentTransform);
		ui.AddNode(uiObj, node);
    }

	public void SendQeustEvent(QuestAction[] actions)
	{
		Array.Sort(actions);

		int count = 0;
		questList.ForEach(quest => {
			if (!quest.CheckProgress(actions)) return;
            ui.UpdateNode(quest, count);
			++count;
        });
    }

	public void CreateObject(GameObject obj)
	{
		Instantiate(obj);
	}
}
