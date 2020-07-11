using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Keybind Profile", menuName = "New Keybind Profile", order = 1)]
[System.Serializable]
public class CreateKeybinds : ScriptableObject
{
    public enum ControllerType
    {
        Controller, Keyboard
    }

    public enum InputType
    {
        Key, Axis, Button
    }

    public enum Action
    {
        Jump,
        Sprint,
        RotateCamera,
        Forward,
        Back
    }

    [System.Serializable]
    public class KeyBind
    {
        public string Name;

        public ControllerType Scheme;
        public InputType InputMethod;
        public Action Action;

        //two axis because for each axis you need horizontal and vertical components
        public bool Joystick;
        public string AxisA;
        public string AxisB;
        public float DeadZone;

        [Range(0.01f, 2f)]
        public float Sensitivity;

        // key is used for KB and Mouse Binds
        public KeyCode Key;

        // These are used for the controller
        public KeyCode ButtonCode;
        public string ButtonName;
    }

    public List<KeyBind> KeyBinds = new List<KeyBind>();
    public List<Action> Actions = new List<Action>();

    #region these are the names of the Axis we can use to recieve input from the xobox 360 controller
    public string[] JoyAxis = new string[]
    {
        "Cancel",
        "Left Analog Horizontal", "Left Analog Vertical",
        "Right Analog Vertical", "Right Analog Horizontal",
        "D-Pad Vertical", "D-Pad Horizontal",
        "Left Trigger", "Right Trigger"
    };
    #endregion

    #region all others
    public string[] OtherAxis = new string[]
    {
        "Horizontal","Vertical",
        "Fire1", "Fire2", "Fire3",
        "Jump",
        "Mouse X", "Mouse Y",
        "Mouse ScrollWheel",
        "Submit", "Cancel",
    };
    #endregion

    // keycodes and buttons share the same indexes
    #region  these are the names buttons on the xobox 360 controller we can use
    public string[] XboxButtons = new string[]
    {
        "A", "B", "X", "Y",
        "L1", "R1",
        "Back", "Start",
        "Left Analog Click", "Right Analog Click"
    };
    #endregion

    #region  these are the keycodes of the xobox 360 controller buttons
    public KeyCode[] XboxKeyCodes = new KeyCode[]
    {
         KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3,
         KeyCode.Joystick1Button4, KeyCode.Joystick1Button5,
         KeyCode.Joystick1Button6, KeyCode.Joystick1Button7,
         KeyCode.Joystick1Button8, KeyCode.Joystick1Button9
    };
    #endregion
}
