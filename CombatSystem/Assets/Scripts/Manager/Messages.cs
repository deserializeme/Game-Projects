using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// handles the enquing of messages into the combat and event logs as well as a basic call the Debug.log
/// </summary>
public class Messages : MonoBehaviour
{


    public static Queue<StringFast> myCombatLog = new Queue<StringFast>();
    public static Queue<StringFast> myEventLog = new Queue<StringFast>();
    public static bool Enable_Debug_Messages;
    public static bool Enabale_Log;

    void Start()
    {
        Enable_Debug_Messages = true;
        Enabale_Log = true;
    }

    public static void Message(string Message)
    {
        if (Enable_Debug_Messages)
        {
            Debug.Log(Message);
        }
    }

    public static void CombatLog(StringFast Message)
    {
        if (Messages.Enabale_Log)
        {
            Messages.myCombatLog.Enqueue(Message);
        }
    }

    public static void EventLog(StringFast Message)
    {
        if (Messages.Enabale_Log)
        {
            Messages.myEventLog.Enqueue(Message);
        }
    }
}
