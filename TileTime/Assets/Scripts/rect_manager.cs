using MouseInput;
using System.Collections.Generic;
using UnityEngine;
using MathStuff;

[RequireComponent(typeof(click_for_point))]
public class rect_manager : MonoBehaviour
{

    #region variables
    //draw handles for a visual representation of the problem
    public bool draw_debug = true;
    public bool every_frame = false;

    //the rect we will be testing
    public RectMaker.Rect my_rect;

    //the size of the rect
    [Range(1f, 50f)]
    public float rect_scale = 25f;

    //side of the handles 
    [Range(1f, 10f)]
    public float handle_scale = 1f;

    //center point of the rect
    public Vector3 rect_center;

    //color to draw the rect, using collider green becaue we're basically remaking the 2d collider system at this point so why not
    public Color rect_color = Color.green;


    //if we have made an initial click or not, just hides the handle until we have valid input
    bool clicked = false;

    //position to test
    public Vector3 click_position;
    public Vector3 intersection_point;

    public float distance_from_click;
    public bool click_is_inside_rect;


    #region points to draw
    // vector3s we create from the RectMaker.Rect's min/max X/Y and scale
    Vector3 p0;
    Vector3 p1;
    Vector3 p2;
    Vector3 p3;
    Vector3 handle_size;
    #endregion

    #endregion

    #region unity methods
    void Start()
    {
        rect_center = Vector3.zero;
        click_position = Vector3.zero;
        NewRect();
    }

    void Update()
    {
        if(every_frame)
        {
            click_position = click_for_point.get_mouse_position(click_position);
            Check_conditions();
        }
        else
        {
            Check_For_Input();
        }

        Update_Points();
    }

    void OnDrawGizmos()
    {
        if (draw_debug)
        {
            if (Application.isPlaying)
            {
                Internal_Point();
                Draw_Handles();
                Draw_Rect_Lines();
                Draw_Intersection();
            }
        }
    }

    #endregion

    #region custom methods
    void Check_For_Input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click_position = click_for_point.get_mouse_position(click_position);

            if (!clicked)
            {
                clicked = true;
            }

            Check_conditions();
        }
    }

    void Check_conditions()
    {
        KeyValuePair<bool, Vector3> results;
        click_is_inside_rect = find_intersection.InsideClick(click_position, my_rect);

        if (!click_is_inside_rect)
        {
            results = find_intersection.Intersection_Point(click_position, my_rect);
            if(results.Key)
            {
                intersection_point = results.Value;
            }

            distance_from_click = Vector3.Distance(click_position, intersection_point);
        }
        else
        {
            distance_from_click = 0;
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

        //update handle sizes live
        handle_size = new Vector3(handle_scale, handle_scale, handle_scale);
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

        if (clicked && !every_frame)
        {
            Gizmos.DrawCube(click_position, handle_size);
        }
        else
        {
            if (every_frame)
            {
                Gizmos.DrawCube(click_position, handle_size);
            }
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

    void Draw_Rect_Lines()
    {
        #region default rect
        Gizmos.color = rect_color;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
        #endregion

        find_intersection.side side = find_intersection.NearSide(click_position, my_rect);

        if (side == find_intersection.side.left)
        {
            #region draw left side red, all others rect_color
            Gizmos.color = Color.red;
            Gizmos.DrawLine(p0, p1);

            Gizmos.color = rect_color;
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p0);
            #endregion
        }

        if (side == find_intersection.side.top)
        {
            #region draw top side red, all others rect_color
            Gizmos.color = Color.red;
            Gizmos.DrawLine(p1, p2);

            Gizmos.color = rect_color;
            Gizmos.DrawLine(p0, p1);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p0);
            #endregion
        }

        if (side == find_intersection.side.right)
        {
            #region draw right side red, all others rect_color
            Gizmos.color = Color.red;
            Gizmos.DrawLine(p2, p3);

            Gizmos.color = rect_color;
            Gizmos.DrawLine(p0, p1);
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p3, p0);
            #endregion
        }

        if (side == find_intersection.side.bottom)
        {
            #region draw bottom side red, all others rect_color
            Gizmos.color = Color.red;
            Gizmos.DrawLine(p3, p0);

            Gizmos.color = rect_color;
            Gizmos.DrawLine(p0, p1);
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            #endregion
        }
    }

    void Draw_Intersection()
    {
        KeyValuePair<bool, Vector3> results;

        if (!find_intersection.InsideClick(click_position, my_rect))
        {
            results = find_intersection.Intersection_Point(click_position, my_rect);

            intersection_point = results.Value;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(intersection_point, handle_size);
            Gizmos.DrawLine(click_position, intersection_point);
        }
    }
    #endregion
}
