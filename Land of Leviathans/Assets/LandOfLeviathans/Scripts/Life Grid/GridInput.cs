using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class GridInput : MonoBehaviour
    {
        int x;
        int y;

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
        public bool one;
        public bool two;
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
            one = Input.GetKeyDown("1");
            two = Input.GetKeyDown("2");
            space = Input.GetKeyDown(KeyCode.Space);
        }

        public void updateMovement()
        {
            if (!restrictUp && w && states.energy > 0)
            {
                x = states.xPos;
                y = states.yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByPlayer = false;
                transform.Translate(0, 1, 0, Camera.main.transform);
                states.xPos -= 1;
                x = states.xPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByPlayer = true;
                w = false;
                GameSession.singleton.clock.Tick(1);
            }
            if (!restrictLeft && a && states.energy > 0)
            {
                x = states.xPos;
                y = states.yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByPlayer = false;
                transform.Translate(-1, 0, 0, Camera.main.transform);
                states.yPos -= 1;
                y = states.yPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByPlayer = true;
                a = false;
                GameSession.singleton.clock.Tick(1);
            }
            if (!restrictDown && s && states.energy > 0)
            {
                x = states.xPos;
                y = states.yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByPlayer = false;
                transform.Translate(0, -1, 0, Camera.main.transform);
                states.xPos += 1;
                x = states.xPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByPlayer = true;
                s = false;
                GameSession.singleton.clock.Tick(1);
            }
            if (!restrictRight && d && states.energy > 0)
            {
                x = states.xPos;
                y = states.yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByPlayer = false;
                transform.Translate(1, 0, 0, Camera.main.transform);
                states.yPos += 1;
                y = states.yPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByPlayer = true;
                d = false;
                GameSession.singleton.clock.Tick(1);
            }
            restrictUp = false;
            restrictDown = false;
            restrictRight = false;
            restrictLeft = false;
        }

        public void updateAction()
        {
            int food = states.food;
            int health = states.health;
            int energy = states.energy;

            if (one && food < 21 && states.energy > 0) //forage - should change to be matched with the player's "strength" possibly
            {
                if (states.food == 20)
                {
                    Debug.Log("cant forage food is full");
                }
                if (states.food < 20)
                {
                    states.food = food + 2;
                    GameSession.singleton.clock.Tick(1);
                }
                if (states.food > 20) // might not be necessary
                {
                    states.food = 20;
                }
            }
            if (two && health < 101 && states.food > 0) //rest - gives 10 health plus full energy
            {
                if (states.health == 100 && energy != 50)
                {
                    states.energy = energy + 11;
                    GameSession.singleton.clock.Tick(8);
                }
                if (states.health < 100)
                {
                    states.health = health + 10;
                    states.energy = energy + 11;
                    GameSession.singleton.clock.Tick(8);
                }
                if (states.health > 100) //meant to limit max
                {
                    states.health = 100;
                }
                if (states.energy > 50)
                {
                    states.energy = 50;
                }
            }
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
