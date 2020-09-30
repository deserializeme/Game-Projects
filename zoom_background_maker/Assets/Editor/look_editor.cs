using System.Collections;
using UnityEngine;
using UnityEditor;
using cam_manager;

[CustomEditor(typeof(camera_manager)), CanEditMultipleObjects]
public class look_editor : Editor
{
    protected virtual void OnSceneGUI()
    {
        camera_manager cm = (camera_manager)target;
        Handles.color = Color.blue;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 40;

        EditorGUI.BeginChangeCheck();
        Vector3 newCameraPosition = Handles.PositionHandle(cm.look.camera_position, Quaternion.identity);
        Handles.Label(cm.look.camera_position + Vector3.up * 2, "Camera", style);
        Handles.SphereHandleCap(0, newCameraPosition, Quaternion.identity, 1, EventType.Repaint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(cm, "Change Look At Target Position");
            cm.look.camera_position = newCameraPosition;
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(cm.look.camera_target, Quaternion.identity);
        Handles.Label(cm.look.camera_target + Vector3.up * 2, "Target", style);
        Handles.SphereHandleCap(1, newTargetPosition, Quaternion.identity, 1, EventType.Repaint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(cm, "Change Look At Target Position");
            cm.look.camera_target = newTargetPosition;
        }

        Handles.DrawLine(newCameraPosition, newTargetPosition);
    }
}