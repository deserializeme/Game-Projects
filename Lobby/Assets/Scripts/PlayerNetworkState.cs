using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkState : NetworkBehaviour
{
    private Material meshMaterial;

    private float movementFactor = 0.01f;

    [SyncVar]
    private Color color;

    private void Start()
    {
        meshMaterial = GetComponent<MeshRenderer>().material;
        color = Color.blue;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
            //Movement();
        }

        meshMaterial.color = color;

    }

    void HandleInput()
    {
        if (Input.GetKeyDown("space"))
        {
            Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            CmdUpdateColor(color);
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal") * movementFactor;
        float z = Input.GetAxis("Vertical") * movementFactor;
        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
    }

    [Command]
    void CmdUpdateColor(Color newColor)
    {
        color = newColor;
    }
}
