using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NPCNetworkState : NetworkBehaviour
{
    private const float movementFactor = 0.01f;

    private void Update()
    {
        if (isServer)
        {
            float x = Input.GetAxis("Horizontal") * movementFactor;
            float z = Input.GetAxis("Vertical") * movementFactor;
            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        }
    }
}
