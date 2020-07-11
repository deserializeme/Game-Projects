using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class TestSplineMovement : MonoBehaviour
{

    public CreateSplineProfile Spline;
    public GameObject Mover;
    [Range(0, 2)]
    public float Speed;
    [Range(0, 10)]
    public float OrbitDistance;
    [Range(0, 10)]
    public float OrbitSpeed;
    public bool Active;
    public float T;

    [Range(.1f, 3)]
    public float FlightTime;


    // Use this for initialization
    void Start()
    {
        Spline.Caster = GameObject.Find("Cube");
        Spline.Victim = GameObject.Find("Cube1");
        Spline.Source = Spline.Caster.transform.position;
        Spline.Target = Spline.Victim.transform.position;
    }


    public IEnumerator<float> MoveObject( float duration)
    {
        float startTime = Time.time;

        float Distance1 = Spline.SplineLength / duration;
        float Distance2 = Vector3.Distance(gameObject.transform.position, Spline.Points[3]);

        while (Time.time < startTime + (duration * Spline.SplineScale))
        {
            Distance2 = Vector3.Distance(gameObject.transform.position, Spline.Points[3]);
            float DistanceFraction = Distance2 / Distance1;
            T = Mathf.Lerp(0, 1, (Time.time - startTime) / (duration * Spline.SplineScale));
            SplineMaker2.TraverseSpline(Spline.Caster, Spline.Victim, Spline, Mover, T);
            yield return 0f;
        }
        Destroy(Mover);
        AttackChecklist.Callback();
    }
}
