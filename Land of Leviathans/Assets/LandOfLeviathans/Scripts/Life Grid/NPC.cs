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


        System.Random rng = new System.Random();


        public void Init(bool isMonster,bool isMerchant)
        {
            if (isMonster)
            {
                this.isMonster = true;
                int r = rng.Next(GameSession.singleton.monsterNames.Count);
                this.name = ((string)GameSession.singleton.monsterNames[r]);

                this.health = rng.Next(20, 100);
                this.food = rng.Next(4, 10);
                this.movespeed = rng.Next(1, 3);
            }
            if (isMerchant)
            {
                this.isMerchant = true;
                int r = rng.Next(GameSession.singleton.menNames.Count);
                this.name = ((string)GameSession.singleton.menNames[r]);

                this.health = rng.Next(20, 100);
                this.food = rng.Next(4, 10);
                this.movespeed = rng.Next(1, 3);
            }
        }

        public void updateBehavior()
        {
            if (isMonster)
            {
                updateMonsterMovement();
            }
            if (isMerchant)
            {
                updateMerchantMovement();
            }
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

        public void updateMonsterMovement()
        {
            this.transform.Translate(0, 1, 0, Camera.main.transform);
            Debug.Log("up!");
        }
        public void updateMerchantMovement()
        {
            this.transform.Translate(0, 1, 0, Camera.main.transform);
            Debug.Log("up!");
        }
    }
}