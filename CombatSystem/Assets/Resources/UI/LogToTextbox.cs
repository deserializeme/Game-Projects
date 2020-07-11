using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// upon FixedUpdate call, dequeues logs into their appropriate textbox and adds a new line character for formatting purposes
/// </summary>
public class LogToTextbox : MonoBehaviour {

    public Text Textbox;
    public Scrollbar Scrollbar;
    
        

	// Use this for initialization
	void Start () {
	}
    void FixedUpdate()
    {
        LogToCombat();
        LogToEvent();
    }

    public void LogToCombat()
    {
        if (Messages.myCombatLog.Count > 0)
        {
            StringFast oldText = new StringFast(64);
            oldText.Append(Textbox.text);
            oldText.Append(Messages.myCombatLog.Dequeue());
            oldText.Append('\n');
            Textbox.text = oldText.ToString();
            Scrollbar.value = 0.00000f;
        }
    }

    public void LogToEvent()
    {
        if (Messages.myEventLog.Count > 0)
        {
            StringFast oldText = new StringFast(64);
            oldText.Append(Textbox.text);
            oldText.Append(Messages.myEventLog.Dequeue());
            oldText.Append('\n');
            Textbox.text = oldText.ToString();
            Scrollbar.value = 0.00000f;
        }
    }

}
