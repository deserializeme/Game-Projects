using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{

    public int width;

    public int length;

    public Vector3[] vertices;

    private Mesh mesh;

    public GameObject Player;

    public Vector3 MousePOS
    {
        get
        {
            return GetMousePOS();
        }
    }

    public LayerMask Layer;

    private void Awake()
    {

    }

    private Vector3 GetMousePOS()
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

    int GetNewLength()
    {
        float Distance = Vector3.Distance(Player.transform.position, MousePOS);
        return (int)Mathf.Round(Distance);
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(width + 1) * (length + 1)];

        Vector2[] uv = new Vector2[vertices.Length];
        float Distance = Vector3.Distance(Player.transform.position, MousePOS);
        Vector3 Direction = (MousePOS - Player.transform.position) / Distance;
        Vector3 PlayerTangent = Vector3.Cross(Direction, Vector3.up);

        Vector3 StartPoint = Player.transform.position + (Direction * 2) + (PlayerTangent * (width / 2));

        for (int i = 0, y = 0; y <= length; y++)
        {
            for (int x = 0; x <= width; x++, i++)
            {
                Vector3 Point = (-PlayerTangent * x) + (Direction * y) + StartPoint;
                Point.y = GetHeight(Point) + .001f;
                vertices[i] = Point;

                uv[i] = new Vector2((float)x / width, (float)y / length);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[width * length * 6];
        for (int ti = 0, vi = 0, y = 0; y < length; y++, vi++)
        {
            for (int x = 0; x < width; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + width + 1;
                triangles[ti + 5] = vi + width + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        length = GetNewLength();
    }

    void Update()
    {
        Generate();
    }
    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(MousePOS, 0.1f);
    }
}
