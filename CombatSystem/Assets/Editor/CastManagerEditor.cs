using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CastManager))]
public class CastManagerEditor : Editor {

    float End;
    float Current;
    float fraction;
    string Text;


    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        CastManager myCastManger = (CastManager)target;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Is Currently Casting");
        myCastManger.IsCasting = EditorGUILayout.Toggle(myCastManger.IsCasting, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Successful Cast");
        myCastManger.Success = EditorGUILayout.Toggle(myCastManger.Success, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cast Interrupted");
        myCastManger.Interruped = EditorGUILayout.Toggle(myCastManger.Interruped, GUILayout.MaxWidth(64));
        EditorGUILayout.EndHorizontal();

        if (myCastManger.IsCasting)
        {
            End = myCastManger.EndTime - myCastManger.StartTime;
            Current = myCastManger.CurTime - myCastManger.StartTime;
            fraction = Current / End;
            Text = myCastManger.Name;
        }
        else
        {
            fraction = 0;
        }


        if (myCastManger.Interruped == true)
        {
            Text = "Interrupted!";
        }

        Rect r = EditorGUILayout.BeginVertical();
        EditorGUI.ProgressBar(r, fraction, Text);
        GUILayout.Space(16);
        EditorGUILayout.EndVertical();

    }

}
