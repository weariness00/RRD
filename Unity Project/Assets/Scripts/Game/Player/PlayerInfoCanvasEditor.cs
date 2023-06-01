using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static PlayerInfoCanvas;

[CustomEditor(typeof(PlayerInfoCanvas), true)]
public class PlayerInfoCanvasEditor : Editor
{
    SerializedProperty CanvasType;
    SerializedProperty Player;
    SerializedProperty HP_Slider;
    SerializedProperty MP_Slider;
    SerializedProperty EXP_Slider;
    SerializedProperty HP_Value;
    SerializedProperty MP_Value;
    SerializedProperty EXP_Value;
    SerializedProperty Level_Value;

    SerializedProperty Skill_NormalAttackNode;
    SerializedProperty Skill_EnhanceAttackNode;
    SerializedProperty Skill_AuxiliaryNode;
    SerializedProperty Skill_UltimateNode;

    public void OnEnable()
    {
        CanvasType = serializedObject.FindProperty("CanvasType");
        Player = serializedObject.FindProperty("player");
        HP_Slider = serializedObject.FindProperty("hp_Slider");
        MP_Slider = serializedObject.FindProperty("mp_Slider");
        EXP_Slider = serializedObject.FindProperty("exp_Slider");
        HP_Value = serializedObject.FindProperty("hp_Value");
        MP_Value = serializedObject.FindProperty("mp_Value");
        EXP_Value = serializedObject.FindProperty("exp_Value");
        Level_Value = serializedObject.FindProperty("level_Value");

        Skill_NormalAttackNode = serializedObject.FindProperty("Skill_NormalAttackNode");
        Skill_EnhanceAttackNode = serializedObject.FindProperty("Skill_EnhanceAttackNode");
        Skill_AuxiliaryNode = serializedObject.FindProperty("Skill_AuxiliaryNode");
        Skill_UltimateNode = serializedObject.FindProperty("Skill_UltimateNode");
    }


    public override void OnInspectorGUI()
    {
        PlayerInfoCanvas playerInfoCanavs = (PlayerInfoCanvas)target;

        EditorGUILayout.PropertyField(CanvasType);
        EditorGUILayout.PropertyField(Player);
        GUILayout.Space(20);

        switch (playerInfoCanavs.CanvasType)
        {
            case PlayerInfoCanvas.Type.Status:
                EditorGUILayout.PropertyField(HP_Slider);
                EditorGUILayout.PropertyField(HP_Value);
                GUILayout.Space(10);

                EditorGUILayout.PropertyField(MP_Slider);
                EditorGUILayout.PropertyField(MP_Value);

                GUILayout.Space(10);
                EditorGUILayout.PropertyField(EXP_Slider);
                EditorGUILayout.PropertyField(EXP_Value);
                EditorGUILayout.PropertyField(Level_Value);
                break;
            case PlayerInfoCanvas.Type.Skill:
                EditorGUILayout.PropertyField(Skill_NormalAttackNode );
                EditorGUILayout.PropertyField(Skill_EnhanceAttackNode);
                EditorGUILayout.PropertyField(Skill_AuxiliaryNode);
                EditorGUILayout.PropertyField(Skill_UltimateNode);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
