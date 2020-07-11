using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ViewSpline))]
public class VSeditor : Editor {

    private void OnSceneGUI()
    {
        ViewSpline VS = (ViewSpline)target;

        if(VS.Spline != null)
        {
            Handles.DrawBezier(VS.Spline.Points[0], VS.Spline.Points[3], VS.Spline.Points[1], VS.Spline.Points[2], Color.red, null, 1f);
        }
        
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
    }


}
