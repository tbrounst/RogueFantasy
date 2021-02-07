using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitBase))]
//[CanEditMultipleObjects]
public class UnitEditor : Editor
{
    SerializedProperty unitName;
    SerializedProperty image;
    SerializedProperty stats;
    SerializedProperty randomStats;
    SerializedProperty abilities;

    private void OnEnable()
    {
        unitName = serializedObject.FindProperty("nameOfUnit");
        image = serializedObject.FindProperty("image");
        stats = serializedObject.FindProperty("stats");
        randomStats = serializedObject.FindProperty("randomStats");
        abilities = serializedObject.FindProperty("potentialAbilities");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.PropertyField(unitName);
        EditorGUILayout.PropertyField(image);
        EditorGUILayout.PropertyField(stats);
        EditorGUILayout.PropertyField(randomStats);

        int sum = 0;
        foreach (int val in (target as UnitBase).stats) {
            sum += val;
        }
        sum += (target as UnitBase).randomStats;

        EditorGUILayout.LabelField($"Sum: {sum}");
        EditorGUILayout.PropertyField(abilities);
        serializedObject.ApplyModifiedProperties();
    }
}
