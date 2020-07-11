using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(InputManager))]
public class InputManagerEditor : Editor {

    #region Gui Variables
    float ScreenWidth;
    float paddingT;
    float paddingS;
    float LabelT;
    float labelS;
    float TogglsT;
    float ToggleS;
    float FloatT;
    float FloatS;
    float LableSpacingT;
    float LableSpacingS;
    float SpacingT;
    float SpacingS;
    float TextT;
    float TextS;
    #endregion

    public bool ShowKBMInput;
    public bool Show360Input;
    public bool[] AxisFoldouts = new bool[20];
    public int AxisIndex;
    GUIStyle mystyle;

    public override void OnInspectorGUI()
    {
        InputManager M = (InputManager)target;

        mystyle = new GUIStyle("Toolbar");

        AxisIndex = 0;
        DrawNodes();
        GUILayouts();

        #region GUI change
        base.OnInspectorGUI();

        EditorUtility.SetDirty(M);

        #endregion
    }

    void DrawNodes()
    {
        InputManager M = (InputManager)target;

        ShowKBMInput = EditorGUILayout.Foldout(ShowKBMInput, "Show KBM Input Nodes");

        #region KBM Nodes
        if (ShowKBMInput)
        {
            #region Header
            EditorGUILayout.BeginHorizontal(mystyle);
            EditorGUILayout.LabelField("Keyboard and Mouse Binds");
            EditorGUILayout.EndHorizontal();
            DrawLabels();
            #endregion 

            #region draw buttons
            for (int i = 0; i < M.Nodes.Count; i++)
            {
                if(M.Nodes[i].myBind.Scheme == CreateKeybinds.ControllerType.Keyboard)
                {
                    if (M.Nodes[i].myBind.Key != KeyCode.None)
                    {
                        DrawKeys(M.Nodes[i]);
                    }
                }
            }
            #endregion

            #region draw axis
            for (int i = 0; i < M.Nodes.Count; i++)
            {
                if (M.Nodes[i].myBind.Scheme == CreateKeybinds.ControllerType.Keyboard)
                {
                    if (M.Nodes[i].myBind.Key == KeyCode.None)
                    {
                        DrawAxis(M.Nodes[i]);
                    }
                }
            }
            #endregion
        }
        #endregion

        Show360Input = EditorGUILayout.Foldout(Show360Input, "Show 360 Input Nodes");

        #region xbox controller nodes
        if (Show360Input)
        {
            #region header
            EditorGUILayout.BeginHorizontal(mystyle);
            EditorGUILayout.LabelField("Xbox Controller Binds");
            EditorGUILayout.EndHorizontal();
            DrawLabels();
            #endregion

            #region draw buttons
            for (int i = 0; i < M.Nodes.Count; i++)
            {
                if(M.Nodes[i].myBind.Scheme == CreateKeybinds.ControllerType.Controller)
                {
                    if (M.Nodes[i].myBind.ButtonCode != KeyCode.None)
                    {
                        DrawKeys(M.Nodes[i]);
                    }
                }
            }
            #endregion

            #region draw axis
            for (int i = 0; i < M.Nodes.Count; i++)
            {
                if (M.Nodes[i].myBind.Scheme == CreateKeybinds.ControllerType.Controller)
                {
                    if (M.Nodes[i].myBind.ButtonCode == KeyCode.None)
                    {
                        DrawAxis(M.Nodes[i]);
                    }
                }
            }
            #endregion
        }
        #endregion

    }

    void DrawLabels()
    {
        InputManager M = (InputManager)target;

        #region Draw Labels
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(paddingS));
        EditorGUILayout.LabelField("Key/Axis", GUILayout.MaxWidth(labelS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(LableSpacingS));
        EditorGUILayout.LabelField("Active", GUILayout.MaxWidth(labelS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(LableSpacingS));
        EditorGUILayout.LabelField("Value", GUILayout.MaxWidth(labelS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(LableSpacingS));
        EditorGUILayout.LabelField("Duration", GUILayout.MaxWidth(labelS));
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    void DrawKeys(InputManager.InputNode Node)
    {
        InputManager M = (InputManager)target;

        #region Draw Key Codes
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField(Node.Name, GUILayout.MaxWidth(TextS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));
        EditorGUILayout.Toggle(Node.Active, GUILayout.MaxWidth(ToggleS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));
        EditorGUILayout.FloatField(Node.myValue, GUILayout.MaxWidth(FloatS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));
        EditorGUILayout.FloatField(Node.Duration, GUILayout.MaxWidth(FloatS));
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    void DrawAxis(InputManager.InputNode Node)
    {
        InputManager M = (InputManager)target;

        #region Draw Axis

        #region header
        EditorGUILayout.BeginHorizontal();
        AxisFoldouts[AxisIndex] = EditorGUILayout.Toggle(AxisFoldouts[AxisIndex], GUILayout.MaxWidth(8));
        // Name
        EditorGUILayout.LabelField(Node.Name, GUILayout.MaxWidth(TextS - 12f));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));
        // Active
        EditorGUILayout.Toggle(Node.Active, GUILayout.MaxWidth(ToggleS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));
        // Value
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(FloatS));
        EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));
        // Duration
        EditorGUILayout.FloatField(Node.Duration, GUILayout.MaxWidth(FloatS));
        EditorGUILayout.EndHorizontal();
        #endregion

        if (AxisFoldouts[AxisIndex])
        {
            #region first Axis
            EditorGUILayout.BeginHorizontal();
            // Name
            EditorGUILayout.TextField(Node.AxisNameH, GUILayout.MaxWidth(TextS));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));

            // Active
            EditorGUILayout.Toggle(Node.ActiveH, GUILayout.MaxWidth(ToggleS));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));

            // Value
            EditorGUILayout.FloatField(Node.AxisValueH, GUILayout.MaxWidth(FloatS));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));

            EditorGUILayout.EndHorizontal();
            #endregion

            #region Second Axis
            EditorGUILayout.BeginHorizontal();
            // Name
            EditorGUILayout.TextField(Node.AxisNameV, GUILayout.MaxWidth(TextS));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));
            // Active
            EditorGUILayout.Toggle(Node.ActiveV, GUILayout.MaxWidth(ToggleS));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));

            // Value
            EditorGUILayout.FloatField(Node.AxisValueV, GUILayout.MaxWidth(FloatS));
            EditorGUILayout.LabelField("", GUILayout.MaxWidth(SpacingS));

            EditorGUILayout.EndHorizontal();
            #endregion
        }
        AxisIndex++;

        #endregion
    }

    void GUILayouts()
    {
        ScreenWidth = Screen.width;
        paddingS = ScreenWidth * paddingT;
        labelS = ScreenWidth * LabelT;
        ToggleS = ScreenWidth * TogglsT;
        FloatS = ScreenWidth * FloatT;
        LableSpacingS = ScreenWidth * LableSpacingT;
        SpacingS = ScreenWidth * SpacingT;
        TextS = ScreenWidth * TextT;

      //TextT = EditorGUILayout.Slider("Text Size", TextT, 0, 1);
      //SpacingT = EditorGUILayout.Slider("Spacing", SpacingT, 0, 1);
      //LableSpacingT = EditorGUILayout.Slider("Label Spacing", LableSpacingT, 0, 1);
      //FloatT = EditorGUILayout.Slider("Float Size", FloatT, 0, 1);
      //TogglsT = EditorGUILayout.Slider("Toggle Size", TogglsT, 0, 1);
      //LabelT = EditorGUILayout.Slider("Lable Size", LabelT, 0, 1);
      //paddingT = EditorGUILayout.Slider("Padding", paddingT, 0, 1);

        TextT = 0.28f;
        SpacingT = 0.057f;
        LableSpacingT = 0.074f;
        FloatT = 0.15f;
        TogglsT = 0.073f;
        LabelT = 0.132f;
        paddingT = 0.079f;

    }

    //needed to refresh the editor pannel even when not in focus.
    void OnSelectionChanged()
    {
        InputManager M = (InputManager)target;
        DrawNodes();
        Repaint();
        EditorUtility.SetDirty(M);
    }
}
