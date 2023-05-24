using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Muryotaisu), true)]
public class EquipmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
        GUILayout.FlexibleSpace();

        Muryotaisu pc = ((Muryotaisu)target);

        if (GUILayout.Button("Create Waepon", GUILayout.Width(100), GUILayout.Height(25)))
        {
            pc.CreateWeapon();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
