using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RectMaker;

namespace MathStuff
{
    public class find_intersection : MonoBehaviour
    {
        //sides of the rect as enums
        public enum side
        {
            right, 
            left,
            top,
            bottom,
            inside
        }

        #region find the line intersection with the closest side of the rect
        //math stuff 
        // formula of a line: Ax + By = C
        // A = Y1 - Y0
        // B = X0 - X1
        // C = Ax0 + By0
        // we know (x0, y0),(x1, y1) for line segment 0 *the closest side to the point
        // we know (x0, y0) for line segment 1, we need to find the direction towards the nearest side and the distance
        // the shortest distance between 2 points is a straight line, so we can just use right angles which make sit simpler
        /// <summary>
        /// Returns the Vector3 of the closest point on the rect
        /// </summary>
        /// <param name="point">A vector3.</param>
        /// <param name="my_rect">A RectMaker.Rect.</param>
        public static KeyValuePair<bool, Vector3> Intersection_Point(Vector3 test_point, RectMaker.Rect my_rect)
        {
            //get the closest side
            side closest_side;
            closest_side = NearSide(test_point, my_rect);

            #region hold points of our rect
            Vector3 p0 = new Vector3(my_rect.minX, my_rect.minY, 0) + my_rect.center;
            Vector3 p1 = new Vector3(my_rect.minX, my_rect.maxY, 0) + my_rect.center;
            Vector3 p2 = new Vector3(my_rect.maxX, my_rect.maxY, 0) + my_rect.center;
            Vector3 p3 = new Vector3(my_rect.maxX, my_rect.minY, 0) + my_rect.center;
            #endregion

            #region initialize some variables
            Vector3 intersection_location = Vector3.zero;
            bool is_on_line = false;
            Vector3 seg1_dir = Vector3.zero;
            Vector3 seg1_start = test_point;
            Vector3 difference = Vector3.zero;
            float single_axis_distance = 0;
            float seg0_A = 0;
            float seg0_B = 0;
            float seg0_C = 0;
            Vector3 side_start = p0;
            Vector3 side_end = p0;
            #endregion

            //chose the direction based on the returned side, then get the single axis distance
            // kind of cheating but acceptance criteria said shortest distance.        
            #region handle each side of the rect
            if (closest_side == side.left)
            {
                side_start = p0;
                side_end = p1;

                seg1_dir = Vector3.right;
                difference = side_start - test_point;
                single_axis_distance = Mathf.Abs(difference.x);
            }

            if (closest_side == side.top)
            {
                side_start = p1;
                side_end = p2;

                seg1_dir = -Vector3.up;
                difference = side_start - test_point;
                single_axis_distance = Mathf.Abs(difference.y);
            }

            if (closest_side == side.right)
            {
                side_start = p2;
                side_end = p3;

                seg1_dir = -Vector3.right;
                difference = side_start - test_point;
                single_axis_distance = Mathf.Abs(difference.x);
            }

            if (closest_side == side.bottom)
            {
                side_start = p3;
                side_end = p0;

                seg1_dir = Vector3.up;
                difference = side_start - test_point;
                single_axis_distance = Mathf.Abs(difference.y);
            }
            #endregion

            //get our lines in the proper format
            seg0_A = side_end.y - side_start.y;
            seg0_B = side_start.x - side_end.x;
            seg0_C = ((seg0_A * side_start.x) + (seg0_B * side_start.y));

            //now that we have the direction and distance for the last line segment we can get the interseation point
            intersection_location = seg1_start + (seg1_dir.normalized * single_axis_distance);


            KeyValuePair<bool, Vector3> test = On_Line(side_start, side_end, seg1_dir, intersection_location);
            if (!test.Key)
            {
                intersection_location = test.Value;
                is_on_line = false;
            }
            else
            {
                is_on_line = true;
            }

            KeyValuePair<bool, Vector3> results = new KeyValuePair<bool, Vector3>(is_on_line, intersection_location);

            return results;
        }
        #endregion

        #region is intersection point on a line segment and if not, return closest end of the line segment
        public static KeyValuePair<bool, Vector3> On_Line(Vector3 seg_start, Vector3 seg_end, Vector3 seg_direction, Vector3 test_point)
        {
            bool is_on_segment = false;
            Vector3 closest_point;

            float segment = Mathf.Sqrt(
                (seg_end.x - seg_start.x) * (seg_end.x - seg_start.x) 
                + (seg_end.y - seg_start.y) * (seg_end.y - seg_start.y) 
                + (seg_end.z - seg_start.z) * (seg_end.z - seg_start.z)
                );

            float start_to_point = Mathf.Sqrt(
                (test_point.x - seg_start.x) * (test_point.x - seg_start.x)
                + (test_point.y - seg_start.y) * (test_point.y - seg_start.y)
                + (test_point.z - seg_start.z) * (test_point.z - seg_start.z));

            float point_to_end = Mathf.Sqrt(
                (seg_end.x - test_point.x) * (seg_end.x - test_point.x)
                + (seg_end.y - test_point.y) * (seg_end.y - test_point.y)
                + (seg_end.z - test_point.z) * (seg_end.z - test_point.z));

            //Debug.Log("segent: " + segment + " start-to-point: " + start_to_point + " point-to-end: " + point_to_end);
            //Debug.Log("segent: " + segment + " vs start-to-point + point-to-end: " + Mathf.Abs(start_to_point + point_to_end));

            //low precision, could get this lower with more time to test but it passes all the tests im throwing at it
            if(Mathf.Abs(segment - (start_to_point + point_to_end)) < .5f )
            {
                is_on_segment = true;
                closest_point = test_point;
            }
            else
            {
                if(start_to_point > point_to_end)
                {
                    closest_point = seg_end;
                }
                else
                {
                    closest_point = seg_start;
                }
            }

            KeyValuePair<bool, Vector3> results = new KeyValuePair<bool, Vector3>(is_on_segment, closest_point);

            //Debug.Log(is_on_segment);
            return results;
        }

        #endregion

        #region find the closest side of the rect to an outside point
        /// <summary>
        /// Returns the name of the nearest side of the rect to the supplied Vector3 point as an Enum
        /// </summary>
        /// <param name="point">A vector3.</param>
        /// <param name="my_rect">A RectMaker.Rect.</param>
        public static side NearSide(Vector3 point, RectMaker.Rect my_rect)
        {
            side nearest_side = side.inside;

            if(!InsideClick(point, my_rect))
            {
                if (point.x > my_rect.maxX + my_rect.center.x)
                {
                    //right
                    nearest_side = side.right;
                }

                if (point.x < my_rect.minX + my_rect.center.x)
                {
                    //left
                    nearest_side = side.left;
                }

                if (point.y > my_rect.maxY + my_rect.center.y)
                {
                    //above
                    nearest_side = side.top;
                }

                if (point.y < my_rect.minY + my_rect.center.y)
                {
                    //below
                    nearest_side = side.bottom;
                }
            }


            return nearest_side;
        }
        #endregion 

        #region Is the point inside the rect? 
        /// <summary>
        /// Returns true if point is inide rect, and false if it is outside the rect.
        /// </summary>
        /// <param name="point">A vector3.</param>
        /// <param name="my_rect">A RectMaker.Rect.</param>
        public static bool InsideClick(Vector3 point, RectMaker.Rect my_rect)
        {
            bool inside = true;

            if (point.x > my_rect.maxX + my_rect.center.x)
            {
                //right
                inside = false;
            }

            if (point.x < my_rect.minX + my_rect.center.x)
            {
                //left
                inside = false;
            }

            if (point.y > my_rect.maxY + my_rect.center.y)
            {
                //above
                inside = false;
            }

            if (point.y < my_rect.minY + my_rect.center.y)
            {
                //below
                inside = false;
            }

            return inside;
        }
        #endregion

    }

}