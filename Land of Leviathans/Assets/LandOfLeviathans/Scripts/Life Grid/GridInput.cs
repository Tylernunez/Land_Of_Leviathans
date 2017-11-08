using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class GridInput : MonoBehaviour
    {
        float vertical;
        float horizontal;
        bool b_input;
        bool a_input;
        bool x_input;
        bool y_input;

        bool rb_input;
        float rt_axis;
        bool rt_input;
        bool lb_input;
        float lt_axis;
        bool lt_input;

        float d_y;
        float d_x;
        bool d_up;
        bool d_down;
        bool d_right;
        bool d_left;

        bool p_d_up;
        bool p_d_down;
        bool p_d_left;
        bool p_d_right;

        bool leftAxis_down;
        bool rightAxis_down;

        float b_timer;
        float rt_timer;
        float lt_timer;

        GridPlayerState states;

        bool isGesturesOpen;

        float delta;

        public static GridInput singleton;
        void Awake()
        {
            singleton = this;
        }

        void Start()
        {
            states = GetComponent<GridPlayerState>();

        }

        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput();
            HandleUI();
        }


        void Update()
        {
            delta = Time.deltaTime;
           
        }

        

        void Interact()
        {
            //Announce

            if (a_input)
            {
                Debug.Log("interaction");
                //InteractLogic
                a_input = false;
            }
        }

        void GetInput()
        {
            vertical = Input.GetAxis(StaticStrings.Vertical);
            horizontal = Input.GetAxis(StaticStrings.Horizontal);


            bool menu = Input.GetButtonDown(StaticStrings.start);

            if (menu)
            {
                //if in menu
                    //open UI
                
                //else not in menu
                    //close UI 
            }

            b_input = Input.GetButton(StaticStrings.B);
                a_input = Input.GetButtonUp(StaticStrings.A);
                y_input = Input.GetButtonUp(StaticStrings.Y);
                x_input = Input.GetButton(StaticStrings.X);
                rt_input = Input.GetButton(StaticStrings.RT);
                rt_axis = Input.GetAxis(StaticStrings.RT);
                if (rt_axis != 0)
                rt_input = true;

                lt_input = Input.GetButton(StaticStrings.LT);
                lt_axis = Input.GetAxis(StaticStrings.LT);
                if (lt_axis != 0)
                    lt_input = true;
                rb_input = Input.GetButton(StaticStrings.RB);
                lb_input = Input.GetButton(StaticStrings.LB);

                leftAxis_down = Input.GetButtonUp(StaticStrings.L) || Input.GetKeyUp(KeyCode.Alpha6);
                rightAxis_down = Input.GetButtonUp(StaticStrings.R) || Input.GetKeyUp(KeyCode.T);

                if (b_input)
                    b_timer += delta;

                d_x = Input.GetAxis(StaticStrings.Pad_x);
                d_y = Input.GetAxis(StaticStrings.Pad_y);

                d_up = Input.GetKeyUp(KeyCode.Alpha1) || d_y > 0;
                d_down = Input.GetKeyUp(KeyCode.Alpha2) || d_y < 0;
                d_left = Input.GetKeyUp(KeyCode.Alpha3) || d_x < 0;
                d_right = Input.GetKeyUp(KeyCode.Alpha4) || d_x > 0;

                bool gesturesMenu = Input.GetButtonUp(StaticStrings.select);
           
        }

        void HandleUI()
        {
            //if UI state is in menu
            /*
            if (invUI.isMenu)
            {
                curUIstate = UIState.inventory;
            }
            */
            //Handle UI States
            /*
            switch (curUIstate)
            {
                case UIState.game:
                    break;
                case UIState.inventory:
                    break;
                default:
                    break;
            }
            */
        }

        UIState curUIstate;
        enum UIState
        {
            game,inventory
        }


        void UpdateStates()
        {   
            //Handle Movement for state manager, may not be valid for Grid purposes
            
            Vector3 v = vertical * camManager.transform.forward;
            Vector3 h = horizontal * camManager.transform.right;
            states.moveDir = (v + h).normalized;
            float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            
            //use m to move character

        }
        //Reset Input, may not be valid for Grid purposes
        /*
        void ResetInputNStates()
        {
            if (a_input)
                a_input = false;

            if (b_input == false)
                b_timer = 0;
            if (states.rollInput)
                states.rollInput = false;
            if (states.run)
                states.run = false;
        }
        */
    }
}
