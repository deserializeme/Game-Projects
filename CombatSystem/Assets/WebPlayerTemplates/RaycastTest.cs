using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour {

    public GameObject Source;
    public GameObject Target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 Direction = Target.transform.position - Source.transform.position;
        float Distance = RangeCheck.Distance(Source, Target);
        RaycastHit hit;

        if (Physics.Raycast(Source.transform.position, Direction, out hit, Distance))
        {
            if(hit.collider.gameObject == Target)
            {
                Debug.DrawRay(Source.transform.position, Direction, Color.red, Distance);
            }
        }
        else
        {
            Debug.DrawRay(Source.transform.position, Direction, Color.green, Distance);
        }

    }
}
