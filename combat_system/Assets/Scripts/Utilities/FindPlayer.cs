using UnityEngine;
using System.Collections;

public class FindPlayer : MonoBehaviour {

    /// <summary>
    /// Finds a game object with the tag "Player" and stores it as a static variable accessable by all scripts.
    /// </summary>
    public static GameObject Player;

	// Use this for initialization
	void Start () {
        string tag = "Player";
        Player = GameObject.FindGameObjectWithTag(tag);
        //Debug.Log("Player is " + Player.name);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
