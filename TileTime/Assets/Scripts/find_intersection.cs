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


        #region find the closest side of the rect to an outside point
        /// <summary>
        /// Returns the name of the nearest side of the rect to the supplied Vector3 point as an Enum
        /// </summary>
        /// <param name="point">A vector3.</param>
        /// <param name="my_rect">A RectMaker.Rect.</param>
        public static side NearSide(Vector3 point, RectMaker.Rect my_rect)
        {
            side nearest_side = side.inside;

            if(InsideClick(point, my_rect))
            {
                if (point.x > my_rect.maxX)
                {
                    //right
                    nearest_side = side.right;
                }

                if (point.x < my_rect.minX)
                {
                    //left
                    nearest_side = side.left;
                }

                if (point.y > my_rect.maxY)
                {
                    //above
                    nearest_side = side.top;
                }

                if (point.y < my_rect.minY)
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

            if (point.x > my_rect.maxX)
            {
                //right
                inside = false;
            }

            if (point.x < my_rect.minX)
            {
                //left
                inside = false;
            }

            if (point.y > my_rect.maxY)
            {
                //above
                inside = false;
            }

            if (point.y < my_rect.minY)
            {
                //below
                inside = false;
            }

            return inside;
        }
        #endregion
    }

}