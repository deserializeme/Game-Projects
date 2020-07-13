using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject input_manager_object;
    InputManager input_manager;
    void Start()
    {
        input_manager = input_manager_object.GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input_manager.Inputs["Jump"].KBM.Active){
            Debug.Log("Jump pressed and held for " + input_manager.Inputs["Jump"].KBM.Duration + " seconds.");
        }

        if(input_manager.Inputs["RotateCamera"].KBM.Active){
            Debug.Log("Mouse Active. H-Value/V-value" + input_manager.Inputs["RotateCamera"].KBM.AxisValueH + "/" + input_manager.Inputs["RotateCamera"].KBM.AxisValueV);
        }
    }
}
