using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterList))]
public class MonsterListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() ���� ���ʹ� GUI ���� ���η� �����˴ϴ�.
        GUILayout.FlexibleSpace();

        MonsterList monsterListScript = (MonsterList)target;

        if (GUILayout.Button("Init List", GUILayout.Width(100), GUILayout.Height(25)))
        {
            monsterListScript.InitList();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
