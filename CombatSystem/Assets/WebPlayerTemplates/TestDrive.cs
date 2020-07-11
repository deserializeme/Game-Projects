using UnityEngine;
using System.Collections;

public class TestDrive : MonoBehaviour {

    public CreateSplineProfile Spline;
    public GameObject Passenger;
    [Range(0,1)]
    public float T;
    public GameObject End;
    public GameObject Startpoint;
    bool Forward;
    float Length;
    float t;
    LineRenderer Line;

    // Use this for initialization
    void Start () {

        Vector3 Source = Startpoint.transform.position;
        Vector3 Target = End.transform.position;
        Length = Vector3.Distance(Source, Target);
        LineRenderer Line = gameObject.GetComponent<LineRenderer>();
        


        float Scale = Vector3.Distance(Source, Target) / Length;

        // Distance along a line: (Origin - Distance * (Origin - (Vector + Origin)))

      /*  
        for (int i = 0; i < 20; i++)
        {
            t = i * .05f;

            GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Sphere.transform.position = SplineMaker.GetPoint(Spline, Startpoint.transform.position, End.transform.position, t);
            Sphere.transform.localScale = new Vector3(.4f, .4f, .4f);
            Sphere.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        */

        for (int i = 0; i < 20; i++)
        {
            t = i * .05f;

            GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject Sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Vector3 Binorm = SplineMaker.GetBiNormal(Spline, Startpoint.transform.position, End.transform.position, t);
            Vector3 p = SplineMaker.GetPoint(Spline, Startpoint.transform.position, End.transform.position, t);
            
            
            Vector3 p1 = p + Spline.OscillationRangeH * (p - (Binorm + p));
            Vector3 p2 = p - Spline.OscillationRangeH * (p - (Binorm + p));

            Sphere.transform.position = p1;
            Sphere1.GetComponent<MeshRenderer>().material.color = Color.red;
            Sphere1.transform.position = p2;
            Sphere.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        /*
        for (int i = 0; i < 20; i++)
        {
            t = i * .05f;

            GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Sphere.transform.position = SplineMaker.GetOrbitPoint(Spline, Startpoint.transform.position, End.transform.position, t, Spline.OrbitSpeed);
            Sphere.transform.localScale = new Vector3(.4f, .4f, .4f);
            Sphere.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        */
        

    }
	
	// Update is called once per frame
	void Update () {
          
        if (Spline.OscillateH)
        {
            Passenger.transform.position = SplineMaker.GetOscPoint(Spline, Startpoint.transform.position, End.transform.position, T, 1);
        }

        if (Spline.OscillateV)
        {
            Passenger.transform.position = SplineMaker.GetOscPoint(Spline, Startpoint.transform.position, End.transform.position, T, 1);
        }

        if (Spline.Orbit)
        {
            Passenger.transform.position = SplineMaker.GetOrbitPoint(Spline, Startpoint.transform.position, End.transform.position, T, Spline.OrbitSpeed);
        }

        if (Spline.OscillateH == false && Spline.OscillateV == false)
        {
            if (Spline.Orbit == false)
            {
                Passenger.transform.position = SplineMaker.GetPoint(Spline, Startpoint.transform.position, End.transform.position, T);
            }
        }

        T = Mathf.PingPong(Time.time * .1f, 1);
        Vector3 Binorm = SplineMaker.GetTangent(Spline, Startpoint.transform.position, End.transform.position, T);
        Vector3 p = SplineMaker.GetPoint(Spline, Startpoint.transform.position, End.transform.position, T);
        Vector3 p1 = Passenger.transform.position;
        Vector3 p2 = Passenger.transform.position + Spline.OscillationRangeH * (p - (Binorm + p));
        // Distance along a line: (Origin - Distance * (Origin - (Vector + Origin)))

        gameObject.GetComponent<LineRenderer>().SetPosition(0, p1);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, p2);

    }
}
