using UnityEngine;
using System.Collections;

public class SplineMaker : MonoBehaviour {

    public static Vector3 GetPointEditor( CreateSplineProfile Spline, float t)
    {
        float omt = 1f - t;
        float omt2 = omt * omt;
        float t2 = t * t;


        return
            Spline.Points[0] * (omt2 * omt) +
            Spline.Points[1] * (3f * omt2 * t) +
            Spline.Points[2] * (3f * omt * t2) +
            Spline.Points[3] * (t2 * t);
    }

    public static Vector3 GetTangentEditor(CreateSplineProfile Spline, float t)
    {
        float omt = 1f - t;
        float omt2 = omt * omt;
        float t2 = t * t;

        Vector3 Tangent =
            Spline.Points[0] * (-omt2) +
            Spline.Points[1] * (3f * omt2 - 2 * omt) +
            Spline.Points[2] * (-3f * t2 + 2 * t) +
            Spline.Points[3] * (t2);

        return Tangent.normalized;
    }

    public static Vector3 GetBiNormalEditor(CreateSplineProfile Spline, float t)
    {
        Vector3 Tangent = GetTangentEditor(Spline, t);
        Vector3 BiNormal = Vector3.Cross(Vector3.up, Tangent);
        return BiNormal;
    }

    public static Vector3 GetNormalEditor(CreateSplineProfile Spline, float t)
    {
        Vector3 Tangent = GetTangentEditor(Spline, t);
        Vector3 BiNormal = Vector3.Cross(Vector3.up, Tangent);
        return Vector3.Cross(Tangent, BiNormal);
    }

    public static Vector3 GetOscPointsEditor(CreateSplineProfile Spline, float t, float Speed)
    {
        Vector3 Origin = GetPointEditor(Spline, t);

        Vector3 VectorH = GetBiNormalEditor(Spline, t);
        Vector3 Point1H = (Origin - Spline.OscillationRangeH * (Origin - (VectorH + Origin)));
        Vector3 Point2H = (Origin + Spline.OscillationRangeH * (Origin - (VectorH + Origin)));
        Vector3 OscVectorH = Point2H - Point1H;

        Vector3 OscPointH = (Point1H + t * (Point1H - (OscVectorH + Point1H)));

        return OscPointH;
    }

    public static Vector3 GetPoint(CreateSplineProfile Spline, Vector3 Source, Vector3 Target, float t)
    {
        float omt = 1f - t;
        float omt2 = omt * omt;
        float t2 = t * t;

        float Scale = Vector3.Distance(Source, Target) / Spline.SplineLength;

        // splinescale should be vector3.distance(souce,target)/splinelength
        // Distance along a line: (Origin - Distance * (Origin - (Vector + Origin)))


        Vector3 p0 = Source;
        Vector3 p1 = (p0 - (Spline.ControlPointLength1 * Scale) * (p0 - (Spline.ControlPointVector1 + p0)));
        Vector3 p3 = Target;
        Vector3 p2 = (p3 + (Spline.ControlPointLength2 * Scale) * (p3 - (Spline.ControlPointVector2 + p3)));



        return
            p0 * (omt2 * omt) +
            p1 * (3f * omt2 * t) +
            p2* (3f * omt * t2) +
            p3 * (t2 * t);
    }

    public static Vector3 GetTangent(CreateSplineProfile Spline, Vector3 Source, Vector3 Target, float t)
    {
        float omt = 1f - t;
        float omt2 = omt * omt;
        float t2 = t * t;

        float Scale = Spline.SplineLength;

        Vector3 p0 = Source;
        Vector3 p1 = (p0 - (Spline.ControlPointLength1 * Scale) * (p0 - (Spline.ControlPointVector1 + p0)));
        Vector3 p3 = Target;
        Vector3 p2 = (p3 + (Spline.ControlPointLength2 * Scale) * (p3 - (Spline.ControlPointVector2 + p3)));

        Vector3 Tangent =
            p0 * (-omt2) +
            p1 * (3f * omt2 - 2 * omt) +
            p2 * (-3f * t2 + 2 * t) +
            p3 * (t2);

        return Tangent.normalized;
    }

    public static Vector3 GetBiNormal(CreateSplineProfile Spline, Vector3 Source, Vector3 Target, float t)
    {
        Vector3 Tangent = GetTangent(Spline, Source, Target, t);
        Vector3 BiNormal = Vector3.Cross(Vector3.up, Tangent);
        return BiNormal;
    }

    public static Vector3 GetNormal(CreateSplineProfile Spline, Vector3 Source, Vector3 Target, float t)
    {
        Vector3 Tangent = GetTangent(Spline, Source, Target, t);
        Vector3 BiNormal = Vector3.Cross(Vector3.up, Tangent);
        return Vector3.Cross(Tangent, BiNormal);
    }

    public static float Oscillator(float T, int Speed)
    {
        float pos = Mathf.PingPong((T + .5f) * Speed, 1);

        return pos;
    }

    public static float OrbiterH(float T, int Speed)
    {
        float posH = Mathf.PingPong((T + .5f) * Speed, 1);

        return posH;
    }

    public static float OrbiterV(float T, int Speed)
    {
        float posV = Mathf.PingPong(T * Speed, 1);

        return posV;
    }

    public static float OrbitDistance(float Distance, float T)
    {
        float NewDistance;
        if (T < .5f)
        {
           NewDistance = Mathf.Lerp(0, Distance, T);
        }
        else
        {
           NewDistance = Mathf.Lerp(Distance, 0, T);
        }

        return NewDistance;

    }

    public static Vector3 GetOscPoint(CreateSplineProfile Spline, Vector3 Source, Vector3 Target, float T, int Speed)
    {
        Vector3 Point = SplineMaker.GetPoint(Spline, Source, Target, T);

        if (Spline.OscillateH == true)
        {
            Vector3 BiNormalVector = SplineMaker.GetBiNormal(Spline, Source, Target, T);
            Vector3 OscPoint1H = (Point - Spline.OscillationRangeH * (Point - (BiNormalVector + Point)));
            Vector3 OscPoint2H = (Point + Spline.OscillationRangeH * (Point - (BiNormalVector + Point)));
            Vector3 OscVectorH = OscPoint2H - OscPoint1H;


            Point = (OscPoint1H - SplineMaker.Oscillator(T, Spline.OscillationSpeedH) * (OscPoint1H - (OscVectorH + OscPoint1H)));
        }

        if (Spline.OscillateV == true)
        {
            Vector3 NormalVector = SplineMaker.GetNormal(Spline, Source, Target, T);
            Vector3 OscPoint1V = (Point - Spline.OscillationRangeV * (Point - (NormalVector + Point)));
            Vector3 OscPoint2V = (Point + Spline.OscillationRangeV * (Point - (NormalVector + Point)));
            Vector3 OscVectorV = OscPoint2V - OscPoint1V;


            Point = (OscPoint1V - SplineMaker.Oscillator(T, Spline.OscillationSpeedV) * (OscPoint1V - (OscVectorV + OscPoint1V)));
        }

        return Point;

    }

    public static Vector3 GetOrbitPoint(CreateSplineProfile Spline, Vector3 Source, Vector3 Target, float T, int Speed)
    {
        Vector3 Point = SplineMaker.GetPoint(Spline, Source, Target, T);

        Vector3 BiNormalVector = SplineMaker.GetBiNormal(Spline, Source, Target, T);
        Vector3 OscPoint1H = (Point - SplineMaker.OrbitDistance(Spline.OrbitDistance, T) * (Point - (BiNormalVector + Point)));
        Vector3 OscPoint2H = (Point + SplineMaker.OrbitDistance(Spline.OrbitDistance, T) * (Point - (BiNormalVector + Point)));
        Vector3 OscVectorH = OscPoint2H - OscPoint1H;

        Vector3 NormalVector = SplineMaker.GetNormal(Spline, Source, Target, T);
        Vector3 OscPoint1V = (Point - SplineMaker.OrbitDistance(Spline.OrbitDistance, T) * (Point - (NormalVector + Point)));
        Vector3 OscPoint2V = (Point + SplineMaker.OrbitDistance(Spline.OrbitDistance, T) * (Point - (NormalVector + Point)));
        Vector3 OscVectorV = OscPoint2V - OscPoint1V;

        Vector3 Point1 = (OscPoint1V - SplineMaker.OrbiterV(T, Spline.OrbitSpeed) * (OscPoint1V - (OscVectorV + OscPoint1V)));
        Vector3 Point2 = (OscPoint1H - SplineMaker.OrbiterH(T, Spline.OrbitSpeed) * (OscPoint1H - (OscVectorH + OscPoint1H)));
        Vector3 OrbitPoint = new Vector3(Point2.x, Point1.y, Point2.z);

        return OrbitPoint;
    }

    public static Vector3 RandomHorizontal(Vector3 Source, Vector3 Target, float Range)
    {
        float Distance = Vector3.Distance(Source, Target) * Range;
        Vector3 angle = (Target - Source).normalized;
        Vector3 BiNormal = Vector3.Cross(Vector3.up, angle);
        Vector3 RandH = Source - Random.Range(-Distance, Distance) * (Source - (BiNormal + Source));

        return RandH;

    }

    public static Vector3 RandomVertical(Vector3 Source, Vector3 Target, float Range)
    {
        float Distance = Vector3.Distance(Source, Target) * Range;
        Vector3 angle = (Target - Source).normalized;
        Vector3 BiNormal = Vector3.Cross(Vector3.up, angle);
        Vector3 Normal = Vector3.Cross(angle, BiNormal);
        Vector3 RandV = Source - Random.Range(0, Distance) * (Source - (Normal + Source));

        return RandV;

    }

    public static Vector3 RandomDistance(Vector3 Source, Vector3 Target, float Range, bool Reverse)
    {
        Vector3 angle = (Target - Source).normalized;
        Vector3 RandD;
        float Distance = Vector3.Distance(Source, Target) * Range;

        if (Reverse == false)
        {
           RandD = Source - Random.Range(0, Distance) * (Source - (angle + Source));
        }
        else
        {
            RandD = Target - Random.Range(0, -Distance) * (Target - (angle + Target));
        }
        return RandD;

    }

    public static Vector3 RandomControlPoint(Vector3 Source, Vector3 Target, float HRange, float DRange, float VRange, bool Reverse)
    {
        Vector3 Step1 = SplineMaker.RandomDistance(Source, Target, DRange, Reverse);
        Vector3 Step2 = SplineMaker.RandomHorizontal(Step1, Target, HRange);
        Vector3 Step3 = SplineMaker.RandomVertical(Step2, Target, VRange);

        return Step3;
    }

    public static Vector3[] ReflectControlPoints(Vector3 Source, Vector3 Target, float HRange, float DRange, float VRange)
    {
        Vector3 angle = (Target - Source).normalized;
        Vector3 BiNormal = Vector3.Cross(Vector3.up, angle);
        Vector3 Normal = Vector3.Cross(angle, BiNormal);

        float DistanceH = Vector3.Distance(Source, Target) * HRange;
        float DistanceD = Vector3.Distance(Source, Target) * DRange;
        float DistanceV = Vector3.Distance(Source, Target) * VRange;

        float RandomHorizontal = Random.Range(-DistanceH, DistanceH);
        float RandomDistance = Random.Range(0, DistanceD);
        float RandomVertical = Random.Range(0, DistanceV);

        Vector3 p1D = Source - RandomDistance * (Source - (angle + Source));
        Vector3 p2D = Target + RandomDistance * (Target - (angle + Target));

        Vector3 p1H = p1D - RandomHorizontal * (p1D - (BiNormal + p1D));
        Vector3 p2H = p2D + RandomHorizontal * (p2D - (BiNormal + p2D));

        Vector3 p1V = p1H - RandomVertical * (p1H - (Normal + p1H));
        Vector3 p2V = p2H - RandomVertical * (p2H - (Normal + p2H));

        Vector3[] Points = new Vector3[2];

        Points[0] = p1V;
        Points[1] = p2V;

        return Points;

    }


}
