using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MouseInput
{
    public class click_for_point : MonoBehaviour
    {
        public static Vector3 get_mouse_position(Vector3 last_click_pos)
        {
            Vector3 click_position = last_click_pos;
            Vector3 raw_mouse_position = Input.mousePosition;
            Vector3 mouse_position = Camera.main.ScreenToWorldPoint(raw_mouse_position);
            mouse_position.z = 0;

            if (mouse_position != click_position)
            {
                click_position = mouse_position;
            }

            return click_position;
        }
    }
}
