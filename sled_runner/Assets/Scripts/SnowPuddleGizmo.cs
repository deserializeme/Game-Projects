using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPuddleGizmo : MonoBehaviour
{
    public bool occupied;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(gameObject.transform.position, .1f);
    }
}
