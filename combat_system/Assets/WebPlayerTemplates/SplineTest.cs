using UnityEngine;
using System.Collections;

public class SplineTest : MonoBehaviour {


    /*
    public GameObject Source;
    public GameObject Target;
    
    [Range(0,5)]
    public float Speed;

    [Range(0, 5)]
    public int Speed_Modifier;

    [Range(0, 1)]
    public float Acceleration_Trheshold;

    [Range(0, 20)]
    public int Drift;

    [Range(0, 5)]
    public float Drift_Speed;

    [Range(0.01f, 3)]
    public float Flight_Speed;

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 Tpos;

    Vector3 newP1;

    public bool Direct;
    public bool RandomSpline;


    public bool Oscillate;

    [Range(0, 5)]
    public float Oscillation_Speed;

    [Range(0,5)]
    public float Oscillation_Length_X;

    [Range(0, 5)]
    public float Oscillation_Length_Y;

    [Range(0, 5)]
    public float Oscillation_Length_Z;

    Vector3 OsPo1;
    Vector3 OsPo2;

    [Range(0, 1)]
    public float t;

    // Use this for initialization
    void Start () {

        Source = gameObject.GetComponent<SplineMaker>().Source;
        Target = gameObject.GetComponent<SplineMaker>().Target;
        p0 = Source.transform.position;
        p2 = Target.transform.position;

        Speed = .2f;

        if(Direct == true)
        {
            gameObject.GetComponent<SplineMaker>().p1 = (p0 + (0.5f * (p2 - p0)));
        }

        if (RandomSpline == true)
        {
            gameObject.GetComponent<SplineMaker>().p1 = (p0 + (0.5f * (p2 - p0)) + new Vector3(Random.Range(-Drift, Drift), Random.Range(0, Drift), Random.Range(0, Vector3.Distance(p0, p2))));
            newP1 = (p0 + (0.5f * (p2 - p0)) + new Vector3(Random.Range(-Drift, Drift), Random.Range(0, Drift), Random.Range(0, Vector3.Distance(p0, p2))));
        }

        t = 0;


    }
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<SplineMaker>().t = t;
        gameObject.GetComponent<SplineMaker>().Tpos = SplineMaker.GetPoint(p0, p1, p2, t);



        if (t > Acceleration_Trheshold)
        {
            Speed = Speed_Modifier;
        }
        t += Mathf.Lerp(0.0f, 1.0f, Time.deltaTime * Flight_Speed);

        if (RandomSpline == true)
        {
            gameObject.GetComponent<SplineMaker>().p1 = Vector3.Lerp(gameObject.GetComponent<SplineMaker>().p1, newP1, Time.deltaTime * Drift_Speed);
        }

        if (t > .99)
        {
            Start();
        }
	}
    */
}
