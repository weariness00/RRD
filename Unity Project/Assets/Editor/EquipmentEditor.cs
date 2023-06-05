using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Muryotaisu), true)]
public class EquipmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() ���� ���ʹ� GUI ���� ���η� �����˴ϴ�.
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
