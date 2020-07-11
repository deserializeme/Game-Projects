using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject Target;

    [Range(0, 20)]
    public int FocalHeight;

    [Range(0, 50)]
    public int FollowDistance;

    [Range(0, 50)]
    public int FollowHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         Vector3 newLook = new Vector3(Target.transform.position.x, FocalHeight, Target.transform.position.z);
         gameObject.transform.LookAt(newLook);

        Vector3 newPOS = new Vector3(gameObject.transform.position.x, FollowHeight, Target.transform.position.z - FollowDistance);
        gameObject.transform.position = newPOS;
    }
}
