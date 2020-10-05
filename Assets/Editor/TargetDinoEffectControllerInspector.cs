﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TargetDinoEffectController))]
[CanEditMultipleObjects]
public class TargetDinoEffectControllerInspector : DinoEffectControllerInspector
{
    SerializedProperty selectedBone, followTargetBone;
    protected override void OnEnable()
    {
        base.OnEnable();
        selectedBone = serializedObject.FindProperty("selectedBone");
        followTargetBone = serializedObject.FindProperty("followTargetBone");
    }

    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(selectedBone);
        bool changedBone = EditorGUI.EndChangeCheck();
        if(changedBone)
            serializedObject.ApplyModifiedProperties();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(followTargetBone);
        bool changedFollowBone = EditorGUI.EndChangeCheck();
        if(changedFollowBone)
            serializedObject.ApplyModifiedProperties();
        
    }
}
