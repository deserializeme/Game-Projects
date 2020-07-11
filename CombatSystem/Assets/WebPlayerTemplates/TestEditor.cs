using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using MovementEffects;

[CustomEditor(typeof(TestSplineMovement))]
public class TestEditor : Editor{

    public override void OnInspectorGUI()
    {
        TestSplineMovement myTest = (TestSplineMovement)target;

        if (GUILayout.Button("Launch"))
        {
            GameObject Mover = Instantiate(Resources.Load<GameObject>("Particles/FireComplex"));
            Mover.AddComponent<TestSplineMovement>();
            Mover.GetComponent<TestSplineMovement>().Mover = Mover;
            Mover.GetComponent<TestSplineMovement>().Spline = Instantiate(Resources.Load<CreateSplineProfile>("Attacks/RangedBehaviors/Random"));

            if (Mover.GetComponent<TestSplineMovement>().Spline.Randomize)
            {
                SplineMaker2.RandomControlPoints(Mover.GetComponent<TestSplineMovement>().Spline.Caster, Mover.GetComponent<TestSplineMovement>().Spline.Victim, Mover.GetComponent<TestSplineMovement>().Spline);
            }
            
            Timing.RunCoroutine(Mover.GetComponent<TestSplineMovement>().MoveObject(2f));
            
        }
        base.OnInspectorGUI();
    }
}
