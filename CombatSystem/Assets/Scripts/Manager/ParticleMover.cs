using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MovementEffects;

public class ParticleMover : ScriptableObject
{

    public GameObject Source;
    public GameObject Target;
    public CreateSplineProfile Spline;
    public CreateNewProjectile Projectile;
    public GameObject Particle;
    public float T;

    /// <summary>
    /// launches the projectile by invoking MoveObject
    /// </summary>
    public void Launch()
    {
        if (Spline.Randomize)
        {
            SplineMaker2.RandomControlPoints(Source, Target, Spline);
        }

        if (Source.GetComponent<ViewSpline>())
        {
            Source.GetComponent<ViewSpline>().Spline = Spline;
        }

        Timing.RunCoroutine(MoveObject(Projectile.TravelTime));
    }


    /// <summary>
    /// moves the projectile along spline path and triggers a callback upon arrival at traget position
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public IEnumerator<float> MoveObject(float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + (duration * Spline.SplineScale))
        {
            T = Mathf.Lerp(0, 1, (Time.time - startTime) / (duration * Spline.SplineScale));

            SplineMaker2.TraverseSpline(Source, Target, Spline, Particle, Projectile.TravelBehavior.Evaluate(T));
            yield return 0f;
        }
        if (Target.activeInHierarchy)
        {
            AttackChecklist.ProjectileDamageCallback(Target, Projectile, Source);
        }

        if (Source.GetComponent<ViewSpline>())
        {
            Source.GetComponent<ViewSpline>().Spline = null;
        }
        Destroy(Particle);
    }

}
