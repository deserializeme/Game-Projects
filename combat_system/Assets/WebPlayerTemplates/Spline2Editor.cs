using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Splines2))]
public class Spline2Editor : Editor
{

    private void OnSceneGUI()
    {
        Splines2 Spline = target as Splines2;


        float MedianPointSize = Spline.Editor_MedianPointSize;
        float ControlPointSize = Spline.Editor_ControlPointSize;
        float PointSize = Spline.Editor_PointSize;

        //adds control handles to the control points so we can move them around

        if (Spline.Editor_Handles == true)
        {
            Quaternion handleRotation1 = Tools.pivotRotation == PivotRotation.Local ? Spline.Source.transform.rotation : Quaternion.identity; ;
            Handles.DoPositionHandle(Spline.p1, handleRotation1);

            EditorGUI.BeginChangeCheck();
            Vector3 p1 = Handles.DoPositionHandle(Spline.p1, handleRotation1);
            if (EditorGUI.EndChangeCheck())
            {
                Spline.p1 = p1;


                Spline.p1_Horizontal = p1.x - Spline.Source.transform.position.x;
                Spline.p1_Vertical = p1.y - Spline.Source.transform.position.y;
                Spline.p1_Range = p1.z - Spline.Source.transform.position.z;
            }



            Quaternion handleRotation2 = Tools.pivotRotation == PivotRotation.Local ? Spline.Target.transform.rotation : Quaternion.identity; ;
            Handles.DoPositionHandle(Spline.p2, handleRotation2);

            EditorGUI.BeginChangeCheck();
            Vector3 p2 = Handles.DoPositionHandle(Spline.p2, handleRotation2);
            if (EditorGUI.EndChangeCheck())
            {
                Spline.p2 = p2;

                Spline.p2_Horizontal = p2.x - Spline.Target.transform.position.x;
                Spline.p2_Vertical = p2.y - Spline.Target.transform.position.y;
                Spline.p2_Range = p2.z - Spline.Target.transform.position.z;
            }
        }


        // Math that generates the control points for the spline

        Spline.p0 = Spline.Source.transform.position;
        Spline.p3 = Spline.Target.transform.position;

        if(Spline.Editor_Handles != true)
        {
            if (Spline.Randomize_Curve != true)
            {
                Spline.p1 = (Spline.Source.transform.position + (Spline.p1_Range * (Spline.Source.transform.forward)) + (Spline.p1_Horizontal * (Spline.Source.transform.right)) + (Spline.p1_Vertical * (Spline.Source.transform.up)));
                Spline.p2 = (Spline.Target.transform.position + (Spline.p2_Range * (Spline.Target.transform.forward)) + (Spline.p2_Horizontal * (Spline.Target.transform.right)) + (Spline.p2_Vertical * (Spline.Target.transform.up)));

            }
            else
            {
                Spline.p1_Horizontal = Random.Range(-Spline.Randomizer_Horizontal, Spline.Randomizer_Horizontal);
                Spline.p1_Range = Random.Range(0, Spline.Randomizer_Range);
                Spline.p1_Vertical = Random.Range(0, Spline.Randomizer_Height);

                if (Spline.Reflexive_Randomization == true)
                {
                    Spline.p2_Range = -Spline.p1_Range;
                    Spline.p2_Horizontal = -Spline.p1_Horizontal;
                    Spline.p2_Vertical = Spline.p1_Vertical;
                }
                else
                {
                    Spline.p2_Horizontal = Random.Range(-Spline.Randomizer_Horizontal, Spline.Randomizer_Horizontal);
                    Spline.p2_Range = Random.Range(-Spline.Randomizer_Range, 0);
                    Spline.p2_Vertical = Random.Range(0, Spline.Randomizer_Height);
                }

                Spline.p1 = (Spline.Source.transform.position + (Spline.p1_Range * (Spline.Source.transform.forward)) + (Spline.p1_Horizontal * (Spline.Source.transform.right)) + (Spline.p1_Vertical * (Spline.Source.transform.up)));
                Spline.p2 = (Spline.Target.transform.position + (Spline.p2_Range * (Spline.Target.transform.forward)) + (Spline.p2_Horizontal * (Spline.Target.transform.right)) + (Spline.p2_Vertical * (Spline.Target.transform.up)));
            }
        }


        // Math that generates the median points for the spline
        Spline.a = (Spline.p0 + (Spline.t * (Spline.p1 - Spline.p0)));
        Spline.b = (Spline.p1 + (Spline.t * (Spline.p2 - Spline.p1)));
        Spline.c = (Spline.p2 + (Spline.t * (Spline.p3 - Spline.p2)));
        Spline.d = (Spline.a + (Spline.t * (Spline.b - Spline.a)));
        Spline.e = (Spline.b + (Spline.t * (Spline.c - Spline.b)));

        // get the tangent, which gives us out forward direction along the spline
        Handles.color = Color.red;

        Vector3 Tan_Point = (Spline.Point - Spline.Direction_Length * (Spline.Point - (Spline.Tan + Spline.Point)));
        if (Spline.Show_Directions == true)
        {
            Handles.DrawDottedLine(Spline.Point, Tan_Point, 1);
            Handles.ConeCap(1, Tan_Point, Quaternion.FromToRotation(Vector3.forward, Spline.Tan), MedianPointSize);
            Handles.Label(Tan_Point + Vector3.up * Spline.Editor_Label_Height, "Tangent");
        }


        //get the normal, which should alwayd point up relative to our direction
        Vector3 Normal_Vector = Vector3.Cross(Spline.Tan, Vector3.right);
        Vector3 Normal_Point = (Spline.Point - Spline.Direction_Length * (Spline.Point - (Normal_Vector + Spline.Point)));
        if (Spline.Show_Directions == true)
        {
            Handles.DrawDottedLine(Spline.Point, Normal_Point, 1);
            Handles.ConeCap(2, Normal_Point, Quaternion.FromToRotation(Vector3.forward, Normal_Vector), MedianPointSize);
            Handles.Label(Normal_Point + Vector3.up * Spline.Editor_Label_Height, "Normal");
        }

        //get the bi normal with is alwayd perpedicular to both the normal and the tangent
        Vector3 BiNormal_Vector = Vector3.Cross(Spline.Tan, Normal_Vector);
        Vector3 BiNormal_Point = (Spline.Point - Spline.Direction_Length * (Spline.Point - (BiNormal_Vector + Spline.Point)));
        if (Spline.Show_Directions == true)
        {
            Handles.DrawDottedLine(Spline.Point, BiNormal_Point, 1);
            Handles.ConeCap(3, BiNormal_Point, Quaternion.FromToRotation(Vector3.forward, BiNormal_Vector), MedianPointSize);
            Handles.Label(BiNormal_Point + Vector3.up * Spline.Editor_Label_Height, "Bi-Normal");
        }


        //the x,y oscillation math
        if (Spline.Oscillate == true)
        {
            Vector3 Spline_Center = (Spline.d + Spline.t * (Spline.e - Spline.d));

            Spline.Osc_p1 = (Spline_Center + (Spline.Osc_Range * (BiNormal_Vector)));
            Spline.Osc_p2 = (Spline_Center + (Spline.Osc_Range * ( -BiNormal_Vector)));
            Spline.Osc_Point_H = (Spline.Osc_p1 + Spline.Osc_t * (Spline.Osc_p2 - Spline.Osc_p1));

            Spline.OscV_p1 = (Spline_Center + (Spline.OscV_Range * (Normal_Vector)));
            Spline.OscV_p2 = (Spline_Center + (Spline.OscV_Range * (-Normal_Vector)));
            Spline.Osc_Point_V = (Spline.OscV_p1 + Spline.OscV_t * (Spline.OscV_p2 - Spline.OscV_p1));
        }

        // the actual movemnet along the spline
        if (Spline.Oscillate == true)
        {
            Spline.Point = (Spline.d + Spline.t * (Spline.e - Spline.d)) + (Spline.Osc_Point_H - (Spline.d + Spline.t * (Spline.e - Spline.d))) + (Spline.Osc_Point_V - (Spline.d + Spline.t * (Spline.e - Spline.d)));
        }
        else
        {
            Spline.Point = (Spline.d + Spline.t * (Spline.e - Spline.d));
        }

        Spline.Tan = Vector3.Normalize(Spline.e - Spline.d);


        // draw the control points and give them some labels

        Handles.color = Color.blue;

        Handles.SphereCap(0, Spline.p0, Quaternion.FromToRotation(Spline.p0, Vector3.forward), ControlPointSize);
        Handles.Label(Spline.p0 + Vector3.up * Spline.Editor_Label_Height, "p0 - Source");

        Handles.SphereCap(1, Spline.p1, Quaternion.FromToRotation(Spline.p1, Vector3.forward), ControlPointSize);
        Handles.Label(Spline.p1 + Vector3.up * Spline.Editor_Label_Height, "p1 - Control Point");

        Handles.SphereCap(2, Spline.p2, Quaternion.FromToRotation(Spline.p2, Vector3.forward), ControlPointSize);
        Handles.Label(Spline.p2 + Vector3.up * Spline.Editor_Label_Height, "p2 - Control Point");

        Handles.SphereCap(3, Spline.p3, Quaternion.FromToRotation(Spline.p3, Vector3.forward), ControlPointSize);
        Handles.Label(Spline.p3 + Vector3.up * Spline.Editor_Label_Height, "p3 - Target");



        // draw some lines to connect the control points
            Handles.DrawLine(Spline.p0, Spline.p1);
            Handles.DrawLine(Spline.p2, Spline.p3);

        if (Spline.Oscillate == true)
        {

            Handles.DrawLine(Spline.Osc_p1, Spline.Osc_p2);
            Handles.DrawLine(Spline.OscV_p1, Spline.OscV_p2);
        }


        // draw the median points and give them some labels

        if (Spline.MoreInfo == true)
        {
            Handles.color = Color.white;

            Handles.SphereCap(4, Spline.a, Quaternion.FromToRotation(Spline.a, Vector3.forward), MedianPointSize);
            Handles.Label(Spline.a + Vector3.up * Spline.Editor_Label_Height, "A");
            Handles.SphereCap(5, Spline.b, Quaternion.FromToRotation(Spline.b, Vector3.forward), MedianPointSize);
            Handles.Label(Spline.b + Vector3.up * Spline.Editor_Label_Height, "B");
            Handles.SphereCap(6, Spline.c, Quaternion.FromToRotation(Spline.c, Vector3.forward), MedianPointSize);
            Handles.Label(Spline.c + Vector3.up * Spline.Editor_Label_Height, "C");
            Handles.SphereCap(7, Spline.d, Quaternion.FromToRotation(Spline.d, Vector3.forward), MedianPointSize);
            Handles.Label(Spline.d + Vector3.up * Spline.Editor_Label_Height, "D");
            Handles.SphereCap(8, Spline.e, Quaternion.FromToRotation(Spline.e, Vector3.forward), MedianPointSize);
            Handles.Label(Spline.e + Vector3.up * Spline.Editor_Label_Height, "E");
        }

        if (Spline.Oscillate == true)
        {
            Handles.SphereCap(11, Spline.Osc_p1, Quaternion.FromToRotation(Spline.Osc_p1, Vector3.forward), MedianPointSize);
            Handles.SphereCap(12, Spline.Osc_p2, Quaternion.FromToRotation(Spline.Osc_p2, Vector3.forward), MedianPointSize);
            Handles.SphereCap(13, Spline.OscV_p1, Quaternion.FromToRotation(Spline.Osc_p1, Vector3.forward), MedianPointSize);
            Handles.SphereCap(14, Spline.OscV_p2, Quaternion.FromToRotation(Spline.Osc_p2, Vector3.forward), MedianPointSize);
        }

        // draw lines to show the frame of the spline

        Handles.color = Color.black;

        if (Spline.MoreInfo == true)
        {
            Handles.DrawLine(Spline.a, Spline.b);
            Handles.DrawLine(Spline.b, Spline.c);
            Handles.DrawLine(Spline.d, Spline.e);
            Handles.DrawLine(Spline.p1, Spline.p2);
        }



        // draw the point we really care about

        Handles.color = Color.red;

        Handles.SphereCap(9, Spline.Point, Quaternion.FromToRotation(Spline.Point, Vector3.forward), PointSize);
        Handles.Label(Spline.Point + Vector3.up * Spline.Editor_Label_Height, "Point");

        // draw the spline
        float SplineWidth = Spline.Editor_SplineWidth;
        Handles.color = Color.blue;
        Handles.DrawBezier(Spline.p0, Spline.p3, Spline.p1, Spline.p2, Color.blue, null, SplineWidth);

        // draws the orbit radius and point

        

        if (Spline.Orbit == true)
        {
            Handles.DrawWireDisc(Spline.Point, Spline.Tan, Spline.Orbit_Distance);

            float r = Spline.Orbit_Distance;
            float s = Spline.Orbit_t * Mathf.Deg2Rad;
            float t = Spline.Orbit_Vert_Angle * Mathf.Deg2Rad;

            float x = (r * Mathf.Cos(s) * Mathf.Sin(t));
            float y = (r * Mathf.Sin(s) * Mathf.Sin(t));
            float z = (r * Mathf.Cos(t));

            Vector3 Orbit_Point = new Vector3(x, y, z) + (Spline.Point);

            Handles.SphereCap(9, Orbit_Point, Quaternion.FromToRotation(Spline.Point, Vector3.forward), PointSize);
            Handles.Label(Orbit_Point + Vector3.up * Spline.Editor_Label_Height, "Orbit Point");
        }


        Spline.TangentPoint = Tan_Point;
        Spline.TangentVector = Spline.Tan;
        Spline.NormalPoint = Normal_Point;
        Spline.NormalPoint = Normal_Vector;
        Spline.BiNormalPoint = BiNormal_Point;
        Spline.BiNormalVector = BiNormal_Vector;

    }




}
