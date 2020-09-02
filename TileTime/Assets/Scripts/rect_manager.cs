using MouseInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathStuff;
using RectMaker;

[RequireComponent(typeof(click_for_point))]
public class rect_manager : MonoBehaviour
{

    #region variables
    //draw handles for a visual representation of the problem
    public bool draw_debug = true;

    //the rect we will be testing
    public RectMaker.Rect my_rect;

    //the size of the rect
    public float rect_scale = 1;
    
    //side of the handles 
    public float handle_scale = .2f;
    
    //center point of the rect
    public Vector3 rect_center;

    //color to draw the rect, using collider green becaue we're basically remaking the 2d collider system at this point so why not
    public Color rect_color = Color.green;


    //if we have made an initial click or not, just hides the handle until we have valid input
    bool clicked = false;

    //position to test
    public Vector3 click_position;


    #region points to draw
    // vector3s we create from the RectMaker.Rect's min/max X/Y and scale
    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;
    Vector3 handle_size;
    #endregion

    #endregion

    void Start()
    {
        rect_center = Vector3.zero;
        click_position = Vector3.zero;
        handle_size = new Vector3(handle_scale, handle_scale, handle_scale);
        NewRect();
    }

    void Update()
    {
        Check_For_Input();
        Update_Points();
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Internal_Point();
            Draw_Handles();
            Draw_Lines();
        }
    }

    void Check_For_Input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click_position = click_for_point.get_mouse_position(click_position);

            if (!clicked)
            {
                clicked = true;
            }
        }
    }

    void Update_Points()
    {
        //update the scale if we want to
        if (my_rect.scale != rect_scale)
        {
            NewRect();
        }

        //update the position if we want to
        if (my_rect.center != rect_center)
        {
            NewRect();
        }

        //update the color if we want to
        if (my_rect.color != rect_color)
        {
            NewRect();
        }
    }

    void NewRect()
    {
        my_rect = new RectMaker.Rect(rect_scale, rect_color, rect_center);
        p0 = new Vector3(my_rect.minX, my_rect.minY, 0) + rect_center;
        p1 = new Vector3(my_rect.minX, my_rect.maxY, 0) + rect_center;
        p2 = new Vector3(my_rect.maxX, my_rect.maxY, 0) + rect_center;
        p3 = new Vector3(my_rect.maxX, my_rect.minY, 0) + rect_center;
    }

    void Internal_Point()
    {
        if (find_intersection.InsideClick(click_position, my_rect))
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.white;
        }

        if (clicked)
        {
            Gizmos.DrawCube(click_position, handle_size);
        }
    }

    void Draw_Handles()
    {
        Gizmos.color = my_rect.color;
        //bottom left
        Gizmos.DrawCube(p0, handle_size);
        //top left
        Gizmos.DrawCube(p1, handle_size);
        //top right
        Gizmos.DrawCube(p2, handle_size);
        //bottom right
        Gizmos.DrawCube(p3, handle_size);

        Gizmos.DrawCube(rect_center, handle_size);
    }

    void Draw_Lines()
    {
        MathStuff.find_intersection.side side = find_intersection.NearSide(click_position, my_rect);

        if(side == find_intersection.side.left)
        {

        }

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
}
