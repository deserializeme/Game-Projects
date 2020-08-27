using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public GameObject input_manager_object;
    InputManager input_manager;
    void Start()
    {
        //grab a ref to our InputManager
        input_manager = input_manager_object.GetComponent<InputManager>();

        //for each node, lets subscribe to the events
        foreach(InputManager.InputNode i in input_manager.Nodes){
            i.KeyDown += KeyDown;
            i.KeyUp += KeyUp;
            i.NodeActive += NodeActive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // example of how to check an input nodes information
        if(input_manager.Inputs["Jump"].KBM.Active){
            //Debug.Log("Jump pressed and held for " + input_manager.Inputs["Jump"].KBM.Duration + " seconds.");
        }

        // an example using an Axis instead of a key
        if(input_manager.Inputs["RotateCamera"].KBM.Active){
            //Debug.Log("Mouse Active. H-Value/V-value" + input_manager.Inputs["RotateCamera"].KBM.AxisValueH + "/" + input_manager.Inputs["RotateCamera"].KBM.AxisValueV);
        }
    }


    
    //heres the functions we mapped our delagtes to.
    void KeyDown(KeyCode Key)
    {
        //Debug.Log(Key + " down press");     
    }

    void KeyUp(KeyCode Key)
    {
        //Debug.Log(Key + " key released");
    }

    void NodeActive(InputManager.InputNode Node)
    {
        Debug.Log("Node Name: " + Node.Name);
        Debug.Log("Action: " + Node.myBind.Action);
        Debug.Log("Key: " + Node.myBind.Key);
        Debug.Log("Value: " + Node.myValue);
        Debug.Log("Duration: " + Node.Duration);
    }

    void OnDisable() {
        {
        //for each node, lets unsub like its an edgy youtuber we forgot was on our to watch list
        foreach(InputManager.InputNode i in input_manager.Nodes){
            i.KeyDown -= KeyDown;
            i.KeyUp -= KeyUp;
            i.NodeActive -= NodeActive;
        }
        }
    }
}
