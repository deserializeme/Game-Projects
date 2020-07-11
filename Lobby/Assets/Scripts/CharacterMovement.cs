using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterMovement : NetworkBehaviour
{
    private CharacterController controller;
    public float Speed;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
 

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Camera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z);
    }

    void Keys()
    {
        if(Input.GetKey(KeyCode.W))
        {
            controller.Move(transform.forward * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            controller.Move(-transform.right * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            controller.Move(-transform.forward * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            controller.Move(transform.right * Time.deltaTime * Speed);
        }


    }

    void Update()
    {
        if (isLocalPlayer)
        {
            Camera();
            Keys();
        }
    }

}

