using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BreakBehavior>())
        {
            collision.gameObject.GetComponent<BreakBehavior>().BreakNow();
        }
        Rigidbody Rb = gameObject.GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
        //Rb.useGravity = false;
        Rb.velocity = new Vector3(0, 0, 0);

        //Debug.Log(collision.gameObject.name);
        GameObject.Destroy(this.gameObject, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
