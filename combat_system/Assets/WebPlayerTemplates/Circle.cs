using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DrawRadar))]
public class Circle : Editor
{
    private void OnSceneGUI()
    {
        DrawRadar myScript = (DrawRadar)target;

        
        if (myScript.Handles)
        {
            Quaternion handleRotation0 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
            Handles.DoPositionHandle(myScript.Center, handleRotation0);
            EditorGUI.BeginChangeCheck();
            Vector3 p0 = Handles.DoPositionHandle(myScript.Center, handleRotation0);
            if (EditorGUI.EndChangeCheck())
            {
                myScript.Center = p0;
            }
        }

        Handles.color = myScript.myColor;
        Handles.SphereCap(0, myScript.Center, Quaternion.identity, 1.5f);

        myScript.Theta = 0f;

        for (float i = 0; i < myScript.ThetaScale; i++)
        {
            myScript.Theta += (2.0f * Mathf.PI * myScript.ThetaScale);
            float x = myScript.radius * Mathf.Cos(myScript.Theta);
            float y = myScript.radius * Mathf.Sin(myScript.Theta);
            Handles.SphereCap(0, new Vector3(x,y,0) + myScript.Center, Quaternion.identity, 1.5f);
        }





        if (GUI.changed)
        {
            EditorUtility.SetDirty(myScript);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
