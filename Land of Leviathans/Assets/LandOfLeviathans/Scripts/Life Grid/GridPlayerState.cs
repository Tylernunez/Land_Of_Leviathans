using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class GridPlayerState : MonoBehaviour
    {
        [Header("Init")]
        public GameObject activeModel;

        [Header("Inputs")]

        public float vertical;
        public float horizontal;

        [Header("Stats")]
        //speed and such, character stats are stored in another file.

        [Header("States")]
        public int xPos;
        public int yPos;
        public int health;
        public int food;
        public int energy;
        public int gold;

        [HideInInspector]
        public InventoryManager inventoryManager;
        public GridInput inputhandler;

        public void Init()
        {
            inputhandler = this.GetComponent<GridInput>();
            health = 100;
            food = 20;
            energy = 50;
            gold = 100;
            //Initialize Inventory
            //Retrieve character stats


        }

        public void Tick()
        {
            if (energy >= 1)
            {
                --energy;
            }
            if(food >= 1)
            {
                --food;
            }
            else
            {
                health = health - 10;
                if (health < 0)
                {
                    health = 0;
                }
            }
            
        }


        public void preventMovement(MapGenerator.Tile location)
        {

            if (location.isNorth)
            {
                inputhandler.w = false;
                inputhandler.restrictUp = true;
            }
            if (location.isSouth)
            {
                inputhandler.s = false;
                inputhandler.restrictDown = true;
            }
            if (location.isEast)
            {
                inputhandler.d = false;
                inputhandler.restrictRight = true;
            }
            if (location.isWest)
            {
                inputhandler.a = false;
                inputhandler.restrictLeft = true;
            }  
        }

        public void InteractLogic()
        {
            //Use when interacting with something
        }

    }
}

