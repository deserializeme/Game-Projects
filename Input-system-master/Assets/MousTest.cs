using UnityEngine;
using System.Collections;

public class MousTest : MonoBehaviour {

    InputManager Man;
    public bool Draw;

	// Update is called once per frame
    void Start()
    {
        Man = gameObject.GetComponent<InputManager>();
    }

	void Update () {

        TargetFromMouse();
        if (Man.Inputs["RotateCamera"].KBM.Active)
        {
            Draw = true;
        }
        else
        {
            Draw = false;
        }

    }

    public Vector3 TargetFromMouse()
    {
        Vector3 point = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            point = hit.point;
        }

        return point;
    }

    void OnDrawGizmos()
    {
        if (Draw)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(TargetFromMouse(), .2f);
        }
    }
}
