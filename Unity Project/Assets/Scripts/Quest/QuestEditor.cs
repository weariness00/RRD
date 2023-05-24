using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestManager), true)]
public class QuestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() ���� ���ʹ� GUI ���� ���η� �����˴ϴ�.
        GUILayout.FlexibleSpace();

        QuestManager questManager = ((QuestManager)target);

        if (GUILayout.Button("Send Quest Massage", GUILayout.Width(300), GUILayout.Height(25)))
        {
            questManager.SendQeustEvent(new QuestAction[] {});
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
