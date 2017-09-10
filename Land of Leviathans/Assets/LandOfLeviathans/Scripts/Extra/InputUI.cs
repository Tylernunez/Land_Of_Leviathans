using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputUI : MonoBehaviour
    {
        public float vertical;
        public float horizontal;
        public bool b_input, a_input, y_input, x_input;
        float d_x, d_y;
        public bool d_up, d_down, d_left, d_right;
        public bool rightAxis_down;

        public void Tick()
        {
            GetInput();
        }

        void GetInput()
        {
            vertical = Input.GetAxis(StaticStrings.Vertical);
            horizontal = Input.GetAxis(StaticStrings.Horizontal);
            b_input = Input.GetButtonUp(StaticStrings.B);
            a_input = Input.GetButtonUp(StaticStrings.A);
            y_input = Input.GetButtonUp(StaticStrings.Y);
            x_input = Input.GetButtonUp(StaticStrings.X);
          
            d_x = Input.GetAxis(StaticStrings.Pad_x);
            d_y = Input.GetAxis(StaticStrings.Pad_y);

            d_up = Input.GetKeyUp(KeyCode.Alpha1) || d_y > 0;
            d_down = Input.GetKeyUp(KeyCode.Alpha2) || d_y < 0;
            d_left = Input.GetKeyUp(KeyCode.Alpha3) || d_x < 0;
            d_right = Input.GetKeyUp(KeyCode.Alpha4) || d_x > 0;

            rightAxis_down = Input.GetButtonUp(StaticStrings.R) || Input.GetKeyUp(KeyCode.T);
        }

        public static InputUI singleton;
        void Awake()
        {
            singleton = this;
        }
    }
}
