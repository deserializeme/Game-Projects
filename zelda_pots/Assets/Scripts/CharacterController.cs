using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject Camera;
    public float MouseSensitivity;
    public bool InvertCamera;
    private Vector2 CurrentRotation;
    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    Rigidbody Rb;
    public float WalkSpeed;
    public float JumpHeight;

    private void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        CameraRotation();

    }

    private void FixedUpdate()
    {
        Walk();
    }

    void CameraRotation()
    {
        //rotate the camera and player aaround Y axis based on mouse lateral input
        CurrentRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;

        //rotate the camera and player aaround Y axis based on mouse lateral input
        if (InvertCamera)
        {
            CurrentRotation.y = Input.GetAxis("Mouse Y") * MouseSensitivity;
        }
        else
        {
            CurrentRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        }

        //limit the angle that the camera can tilt
        CurrentRotation.x = Mathf.Repeat(CurrentRotation.x, 360);
        CurrentRotation.y = Mathf.Clamp(CurrentRotation.y, -maxYAngle, maxYAngle);

        //apply the rotation
        Camera.transform.rotation = Quaternion.Euler(CurrentRotation.y, CurrentRotation.x, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, CurrentRotation.x, 0);
    }

    void Walk()
    {
        //gather input and combine into vector, then apply movement

        Vector3 movement = new Vector3(0,0,0);

        // forward
        if (Input.GetKey(KeyCode.W))
        {
            movement = movement + transform.forward;
        }

        // backward
        if (Input.GetKey(KeyCode.S))
        {
            movement = movement - transform.forward;
        }

        // right
        if (Input.GetKey(KeyCode.A))
        {
            movement = movement + transform.TransformDirection(Vector3.left);
        }

        // left
        if (Input.GetKey(KeyCode.D))
        {
            movement = movement + transform.TransformDirection(Vector3.right);
        }

        //combine vectors
        movement = Vector3.Normalize(movement);
        movement = movement * WalkSpeed;

        //apply
        Rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 13)
        {
            Jump();
        }
    }
}
