using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{

    public class NPC : MonoBehaviour
    {
        public int xPos;
        public int yPos;
        //Seems a bit subjective. :thinking:
        public bool isMonster;
        public bool isFeral;

        public bool isMerchant;

        public string name;
        public int alignment;

        public int health;
        public int food;
        public int gold;
        public int movespeed;

        public List<string> monsterNames;
        public List<string> menNames;


        System.Random rng = new System.Random();


        public void Init(bool isMonster,bool isMerchant)
        {
            if (isMonster)
            {
                int r = rng.Next(monsterNames.Count);
                string name = ((string)monsterNames[r]);

                this.health = rng.Next(20, 100);
                this.food = rng.Next(4, 10);
                this.movespeed = rng.Next(1, 3);
            }
            if (isMerchant)
            {
                int r = rng.Next(menNames.Count);
                string name = ((string)menNames[r]);

                this.health = rng.Next(20, 100);
                this.food = rng.Next(4, 10);
                this.movespeed = rng.Next(1, 3);
            }
        }

        public void updateBehavior()
        {

        }

        public void preventMovement(MapGenerator.Tile location)
        {
            /*
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
            */
        }
    }
}