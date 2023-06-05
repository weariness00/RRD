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

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
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
