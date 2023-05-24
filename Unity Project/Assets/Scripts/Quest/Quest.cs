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

    public int golaCount = 1;
    public int count = 0;
	public bool isScessce = false;

    public QuestAction[] Progress;
    public UnityEvent RewardEvent;
    // ���൵ �˷��ִ� ����

    public bool CheckProgress(QuestAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
            if (actions[i] != Progress[i]) return false;
        Debug.Log($"{title} ����Ʈ ������");
        
        if(golaCount.Equals(++count)) isScessce = true;
        Scessce();

        return true;
    }

    public void Scessce()
    {
        if (!isScessce) return;
        RewardEvent?.Invoke();
        Debug.Log("���� ����");
    }
}