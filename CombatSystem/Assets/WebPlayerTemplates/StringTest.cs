using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StringTest : MonoBehaviour {

    public Text TextBox;
    const string Stack = "{0}: {1} applies a stack of {2} on {3}. \n Total Stacks = {4} \n";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        TextBox.text += string.Format(Stack, Time.time.ToString(), gameObject.name, gameObject.name, gameObject.name, gameObject.name);
	
	}
}
