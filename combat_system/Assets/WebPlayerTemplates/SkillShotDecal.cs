using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillShotDecal : MonoBehaviour {

    GameObject Decal;
    public GameObject Player;

    Mesh M;

    public Vector3 PlayerPOS
    {
        get
        {
            return Player.transform.position;
        }
    }

    public Vector3 MousePOS
    {
        get
        {
            return GetMousePOS();
        }
    }

    public Vector3[] StartPoints = new Vector3[2];
    public float StartWidth;

    public Vector3[] EndPoints = new Vector3[2];
    public float EndWidth;

    int Resolution;

    public Vector3[] Verts;
    public int[] Tris;
    public LayerMask Layer;




    // Use this for initialization
    void Start () {

        Decal = new GameObject();
        M = Decal.AddComponent<MeshFilter>().mesh;
        Resolution = 4;
	
	}
	
    Vector3 GetMousePOS()
    {
        Vector3 POS = Vector3.zero;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            POS = hit.point;
        }

        return POS;
    }

    void GetPoints()
    {
        float Distance = Vector3.Distance(MousePOS, PlayerPOS);
        Vector3 Direction = (MousePOS - PlayerPOS) / Distance;

        // get the tangent so that we retain the correct shape
        // tangent sould always be horizonal from the perspective 
        // of the source
        Vector3 Tan = Vector3.Cross(Direction, Vector3.up);


        // start left
        StartPoints[0] = PlayerPOS + (-Tan * StartWidth / 2);
        // start right
        StartPoints[1] = PlayerPOS + (Tan * StartWidth / 2);

        // end left
        EndPoints[0] = MousePOS + (-Tan * EndWidth / 2);
        // end right
        EndPoints[1] = MousePOS + (Tan * EndWidth / 2);
    }

    float GetHeight(Vector3 point)
    {
        RaycastHit hit;
        Vector3 From = point + Vector3.up * 100;
        Vector3 HitPoint = point;

        if (Physics.Raycast(From, -Vector3.up, out hit, Mathf.Infinity, Layer))
        {
            HitPoint = hit.point;
        }

        return HitPoint.y;
    }

    Vector3[] GetGrid()
    {
        // define the area of the decal
        Vector3 LeftSideDirection = (EndPoints[0] - StartPoints[0]).normalized;
        Vector3 RightSideDirection = (EndPoints[1] - StartPoints[1]).normalized;
        Vector3 BottomDirection = (StartPoints[1] - StartPoints[0]).normalized;
        Vector3 TopDirection = (EndPoints[1] - EndPoints[0]).normalized;

        // get the distances of each side
        float LeftDistance = Vector3.Distance(EndPoints[0], StartPoints[0]);
        float RightDistance = Vector3.Distance(EndPoints[1], StartPoints[1]);
        float BottomDistance = Vector3.Distance(StartPoints[1], StartPoints[0]);
        float TopDistance = Vector3.Distance(EndPoints[1], EndPoints[0]);

        //create array to hold the verticies


        List<Vector3> Verts = new List<Vector3>();
        List<int> tris = new List<int>();

        Verts.Add(StartPoints[0]);
        Verts.Add(StartPoints[1]);
        Verts.Add(EndPoints[0]);
        Verts.Add(EndPoints[0]);

        tris.Add(0);
        tris.Add(2);
        tris.Add(3);



        Tris = tris.ToArray();
        return Verts.ToArray();


    }

	// Update is called once per frame
	void Update () {
        GetPoints();
        Verts = GetGrid();
        M.vertices = Verts;
        M.triangles = Tris;
        M.RecalculateNormals();
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(MousePOS, .3f);

        Gizmos.color = Color.cyan;
        for (int i = 0; i < Verts.Length; i++)
        {
            Gizmos.DrawSphere(Verts[i], .3f);
        }
    }
}
