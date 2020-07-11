using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SplineManager))]
public class SplineEditor : Editor
{

    // Distance along a line: (Origin - Distance * (Origin - (Vector + Origin)))

    private void OnSceneGUI()
    {
        SplineManager Spline = (SplineManager)target;

        Spline.Spline.OscillationRangeH = Spline.OscillationRangeH;
        Spline.Spline.OscillationSpeedH = Spline.OscillationSpeedH;
        Spline.Spline.OscillationRangeV = Spline.OscillationRangeV;
        Spline.Spline.OscillationSpeedV = Spline.OscillationSpeedV;


        // create the handles to edit the points

        if (Spline.EditorHandles == true)
        {
            if (Spline.LockOrigins != true)
            {
                Quaternion handleRotation0 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                Handles.DoPositionHandle(Spline.Spline.Points[0], handleRotation0);
                EditorGUI.BeginChangeCheck();
                Vector3 p0 = Handles.DoPositionHandle(Spline.Spline.Points[0], handleRotation0);
                if (EditorGUI.EndChangeCheck())
                {
                    Spline.Spline.Points[0] = p0;
                    Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
                    Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);
                }
            }


            Quaternion handleRotation1 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
            Handles.DoPositionHandle(Spline.Spline.Points[1], handleRotation1);
            EditorGUI.BeginChangeCheck();
            Vector3 p1 = Handles.DoPositionHandle(Spline.Spline.Points[1], handleRotation1);
            if (EditorGUI.EndChangeCheck())
            {
                Spline.Spline.Points[1] = p1;
                Spline.Spline.ControlPointLength1 = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[1]);
                Spline.Spline.ControlPointVector1 = Vector3.Normalize(Spline.Spline.Points[1] - Spline.Spline.Points[0]);
            }


            Quaternion handleRotation2 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
            Handles.DoPositionHandle(Spline.Spline.Points[2], handleRotation2);
            EditorGUI.BeginChangeCheck();
            Vector3 p2 = Handles.DoPositionHandle(Spline.Spline.Points[2], handleRotation2);
            if (EditorGUI.EndChangeCheck())
            {
                Spline.Spline.Points[2] = p2;
                Spline.Spline.ControlPointLength2 = Vector3.Distance(Spline.Spline.Points[2], Spline.Spline.Points[3]);
                Spline.Spline.ControlPointVector2 = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[2]);
            }


            if (Spline.LockOrigins != true)
            {
                Quaternion handleRotation3 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                Handles.DoPositionHandle(Spline.Spline.Points[3], handleRotation3);
                EditorGUI.BeginChangeCheck();
                Vector3 p3 = Handles.DoPositionHandle(Spline.Spline.Points[3], handleRotation3);
                if (EditorGUI.EndChangeCheck())
                {
                    Spline.Spline.Points[3] = p3;
                    Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
                    Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);
                }
            }

        }


        // draw the control points and such

        Handles.color = Spline.ControlPointColor;

        Handles.SphereCap(0, Spline.Spline.Points[0], Quaternion.identity, Spline.ControlPointSize);
        Handles.Label(Spline.Spline.Points[0] + Vector3.up * Spline.LabelHeight, "Start");

        Handles.SphereCap(1, Spline.Spline.Points[3], Quaternion.identity, Spline.ControlPointSize);
        Handles.Label(Spline.Spline.Points[1] + Vector3.up * Spline.LabelHeight, "Control Point 1");

        Handles.SphereCap(2, Spline.Spline.Points[1], Quaternion.identity, Spline.ControlPointSize);
        Handles.Label(Spline.Spline.Points[2] + Vector3.up * Spline.LabelHeight, "Control Point 2");

        Handles.SphereCap(3, Spline.Spline.Points[2], Quaternion.identity, Spline.ControlPointSize);
        Handles.Label(Spline.Spline.Points[3] + Vector3.up * Spline.LabelHeight, "End");

        Handles.DrawLine(Spline.Spline.Points[0], Spline.Spline.Points[1]);
        Handles.DrawLine(Spline.Spline.Points[2], Spline.Spline.Points[3]);

        Handles.DrawBezier(Spline.Spline.Points[0], Spline.Spline.Points[3], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.ControlPointColor, null, Spline.SplineLineWidth);


        Handles.color = Spline.PointColor;


        Spline.Point = SplineMaker.GetPointEditor(Spline.Spline, Spline.T);


        Vector3 Point = Spline.Point;

        Handles.SphereCap(8, Point, Quaternion.identity, Spline.ControlPointSize);
        Handles.Label(Point + Vector3.up * Spline.LabelHeight, "Point");

        if (Spline.OscillateHorizontal == true)
        {
            Vector3 BiNormalVector = SplineMaker.GetBiNormalEditor(Spline.Spline, Spline.T);
            Vector3 OscPoint1H = (Spline.Point - Spline.OscillationRangeH * (Spline.Point - (BiNormalVector + Spline.Point)));
            Vector3 OscPoint2H = (Spline.Point + Spline.OscillationRangeH * (Spline.Point - (BiNormalVector + Spline.Point)));
            Vector3 OscVectorH = OscPoint2H - OscPoint1H;


            Point = (OscPoint1H - SplineMaker.Oscillator(Spline.T, Spline.OscillationSpeedH) * (OscPoint1H - (OscVectorH + OscPoint1H)));
            Handles.SphereCap(8, Point, Quaternion.identity, Spline.ControlPointSize);
            Handles.DrawLine(OscPoint1H, OscPoint2H);
        }

        if (Spline.OscillateVertical == true)
        {
            Vector3 NormalVector = SplineMaker.GetNormalEditor(Spline.Spline, Spline.T);
            Vector3 OscPoint1V = (Spline.Point - Spline.OscillationRangeV * (Spline.Point - (NormalVector + Spline.Point)));
            Vector3 OscPoint2V = (Spline.Point + Spline.OscillationRangeV * (Spline.Point - (NormalVector + Spline.Point)));
            Vector3 OscVectorV = OscPoint2V - OscPoint1V;


            Point = (OscPoint1V - SplineMaker.Oscillator(Spline.T, Spline.OscillationSpeedV) * (OscPoint1V - (OscVectorV + OscPoint1V)));
            Handles.SphereCap(8, Point, Quaternion.identity, Spline.ControlPointSize);
            Handles.DrawLine(OscPoint1V, OscPoint2V);
        }

        if (Spline.Orbit == true)
        {
            Vector3 BiNormalVector = SplineMaker.GetBiNormalEditor(Spline.Spline, Spline.T);
            Vector3 OscPoint1H = (Spline.Point - SplineMaker.OrbitDistance(Spline.OrbitRange, Spline.T) * (Spline.Point - (BiNormalVector + Spline.Point)));
            Vector3 OscPoint2H = (Spline.Point + SplineMaker.OrbitDistance(Spline.OrbitRange, Spline.T) * (Spline.Point - (BiNormalVector + Spline.Point)));
            Vector3 OscVectorH = OscPoint2H - OscPoint1H;

            Vector3 NormalVector = SplineMaker.GetNormalEditor(Spline.Spline, Spline.T);
            Vector3 OscPoint1V = (Spline.Point - SplineMaker.OrbitDistance(Spline.OrbitRange, Spline.T) * (Spline.Point - (NormalVector + Spline.Point)));
            Vector3 OscPoint2V = (Spline.Point + SplineMaker.OrbitDistance(Spline.OrbitRange, Spline.T) * (Spline.Point - (NormalVector + Spline.Point)));
            Vector3 OscVectorV = OscPoint2V - OscPoint1V;

            Vector3 Point1 = (OscPoint1V - SplineMaker.OrbiterV(Spline.T, Spline.OrbitSpeed) * (OscPoint1V - (OscVectorV + OscPoint1V)));
            Vector3 Point2 = (OscPoint1H - SplineMaker.OrbiterH(Spline.T, Spline.OrbitSpeed) * (OscPoint1H - (OscVectorH + OscPoint1H)));
            Vector3 OrbitPoint = new Vector3(Point2.x, Point1.y, Point2.z);

            Handles.DrawLine(OscPoint1V, OscPoint2V);
            Handles.DrawLine(OscPoint1H, OscPoint2H);
            Handles.DrawWireDisc(Spline.Point, SplineMaker.GetTangentEditor(Spline.Spline, Spline.T), SplineMaker.OrbitDistance(Spline.OrbitRange, Spline.T));
            Handles.SphereCap(8, OrbitPoint, Quaternion.identity, Spline.ControlPointSize);
        }


        if (Spline.ShowDirection == true)
        {
            Vector3 TangentVector = SplineMaker.GetTangentEditor(Spline.Spline, Spline.T);
            Vector3 TangetPoint = (Point - Spline.DirectionDistance * (Point - (TangentVector + Point)));
            Handles.DrawLine(Point, TangetPoint);
            Handles.ConeCap(1, TangetPoint, Quaternion.FromToRotation(Vector3.forward, TangentVector), Spline.ControlPointSize);


            Vector3 NormalVector = SplineMaker.GetNormalEditor(Spline.Spline, Spline.T);
            Vector3 NormalPoint = (Point - Spline.DirectionDistance * (Point - (NormalVector + Point)));
            Handles.DrawLine(Point, NormalPoint);
            Handles.ConeCap(1, NormalPoint, Quaternion.FromToRotation(Vector3.forward, NormalVector), Spline.ControlPointSize);

            Vector3 BiNormalVector = SplineMaker.GetBiNormalEditor(Spline.Spline, Spline.T);
            Vector3 BiNormalPoint = (Point - Spline.DirectionDistance * (Point - (BiNormalVector + Point)));
            Handles.DrawLine(Point, BiNormalPoint);
            Handles.ConeCap(1, BiNormalPoint, Quaternion.FromToRotation(Vector3.forward, BiNormalVector), Spline.ControlPointSize);
        }



        // debug options for seeing the spline generated from vector information

        if (Spline.Debug == true)
        {
            // splinescale should be vector3.distance(souce,target)/splinelength

            Handles.color = Color.red;
            Handles.SphereCap(4, Spline.Spline.Points[0], Quaternion.identity, Spline.ControlPointSize * 1.5F);

            Vector3 SplineEnd = (Spline.Spline.Points[0] - (Spline.Spline.SplineLength * Spline.SplineScale) * (Spline.Spline.Points[0] - (Spline.Spline.LenghtVector + Spline.Spline.Points[0])));
            Handles.SphereCap(5, SplineEnd, Quaternion.identity, Spline.ControlPointSize * 1.5F);

            Vector3 ControlPoint1 = (Spline.Spline.Points[0] - (Spline.Spline.ControlPointLength1 * Spline.SplineScale) * (Spline.Spline.Points[0] - (Spline.Spline.ControlPointVector1 + Spline.Spline.Points[0])));
            Handles.SphereCap(6, ControlPoint1, Quaternion.identity, Spline.ControlPointSize * 1.5F);

            Vector3 ControlPoint2 = (SplineEnd + (Spline.Spline.ControlPointLength2 * Spline.SplineScale) * (SplineEnd - (Spline.Spline.ControlPointVector2 + SplineEnd)));
            Handles.SphereCap(7, ControlPoint2, Quaternion.identity, Spline.ControlPointSize * 1.5F);

            Handles.DrawBezier(Spline.Spline.Points[0], SplineEnd, ControlPoint1, ControlPoint2, Color.red, null, Spline.SplineLineWidth);
            Handles.DrawLine(Spline.Spline.Points[0], ControlPoint1);
            Handles.DrawLine(ControlPoint2, SplineEnd);

        }

        Spline.Spline.OscillateH = Spline.OscillateHorizontal;
        Spline.Spline.OscillateV = Spline.OscillateVertical;
        Spline.Spline.Orbit = Spline.Orbit;
        Spline.Spline.Randomize = Spline.AutoRandom;
        Spline.Spline.Reflection = Spline.RandomizerReflection;
        Spline.Spline.RandomD = Spline.RandomD;
        Spline.Spline.RandomH = Spline.RandomH;
        Spline.Spline.RandomV = Spline.RandomV;

        if (GUI.changed)
        {
            EditorUtility.SetDirty(Spline.Spline);
        }
    }



    //editor syles

    public override void OnInspectorGUI()
    {
        SplineManager Spline = (SplineManager)target;

        if (GUILayout.Button("Reset"))
        {
            Spline.Spline.Points[0] = new Vector3(-10, 0, 0);
            Spline.Spline.Points[1] = new Vector3(-5, 0, 0);
            Spline.Spline.Points[2] = new Vector3(0, 0, 0);
            Spline.Spline.Points[3] = new Vector3(5, 0, 0);
        }

        if (GUILayout.Button("Randomize"))
        {
            if(Spline.RandomizerReflection == false)
            {
                Spline.Spline.Points[1] = SplineMaker.RandomControlPoint(Spline.Spline.Points[0], Spline.Spline.Points[3], Spline.RandomH, Spline.RandomD, Spline.RandomV, false);
                Spline.Spline.Points[2] = SplineMaker.RandomControlPoint(Spline.Spline.Points[0], Spline.Spline.Points[3], Spline.RandomH, Spline.RandomD, Spline.RandomV, true);
            }
            else
            {
                Vector3[] Points = SplineMaker.ReflectControlPoints(Spline.Spline.Points[0], Spline.Spline.Points[3], Spline.RandomH, Spline.RandomD, Spline.RandomV);
                Spline.Spline.Points[1] = Points[0];
                Spline.Spline.Points[2] = Points[1];
            }

            Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
            Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);
            Spline.Spline.ControlPointLength1 = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[1]);
            Spline.Spline.ControlPointVector1 = Vector3.Normalize(Spline.Spline.Points[1] - Spline.Spline.Points[0]);
            Spline.Spline.ControlPointLength2 = Vector3.Distance(Spline.Spline.Points[2], Spline.Spline.Points[3]);
            Spline.Spline.ControlPointVector2 = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[2]);
            Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
            Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);

        }


        base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(Spline.Spline);
        }
    }

}
