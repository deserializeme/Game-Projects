using UnityEngine;
using System.Collections;

public class SPLineTestNewBehaviourScript : MonoBehaviour {

    public GameObject Source;
    public GameObject Target;
    public CreateSplineProfile Spline;

    public Vector3[] points = new Vector3[4];


	// Use this for initialization
	void Start () {
        points = SplineMaker2.Points(Source, Target, Spline);

        for (int i = 0; i < points.Length; i++)
        {
            GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Sphere.name = "point " + i;
            Sphere.GetComponent<MeshRenderer>().material.color = Color.red;
            Sphere.transform.position = points[i];
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
