using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterList))]
public class MonsterListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
        GUILayout.FlexibleSpace();

        MonsterList monsterListScript = (MonsterList)target;
        if (GUILayout.Button("Sort List", GUILayout.Width(100),GUILayout.Height(25)))
        {
            if(monsterListScript.monsterList.Count != monsterListScript.monsterData.data.Count)
            {
                Debug.LogError("Diffrent Number in List and data");
                return;
            }

            monsterListScript.SortMonsterList();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
