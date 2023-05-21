using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Quest Node", menuName = "Scriptable Object/Quest", order = int.MaxValue)]
public class Quest : ScriptableObject
{
    public int id;

    public string title;
    public string text;

    int golaCount = 1;
    int count = 0;
	bool isScessce = false;

    public QuestAction[] Progress;
    public UnityEvent RewardEvent;
    // ���൵ �˷��ִ� ����

    public void CheckProgress(QuestAction[] actions)
    {
        if (!actions.Length.Equals(Progress.Length)) return;
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i] != Progress[i]) return;
        }
        Debug.Log($"{title} ����Ʈ ������");
        
        if(golaCount.Equals(++count)) isScessce = true;
        Scessce();
    }

    public void Scessce()
    {
        if (!isScessce) return;
        RewardEvent?.Invoke();
        Debug.Log("���� ����");
    }
}