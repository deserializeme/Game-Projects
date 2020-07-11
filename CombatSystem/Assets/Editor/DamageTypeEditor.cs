using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CreateNewDamageType))]
public class DamageTypeEditor : Editor {

    public override void OnInspectorGUI()
    {
        CreateNewDamageType myDamageType = (CreateNewDamageType)target;

        serializedObject.Update();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Resist"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ResistAmnt"), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add New Resistance"))
        {
            myDamageType.Resist.Add(new CreateNewDamageType());
            myDamageType.ResistAmnt.Add(new int());
        }

        if (GUILayout.Button("Remove Resistance"))
        {
            if (myDamageType.Resist.Count > 0)
            {
                myDamageType.Resist.Remove(myDamageType.Resist[myDamageType.Resist.Count - 1]);
                myDamageType.ResistAmnt.Remove(myDamageType.ResistAmnt[myDamageType.ResistAmnt.Count - 1]);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Weak"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("WeakAmnt"), true);
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();



        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add New Weakness"))
        {
            myDamageType.Weak.Add(new CreateNewDamageType());
            myDamageType.WeakAmnt.Add(new int());
        }

        if (GUILayout.Button("Remove Weakness"))
        {
            if (myDamageType.Weak.Count > 0)
            {
                myDamageType.Weak.Remove(myDamageType.Weak[myDamageType.Weak.Count - 1]);
                myDamageType.WeakAmnt.Remove(myDamageType.WeakAmnt[myDamageType.WeakAmnt.Count - 1]);
            }
        }


        EditorGUILayout.EndHorizontal();
        //base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(myDamageType);
        }
    }
}
