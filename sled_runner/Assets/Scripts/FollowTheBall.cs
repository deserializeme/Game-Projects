using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheBall : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Point = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z - .2f);
        gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, Point, Time.deltaTime * 10);

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
