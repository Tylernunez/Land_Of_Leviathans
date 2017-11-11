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

        public bool w;
        public bool a;
        public bool s;
        public bool d;
        bool space;

        public bool restrictUp;
        public bool restrictDown;
        public bool restrictRight;
        public bool restrictLeft;
        public bool onLocation;

        GridPlayerState states;

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

        public void GetInput()
        {
            w = Input.GetKeyDown("w");
            a = Input.GetKeyDown("a");
            s = Input.GetKeyDown("s");
            d = Input.GetKeyDown("d");
            space = Input.GetKeyDown(KeyCode.Space);
        }

        public void updateMovement()
        {
            if (!restrictUp && w)
            {
                transform.Translate(0, 1, 0, Camera.main.transform);
                states.xPos -= 1;
                w = false;
                GameSession.singleton.clock.Tick(1);
            }
            if (!restrictLeft && a)
            {
                transform.Translate(-1, 0, 0, Camera.main.transform);
                states.yPos -= 1;
                a = false;
                GameSession.singleton.clock.Tick(1);
            }
            if (!restrictDown && s)
            {
                transform.Translate(0, -1, 0, Camera.main.transform);
                states.xPos += 1;
                s = false;
                GameSession.singleton.clock.Tick(1);
            }
            if (!restrictRight && d)
            {
                transform.Translate(1, 0, 0, Camera.main.transform);
                states.yPos += 1;
                d = false;
                GameSession.singleton.clock.Tick(1);
            }
            restrictUp = false;
            restrictDown = false;
            restrictRight = false;
            restrictLeft = false;
        }

        public void TileInteract()
        {
            if (space)
            {
                //if (onLocation)
                //{
                    GameSession.singleton.GenerateLocation();
                //}
            }
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


       
    }
}
