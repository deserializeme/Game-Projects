using UnityEngine;
using System.Collections;

public class AOEtest : MonoBehaviour {

    public static void GetObjects(GameObject Target, float Radius = 10f)
    {
        Collider[] hitColliders = Physics.OverlapSphere(Target.transform.position, Radius);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                MeshRenderer Rend = hitColliders[i].gameObject.GetComponent<MeshRenderer>();
                Rend.material.color = Color.red;
            }
        }
    }
}
