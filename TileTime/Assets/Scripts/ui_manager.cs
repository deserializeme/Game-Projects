using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

[RequireComponent(typeof(rect_manager))]
public class ui_manager : MonoBehaviour
{
    public TextMeshProUGUI distance_value;
    public TextMeshProUGUI point_value;
    public TextMeshProUGUI i_point_value;
    public TextMeshProUGUI inside_value;

    private float _distance;
    float distance
    {
        get
        {
            return _distance;
        }

        set
        {
            _distance = value;
            distance_value.SetText("{0:2}", _distance);
        }
    }

    private Vector3 _pos;
    Vector3 pos
    {
        get
        {
            return _pos;
        }

        set
        {
            _pos = value;
            point_value.SetText("( {0:1}, {1:1}, {2:1} )", rm.click_position.x, rm.click_position.y, rm.click_position.z);
        }
    }

    private Vector3 _ipos;
    Vector3 ipos
    {
        get
        {
            return _ipos;
        }

        set
        {
            _ipos = value;
            i_point_value.SetText("( {0:1}, {1:1}, {2:1} )", rm.intersection_point.x, rm.intersection_point.y, rm.intersection_point.z);
        }
    }

    private bool _inside;
    bool inside
    {
        get
        {
            return _inside;
        }

        set
        {
            _inside = value;
            inside_value.SetText(rm.click_is_inside_rect.ToString());
        }
    }

    rect_manager rm;

    void Start()
    {
        rm = gameObject.GetComponent<rect_manager>();
        inside = rm.click_is_inside_rect;
    }

    void Update()
    {
        compare_values();
    }

    void compare_values()
    {
        if(distance != rm.distance_from_click)
        {
            distance = rm.distance_from_click;
        }

        if(pos != rm.click_position)
        {
            pos = rm.click_position;
        }

        if (ipos != rm.intersection_point)
        {
            ipos = rm.intersection_point;
        }

        if (inside != rm.click_is_inside_rect)
        {
            inside = rm.click_is_inside_rect;
        }

    }
}
