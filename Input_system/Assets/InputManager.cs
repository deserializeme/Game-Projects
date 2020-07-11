using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class InputManager : MonoBehaviour {

    public class InputNode{
        #region Delegates
        public delegate void KeyDelegate(KeyCode Key);
        public event KeyDelegate KeyDown;
        public event KeyDelegate KeyUp;

        public delegate void AxisDelegate(string Axis, float Value);
        public event AxisDelegate AxisActive;
        #endregion

        #region Variables
        public CreateKeybinds.KeyBind myBind;
        public float myValue;
        public float Duration;
        public bool AxisIdle;
        public bool Active;
        public string Name;

        public bool ActiveH;
        public float AxisValueH;
        public string AxisNameH;

        public bool ActiveV;
        public float AxisValueV;
        public string AxisNameV;
        #endregion

        void MonitorKey()
        {
            if (myBind.Scheme == CreateKeybinds.ControllerType.Keyboard)
            {
                #region Keys
                if (Input.GetKeyDown(myBind.Key))
                {
                    KeyDown(myBind.Key);
                }

                if (Input.GetKeyUp(myBind.Key))
                {
                    KeyUp(myBind.Key);
                }
                if (!Active)
                {
                    if (Input.GetKey(myBind.Key))
                    {
                        Active = true;
                        myValue = 1;
                    }
                }

                if (Active)
                {
                    Timer();
                    if (!Input.GetKey(myBind.Key))
                    {
                        Active = false;
                        myValue = 0;
                        Duration = 0;
                    }
                }
                #endregion
            }
        }

        void MonitorButton()
        {
            if(myBind.Scheme == CreateKeybinds.ControllerType.Controller)
            {
                #region Buttons
                if (Input.GetKeyDown(myBind.ButtonCode))
                {
                    KeyDown(myBind.ButtonCode);
                }
                if (Input.GetKeyUp(myBind.ButtonCode))
                {
                    KeyUp(myBind.ButtonCode);
                }
                if (!Active)
                {
                    if (Input.GetKey(myBind.ButtonCode))
                    {
                        Active = true;
                        myValue = 1;
                    }
                }
                if (Active)
                {
                    Timer();
                    if (!Input.GetKey(myBind.ButtonCode))
                    {
                        Active = false;
                        myValue = 0;
                        Duration = 0;
                    }
                }
                #endregion
            }
        }

        void Timer()
        {
            Duration += Time.deltaTime;
        }

        void MonitorAxis()
        {
            #region Monitor Axis

            #region Horizontal axis
            if (myBind.AxisA != "Cancel" && myBind.AxisA != "")
            {
                if (Mathf.Abs(Input.GetAxis(myBind.AxisA)) > myBind.DeadZone)
                {
                    AxisValueH = Input.GetAxis(myBind.AxisA);

                    if (!ActiveH)
                    {
                        ActiveH = true;
                    }
                    else
                    {
                        Timer();
                        AxisActive(myBind.AxisA, AxisValueH);
                    }
                }
                else
                {
                    if (ActiveH)
                    {
                        ActiveH = false;
                        Duration = 0;
                        AxisValueH = 0;
                    }
                }
            }
            #endregion

            #region Vertical Axis
            if (myBind.Joystick)
            {
                if (myBind.AxisB != "Cancel" && myBind.AxisB != "")
                {
                    if (Mathf.Abs(Input.GetAxis(myBind.AxisB)) > myBind.DeadZone)
                    {
                        AxisValueV = Input.GetAxis(myBind.AxisB);

                        if (!ActiveV)
                        {
                            ActiveV = true;
                        }
                        else
                        {
                            Timer();
                            AxisActive(myBind.AxisB, AxisValueV);
                        }
                    }
                    else
                    {
                        if (ActiveV)
                        {
                            ActiveV = false;
                            Duration = 0;
                            AxisValueV = 0;
                        }
                    }
                }
            }
            #endregion

            if(ActiveH || ActiveV)
            {
                Active = true;
            }
            else
            {
                Active = false;
            }

            #endregion
        }

        public void UpdateKeys()
        {
            if (myBind.Scheme == CreateKeybinds.ControllerType.Keyboard)
            {
                if(myBind.Key != KeyCode.None)
                {
                    MonitorKey();
                }
                else
                {
                    MonitorAxis();
                }
                
            }

            if(myBind.Scheme == CreateKeybinds.ControllerType.Controller)
            {
                MonitorButton();
                if (myBind.ButtonCode != KeyCode.None)
                {
                    MonitorButton();
                }
                else
                {
                    MonitorAxis();
                }
            }
        }
    }
    public class DictNodes
    {
        public InputNode Controller;
        public InputNode KBM;
    }

    public List<InputNode> Nodes = new List<InputNode>();
    public Dictionary<string, DictNodes> Inputs = new Dictionary<string, DictNodes>();
    

    public string[] KeyNames;
    public CreateKeybinds KeyBinds;
    public List<string> ValidKeybinds = new List<string>();
    RumbleManager Rumble;

    void MonitorCheck()
    {
        for (int i = 0; i < KeyBinds.KeyBinds.Count; i++)
        {
            CreateMonitor(KeyBinds.KeyBinds[i]);
        }
    }

    void CreateMonitor(CreateKeybinds.KeyBind K)
    {
        InputNode N = new InputNode();
        N.myBind = K;
        N.Active = false;
        N.Name = K.Name;

        if (K.Joystick)
        {
            N.AxisNameH = K.AxisA;
            N.AxisNameV = K.AxisB;
        }
        else
        {
            N.AxisNameH = K.AxisA;
        }

        N.KeyDown += KeyDown;
        N.KeyUp += KeyUp;
        N.AxisActive += AxisActive;

        Nodes.Add(N);
    }

    void PupulateInputs()
    {
        for(int i = 0; i < Nodes.Count; i += 2)
        {
            DictNodes D = new DictNodes();
            D.Controller = Nodes[i];
            D.KBM = Nodes[i + 1];
            Inputs.Add(Nodes[i].myBind.Action.ToString(), D);
        }
    }

    void Start()
    {
        MonitorCheck();
        PupulateInputs();
        Rumble = new RumbleManager();
    }

    void Update()
    {
        for(int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].UpdateKeys();
        }
    }

    void KeyDown(KeyCode Key)
    {
        //Debug.Log(Key);
        //if (Key != KeyCode.None)
        //{
        //Rumble.DoRumble(XInputDotNetPure.PlayerIndex.One, 1f, 1f, .1f);
        //}
        
    }

    void KeyUp(KeyCode Key)
    {
        //Debug.Log(Key);
    }

    void AxisActive(string Axis, float Value)
    {
        //Debug.Log(Axis + " " + Value);
    }

    void OnDisable()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].KeyDown -= KeyDown;
            Nodes[i].KeyUp -= KeyUp;
            Nodes[i].AxisActive -= AxisActive;

        }
    }
}
