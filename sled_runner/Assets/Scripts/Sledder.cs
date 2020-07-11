using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sledder : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody Rb;

    [Range(0f,20f)]
    public int Force;

    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rb.AddForce(-Vector3.right * Force, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rb.AddForce(Vector3.right * Force, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Rb.AddForce(Vector3.forward * Force, ForceMode.Force);
        }
    }
}
