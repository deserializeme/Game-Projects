using UnityEngine;
using System.Collections;

public class Splines2 : MonoBehaviour
{

    public Vector3 p0;
    public Vector3 p3;
    public Vector3 p1;
    public Vector3 p2;

    public Vector3 a;
    public Vector3 b;
    public Vector3 c;
    public Vector3 d;
    public Vector3 e;

    public Vector3 Osc_p1;
    public Vector3 Osc_p2;
    public Vector3 Osc_Point_H;

    public Vector3 BiNormalPoint;
    public Vector3 BiNormalVector;
    public Vector3 NormalPoint;
    public Vector3 NormalVector;
    public Vector3 TangentPoint;
    public Vector3 TangentVector;

    [Range(0, 1)]
    public float Osc_t;

    public Vector3 OscV_p1;
    public Vector3 OscV_p2;
    public Vector3 Osc_Point_V;

    [Range(0, 1)]
    public float OscV_t;


    [Range(0, 10)]
    public float Osc_Range;

    [Range(0, 10)]
    public float OscV_Range;

    [Range(0, 10)]
    public float Orbit_Distance;

    [Range(0, 360)]
    public float Orbit_t;

    [Range(0, 90)]
    public float Orbit_Vert_Angle;


    public GameObject Source;
    public GameObject Target;

    public Vector3 Point;
    public Vector3 Tan;

    [Range(-100, 100)]
    public float p1_Range;

    [Range(-30,30)]
    public float p1_Horizontal;

    [Range(0, 100)]
    public float p1_Vertical;

    [Range(-100, 100)]
    public float p2_Range;

    [Range(-30, 30)]
    public float p2_Horizontal;

    [Range(0, 100)]
    public float p2_Vertical;

    public bool Editor_Handles;
    public bool Randomize_Curve;
    public bool Reflexive_Randomization;

    [Range(0, 20)]
    public float Randomizer_Range;
    [Range(0, 20)]
    public float Randomizer_Height;
    [Range(0, 20)]
    public float Randomizer_Horizontal;

    public bool Oscillate;
    public bool Orbit;

    public bool MoreInfo;
    public bool Show_Directions;
    [Range(0, 20)]
    public float Direction_Length;

    [Range(0,5)]
    public float Editor_ControlPointSize;

    [Range(0, 5)]
    public float Editor_MedianPointSize;

    [Range(0, 5)]
    public float Editor_PointSize;

    [Range(0, 5)]
    public float Editor_SplineWidth;

    [Range(0, 3)]
    public float Editor_Label_Height;


    [Range(0, 1)]
    public float t;

    public void GetPoint()
    {
        p0 = Source.transform.position;
        p3 = Target.transform.position;

        TangentVector = (Vector3.Normalize(e - d));
        TangentPoint = (Point - 5 * (Point - (Tan + Point)));
        NormalVector = Vector3.Cross(Tan, Vector3.right);
        NormalPoint = (Point - 5 * (Point - (NormalVector + Point)));
        BiNormalVector = Vector3.Cross(Tan, NormalVector);
        BiNormalPoint = (Point - 5 * (Point - (BiNormalVector + Point)));


        if (Randomize_Curve != true)
        {
            p1 = (Source.transform.position + (p1_Range * (Source.transform.forward)) + (p1_Horizontal * (Source.transform.right)) + (p1_Vertical * (Source.transform.up)));
            p2 = (Target.transform.position + (p2_Range * (Target.transform.forward)) + (p2_Horizontal * (Target.transform.right)) + (p2_Vertical * (Target.transform.up)));
        }
        else
        {
            p1_Horizontal = Random.Range(-Randomizer_Horizontal, Randomizer_Horizontal);
            p1_Range = Random.Range(0, Randomizer_Range);
            p1_Vertical = Random.Range(0, Randomizer_Height);

            p2_Horizontal = Random.Range(-Randomizer_Horizontal, Randomizer_Horizontal);
            p2_Range = Random.Range(-Randomizer_Range, 0);
            p2_Vertical = Random.Range(0, Randomizer_Height);

            p1 = (Source.transform.position + (p1_Range * (Source.transform.forward)) + (p1_Horizontal * (Source.transform.right)) + (p1_Vertical * (Source.transform.up)));
            p2 = (Target.transform.position + (p2_Range * (Target.transform.forward)) + (p2_Horizontal * (Target.transform.right)) + (p2_Vertical * (Target.transform.up)));
        }

        if (Oscillate == true)
        {
            Vector3 Spline_Center = (d + t * (e - d));

            Osc_p1 = (Spline_Center + (Osc_Range * (BiNormalVector)));
            Osc_p2 = (Spline_Center + (Osc_Range * (-BiNormalVector)));
            Osc_Point_H = (Osc_p1 + Osc_t * (Osc_p2 - Osc_p1));

            OscV_p1 = (Spline_Center + (OscV_Range * (NormalVector)));
            OscV_p2 = (Spline_Center + (OscV_Range * (-NormalVector)));
            Osc_Point_V = (OscV_p1 + OscV_t * (OscV_p2 - OscV_p1));

            if (Reflexive_Randomization == true)
            {
                p2_Range = -p1_Range;
                p2_Horizontal = -p1_Horizontal;
                p2_Vertical = p1_Vertical;
            }
            else
            {
                p2_Horizontal = Random.Range(-Randomizer_Horizontal, Randomizer_Horizontal);
                p2_Range = Random.Range(-Randomizer_Range, 0);
                p2_Vertical = Random.Range(0, Randomizer_Height);
            }
        }

        a = (p0 + (t * (p1 - p0)));
        b = (p1 + (t * (p2 - p1)));
        c = (p2 + (t * (p3 - p2)));
        d = (a + (t * (b - a)));
        e = (b + (t * (c - b)));

        if (Oscillate == true)
        {
            Point = (d + t * (e - d)) + (Osc_Point_H - (d + t * (e - d))) + (Osc_Point_V - (d + t * (e - d)));
        }
        else
        {
            Point = (d + t * (e - d));
        }

        Tan = Vector3.Normalize(e - d);

    }

}
