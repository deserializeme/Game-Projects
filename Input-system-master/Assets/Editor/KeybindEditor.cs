using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using MovementEffects;



[CustomEditor(typeof(CreateKeybinds))]
public class KeybindEditor : Editor
{
    public enum ShemeToShow
    {
        Controller,
        KBM
    }
    public ShemeToShow ControlSheme;

    public override void OnInspectorGUI()
    {
        CreateKeybinds K = (CreateKeybinds)target;

        #region pupulate the Action list with all defined actions
        foreach (CreateKeybinds.Action action in System.Enum.GetValues(typeof(CreateKeybinds.Action)))
        {
            if (!K.Actions.Contains(action))
            {
                K.Actions.Add(action);
                CreateKeybinds.KeyBind KB1 = new CreateKeybinds.KeyBind();
                KB1.Name = action.ToString();
                KB1.Action = action;
                KB1.Scheme = CreateKeybinds.ControllerType.Controller;
                KB1.InputMethod = CreateKeybinds.InputType.Button;
                K.KeyBinds.Add(KB1);

                CreateKeybinds.KeyBind KB2 = new CreateKeybinds.KeyBind();
                KB2.Name = action.ToString();
                KB2.Action = action;
                KB2.Scheme = CreateKeybinds.ControllerType.Keyboard;
                KB2.InputMethod = CreateKeybinds.InputType.Key;
                K.KeyBinds.Add(KB2);
            }
        }
        #endregion

        #region remove old Actions
        for(int i = 0; i < K.Actions.Count; i++)
        {
            bool Defined = false;

            foreach (CreateKeybinds.Action action in System.Enum.GetValues(typeof(CreateKeybinds.Action)))
            {
                if(K.Actions[i] == action)
                {
                    Defined = true;
                }
            }

            if (!Defined)
            {
                for(int j = 0; j < K.KeyBinds.Count; j++)
                {
                    if(K.KeyBinds[j].Name == K.Actions[i].ToString())
                    {
                        K.KeyBinds.Remove(K.KeyBinds[j]);
                    }
                }
                K.Actions.Remove(K.Actions[i]);
            }

        }
        #endregion

        #region Draw GUI Elements

        #region Header
        GUIStyle style = new GUIStyle("Box");

        EditorGUILayout.BeginVertical(style);
        ControlSheme = (ShemeToShow)EditorGUILayout.EnumPopup("Control Scheme", ControlSheme);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("To add more Keybinds: add to the list of enums in CreateKeybinds.Action", MessageType.None);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        #endregion

        for (int i = 0; i < K.KeyBinds.Count; i++)
        {
            #region Display Controller Bindings;
            if (ControlSheme == ShemeToShow.Controller)
            {
                if(K.KeyBinds[i].Scheme == CreateKeybinds.ControllerType.Controller)
                {
                   
                    #region Axis setup
                    if (K.KeyBinds[i].InputMethod == CreateKeybinds.InputType.Axis)
                    {
                        #region variables
                        string[] options = K.JoyAxis;

                        int A = 0;
                        if (K.KeyBinds[i].AxisA != "")
                        {
                            A = System.Array.IndexOf(options, K.KeyBinds[i].AxisA);
                        }

                        int B = 0;
                        if (K.KeyBinds[i].AxisB != "")
                        {
                            B = System.Array.IndexOf(options, K.KeyBinds[i].AxisB);
                        }

                        bool DualAxis;
                        DualAxis = K.KeyBinds[i].Joystick;

                        GUIStyle mystyle = new GUIStyle("Toolbar");
                        #endregion

                        K.KeyBinds[i].Key = KeyCode.None;
                        K.KeyBinds[i].ButtonCode = KeyCode.None;

                        #region Name
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal(mystyle);
                        EditorGUILayout.LabelField(K.KeyBinds[i].Name);
                        EditorGUILayout.EndHorizontal();

                        #endregion
                        EditorGUILayout.BeginVertical(style);

                        K.KeyBinds[i].InputMethod = (CreateKeybinds.InputType)EditorGUILayout.EnumPopup("Input Type", K.KeyBinds[i].InputMethod);

                        #region Axis selection
                        K.KeyBinds[i].Joystick = EditorGUILayout.Toggle("Dual Axis", K.KeyBinds[i].Joystick);

                        A = EditorGUILayout.Popup("X Axis", A, options);
                        K.KeyBinds[i].AxisA = options[A];

                        if (K.KeyBinds[i].Joystick)
                        {
                            B = EditorGUILayout.Popup("Y Axis", B, options);
                            K.KeyBinds[i].AxisB = options[B];
                        }
                        #endregion

                        #region Dead Zone Slider
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Dead Zone", GUILayout.MaxWidth(128));
                        K.KeyBinds[i].DeadZone = EditorGUILayout.Slider(K.KeyBinds[i].DeadZone, 0, 1);
                        EditorGUILayout.EndHorizontal();
                        #endregion

                        #region Sensetivity slider
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Sensetivity", GUILayout.MaxWidth(128));
                        K.KeyBinds[i].Sensitivity = EditorGUILayout.Slider(K.KeyBinds[i].Sensitivity, 0.0f, 2f);
                        EditorGUILayout.EndHorizontal();
                        #endregion
                        EditorGUILayout.EndVertical();


                        EditorGUILayout.EndVertical();
                    }
                    #endregion

                    #region Button setup
                    if (K.KeyBinds[i].InputMethod == CreateKeybinds.InputType.Button)
                    {
                        #region Variables
                        GUIStyle mystyle = new GUIStyle("Toolbar");

                        string[] options = K.XboxButtons;
                        int B = 0;
                        if(K.KeyBinds[i].ButtonName != "")
                        {
                            B = System.Array.IndexOf(options, K.KeyBinds[i].ButtonName);
                        }

                        #endregion

                        K.KeyBinds[i].AxisA = "Cancel";
                        K.KeyBinds[i].AxisB = "Cancel";

                        #region Name
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal(mystyle);
                        EditorGUILayout.LabelField(K.KeyBinds[i].Name);
                        EditorGUILayout.EndHorizontal();
                        #endregion
                        EditorGUILayout.BeginVertical(style);

                        K.KeyBinds[i].InputMethod = (CreateKeybinds.InputType)EditorGUILayout.EnumPopup("Input Type", K.KeyBinds[i].InputMethod);

                        #region Button Select
                        B = EditorGUILayout.Popup("Button ", B, options);
                        K.KeyBinds[i].ButtonName = K.XboxButtons[B];
                        K.KeyBinds[i].ButtonCode = K.XboxKeyCodes[B];
                        #endregion
                        EditorGUILayout.EndVertical();
                    }
                    #endregion

                    #region Key setup
                    if (K.KeyBinds[i].InputMethod == CreateKeybinds.InputType.Key)
                    {
                        #region Variables
                        GUIStyle mystyle = new GUIStyle("Toolbar");
                        #endregion

                        K.KeyBinds[i].AxisA = "Cancel";
                        K.KeyBinds[i].AxisB = "Cancel";

                        #region Name
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal(mystyle);
                        EditorGUILayout.LabelField(K.KeyBinds[i].Name);
                        EditorGUILayout.EndHorizontal();
                        #endregion
                        EditorGUILayout.BeginVertical(style);

                        K.KeyBinds[i].InputMethod = (CreateKeybinds.InputType)EditorGUILayout.EnumPopup("Input Type", K.KeyBinds[i].InputMethod);
                        EditorGUILayout.LabelField("Dont use Keys for Controller Bindings! Use 'Button' or 'Axis'");
                        EditorGUILayout.EndVertical();
                    }
                    #endregion
                }
            }
            #endregion

            #region Display Keyboard and Mouse Bindings
            if (ControlSheme == ShemeToShow.KBM)
            {
                if(K.KeyBinds[i].Scheme == CreateKeybinds.ControllerType.Keyboard)
                {
                    #region Axis setup
                    if (K.KeyBinds[i].InputMethod == CreateKeybinds.InputType.Axis)
                    {
                        #region variables
                        string[] options = K.OtherAxis;

                        int A = 0;
                        if (K.KeyBinds[i].AxisA != "")
                        {
                            A = System.Array.IndexOf(options, K.KeyBinds[i].AxisA);
                        }

                        int B = 0;
                        if (K.KeyBinds[i].AxisB != "")
                        {
                            B = System.Array.IndexOf(options, K.KeyBinds[i].AxisB);
                        }

                        bool DualAxis;
                        DualAxis = K.KeyBinds[i].Joystick;

                        GUIStyle mystyle = new GUIStyle("Toolbar");
                        #endregion

                        K.KeyBinds[i].Key = KeyCode.None;
                        K.KeyBinds[i].ButtonCode = KeyCode.None;

                        #region Name
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal(mystyle);
                        EditorGUILayout.LabelField(K.KeyBinds[i].Name);
                        EditorGUILayout.EndHorizontal();
                        #endregion
                        EditorGUILayout.BeginVertical(style);

                        K.KeyBinds[i].InputMethod = (CreateKeybinds.InputType)EditorGUILayout.EnumPopup("Input Type", K.KeyBinds[i].InputMethod);

                        #region Axis selection
                        K.KeyBinds[i].Joystick = EditorGUILayout.Toggle("Dual Axis", K.KeyBinds[i].Joystick);

                        A = EditorGUILayout.Popup("X Axis", A, options);
                        K.KeyBinds[i].AxisA = options[A];

                        if (K.KeyBinds[i].Joystick)
                        {
                            B = EditorGUILayout.Popup("Y Axis", B, options);
                            K.KeyBinds[i].AxisB = options[B];
                        }
                        #endregion

                        #region Dead Zone Slider
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Dead Zone", GUILayout.MaxWidth(128));
                        K.KeyBinds[i].DeadZone = EditorGUILayout.Slider(K.KeyBinds[i].DeadZone, 0, 1);
                        EditorGUILayout.EndHorizontal();
                        #endregion

                        #region Sensetivity slider
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Sensetivity", GUILayout.MaxWidth(128));
                        K.KeyBinds[i].Sensitivity = EditorGUILayout.Slider(K.KeyBinds[i].Sensitivity, 0.0f, 2f);
                        EditorGUILayout.EndHorizontal();
                        #endregion


                        EditorGUILayout.EndVertical();
                    }
                    #endregion

                    #region Button setup
                    if (K.KeyBinds[i].InputMethod == CreateKeybinds.InputType.Button)
                    {
                        #region Variables
                        GUIStyle mystyle = new GUIStyle("Toolbar");
                        #endregion

                        K.KeyBinds[i].AxisA = "Cancel";
                        K.KeyBinds[i].AxisB = "Cancel";

                        #region Name
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal(mystyle);
                        EditorGUILayout.LabelField(K.KeyBinds[i].Name);
                        EditorGUILayout.EndHorizontal();
                        #endregion
                        EditorGUILayout.BeginVertical(style);

                        K.KeyBinds[i].InputMethod = (CreateKeybinds.InputType)EditorGUILayout.EnumPopup("Input Type", K.KeyBinds[i].InputMethod);
                        EditorGUILayout.LabelField("Dont use Buttons for Keybaord/Mouse Bindings! Use 'Key' or 'Axis'");
                        EditorGUILayout.EndVertical();
                    }
                    #endregion

                    #region Key setup
                    if (K.KeyBinds[i].InputMethod == CreateKeybinds.InputType.Key)
                    {
                        #region Variables
                        GUIStyle mystyle = new GUIStyle("Toolbar");
                        #endregion

                        K.KeyBinds[i].AxisA = "Cancel";
                        K.KeyBinds[i].AxisB = "Cancel";

                        #region Name
                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal(mystyle);
                        EditorGUILayout.LabelField(K.KeyBinds[i].Name);
                        EditorGUILayout.EndHorizontal();
                        #endregion
                        EditorGUILayout.BeginVertical(style);

                        K.KeyBinds[i].InputMethod = (CreateKeybinds.InputType)EditorGUILayout.EnumPopup("Input Type", K.KeyBinds[i].InputMethod);

                        K.KeyBinds[i].Key = (KeyCode)EditorGUILayout.EnumPopup("Key ", K.KeyBinds[i].Key);
                        EditorGUILayout.EndVertical();
                    }
                    #endregion
                }
            }
            #endregion

            EditorGUILayout.Space();
        }
        #endregion


        //base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(K);
        }
    }

}
