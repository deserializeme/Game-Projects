using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnPointGizmo : MonoBehaviour
{
    public bool occupied;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gameObject.transform.position, .1f);
    }
}
