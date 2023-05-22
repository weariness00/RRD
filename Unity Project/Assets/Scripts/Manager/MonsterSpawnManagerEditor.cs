using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(MonsterSpawnManager))]
public class MonsterSpawnManagerEditor : Editor
{
    MonsterSpawnManager Instance;
    SerializedProperty _SpawnPoolParant;
    SerializedProperty _Type;
    SerializedProperty _SpawnPositionType;

    SerializedProperty _IsTarget;
    SerializedProperty _SpawnPosition;
    SerializedProperty _SpawnTarget;
    SerializedProperty _SpawnMinDistance;
    SerializedProperty _SpawnMaxDistance;
    SerializedProperty _MaxAliveMonsterCount;
    SerializedProperty _SpawnSpot_Object;

    SerializedProperty _WaveNode;
    private void OnEnable()
    {
        Instance = (MonsterSpawnManager)target;
        _Type = serializedObject.FindProperty("type");
        _SpawnPoolParant = serializedObject.FindProperty("spawnPoolParant");
        _MaxAliveMonsterCount = serializedObject.FindProperty("maxAliveMonsterCount");
        _SpawnPositionType = serializedObject.FindProperty("SpawnPositionType");

        _IsTarget = serializedObject.FindProperty("isTarget");
        _SpawnPosition = serializedObject.FindProperty("spawnPosition");
        _SpawnTarget = serializedObject.FindProperty("spawnTarget");
        _SpawnMinDistance = serializedObject.FindProperty("spawnMinDistance");
        _SpawnMaxDistance = serializedObject.FindProperty("spawnMaxDistance");
        _SpawnSpot_Object = serializedObject.FindProperty("spawnSpot_Object");

        _WaveNode = serializedObject.FindProperty("waveNode");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_Type);
        EditorGUILayout.PropertyField(_SpawnPoolParant);
        EditorGUILayout.PropertyField(_MaxAliveMonsterCount);
        EditorGUILayout.PropertyField(_SpawnPositionType);
        EditorGUILayout.Space();

        switch(Instance.type)
        {
            case MonsterSpawnType.Distance:
                Distance();
                break;
            case MonsterSpawnType.Spot:
                Spot();
                break;
        }

        EditorGUILayout.PropertyField(_WaveNode);

        serializedObject.ApplyModifiedProperties();
    }

    void Distance()
    {
        EditorGUILayout.PropertyField(_IsTarget);
        if (_IsTarget.boolValue) EditorGUILayout.PropertyField(_SpawnTarget);
        else EditorGUILayout.PropertyField(_SpawnPosition);
        EditorGUILayout.PropertyField(_SpawnMinDistance);
        EditorGUILayout.PropertyField(_SpawnMaxDistance);
    }
    
    void Spot()
    {
        EditorGUILayout.PropertyField(_SpawnSpot_Object);
    }
}
