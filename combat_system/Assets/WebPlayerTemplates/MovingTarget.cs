using UnityEngine;
using System.Collections;

public class MovingTarget : MonoBehaviour {

    public GameObject Target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float Ping = Mathf.PingPong(Time.time * 5, 35);
        float Pong = Mathf.PingPong(Time.time * 2, 18);
        Vector3 newvec = new Vector3(Ping, Target.transform.position.y, Pong);
        Target.transform.position = newvec;

    }
}
