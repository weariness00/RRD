using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Difficulty))]
public class DifficultyEditor : Editor
{
    SerializedProperty Path;
    SerializedProperty DifficultyData;
    SerializedProperty TimeView;

    private void OnEnable()
    {
        Path = serializedObject.FindProperty("path");
        DifficultyData = serializedObject.FindProperty("data");
        TimeView = serializedObject.FindProperty("timeView");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Difficulty difficulty = (Difficulty)target;
        if (difficulty.data.type == 0)
            EditorGUILayout.PropertyField(TimeView);

        EditorGUILayout.PropertyField(DifficultyData);
        if (difficulty.data.type != 0) CreatDifficultDataJson(difficulty);

        serializedObject.ApplyModifiedProperties();
    }

    void CreatDifficultDataJson(Difficulty difficulty)
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.PropertyField(Path);
        if (GUILayout.Button("Create Difficulty Json"))
        {
            JsonUtil.SaveFile(difficulty.data, Path.stringValue);
        }
    }
}
