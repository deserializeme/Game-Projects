using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGizmo : MonoBehaviour
{
    public bool occupied;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gameObject.transform.position, new Vector3(.1f,.1f,.1f));
    }
}

