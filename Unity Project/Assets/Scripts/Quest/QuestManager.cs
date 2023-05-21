using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum QuestAction
{
	Kill,
	Dead,

	Monster,
}

public class QuestManager : MonoBehaviour
{
	public List<Quest> questList;

	public void AddQuest(Quest node)
	{
		Array.Sort(node.Progress);
		questList.Add(node);

        questList.OrderBy(q => q.id);
    }

	public void SendQeustEvent(QuestAction[] actions)
	{
		questList.ForEach(quest => { quest.CheckProgress(actions); });
    }

	public void CreateObject(GameObject obj)
	{
		Instantiate(obj);
	}
}
