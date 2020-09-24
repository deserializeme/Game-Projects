using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(camera_look))]
public class look_editor : Editor
{

    public override void OnInspectorGUI()
    {
        camera_look my_look = (camera_look)target;
        serializedObject.Update();




        base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(my_look);
        }

    }
}
