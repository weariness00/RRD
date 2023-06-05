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
    public void AddNode(GameObject ui, QuestData quest)
    {
        Bind<TMP_Text>(ui, new string[] { "Title" });
        Bind<TMP_Text>(ui, new string[] { "Text" });
        Bind<TMP_Text>(ui, new string[] { "Goal" });

        UpdateNode(quest, nodeLenth);
        ++nodeLenth;
    }

    public void UpdateNode(QuestData quest, int index)
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

    public List<QuestData> questList;

    private void Awake()
    {
		Instance = this;

        for (int i = 0; i < questList.Count; i++)
        {
            questList[i] = Instantiate(questList[i]);
            GameObject uiObj = Instantiate(uiNode, NodeParentTransform);
            ui.AddNode(uiObj, questList[i]);
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

    //public bool CheckProgress(QuestData data,QuestAction[] actions)
    //{
    //    for (int i = 0; i < actions.Length; i++)
    //        if (actions[i] != data.Progress[i]) return false;
    //    Debug.Log($"{data.title} 퀘스트 진행중");

    //    if (data.golaCount.Equals(++data.count)) data.isScessce = true;
    //    Scessce(data);

    //    return true;
    //}

    //public void Scessce(QuestData data)
    //{
    //    if (!data.isScessce) return;
    //    data.RewardEvent?.Invoke();
    //    Debug.Log("보상 지급");
    //}

    public void AddQuest(QuestData node)
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
}
