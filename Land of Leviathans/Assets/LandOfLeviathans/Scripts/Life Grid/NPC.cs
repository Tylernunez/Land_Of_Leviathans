using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{

    public class NPC : MonoBehaviour
    {
        int x;
        int y;

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


        public void Init(bool isMonster,bool isMerchant, int x, int y)
        {
            if (isMonster)
            {
                this.isMonster = true;
                int r = rng.Next(GameSession.singleton.monsterNames.Count);
                this.name = ((string)GameSession.singleton.monsterNames[r]);

                this.health = rng.Next(20, 100);
                this.food = rng.Next(4, 10);
                this.movespeed = rng.Next(1, 3);
                this.xPos = x;
                this.yPos = y;
            }
            if (isMerchant)
            {
                this.isMerchant = true;
                int r = rng.Next(GameSession.singleton.menNames.Count);
                this.name = ((string)GameSession.singleton.menNames[r]);

                this.health = rng.Next(20, 100);
                this.food = rng.Next(4, 10);
                this.movespeed = rng.Next(1, 3);
                this.xPos = x;
                this.yPos = y;
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
            int r = rng.Next(1, 12);
            
            if (r == 1) //up
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMonster = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(0, 1, 0, Camera.main.transform);
                xPos -= 1;
                x = xPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMonster = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r == 2) //left
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMonster = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(-1, 0, 0, Camera.main.transform);
                yPos -= 1;
                y = yPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMonster = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r == 3) //down
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMonster = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(0, -1, 0, Camera.main.transform);
                xPos += 1;
                x = xPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMonster = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r == 4) //right
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMonster = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(1, 0, 0, Camera.main.transform);
                yPos += 1;
                y = yPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMonster = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r >= 5)
            {
                this.food = this.food + 2; //eh?
            }
        }
        public void updateMerchantMovement()
        {
            int r = rng.Next(1, 12);

            if (r == 1) //up
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMerchant = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(0, 1, 0, Camera.main.transform);
                xPos -= 1;
                x = xPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMerchant = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r == 2) //left
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMerchant = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(-1, 0, 0, Camera.main.transform);
                yPos -= 1;
                y = yPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMerchant = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r == 3) //down
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMerchant = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(0, -1, 0, Camera.main.transform);
                xPos += 1;
                x = xPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMerchant = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r == 4) //right
            {
                x = xPos;
                y = yPos;
                MapGenerator.Tile prevLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                prevLocation.isOccupiedByMerchant = false;
                prevLocation.isOccupiedByNPC = false;
                this.transform.Translate(1, 0, 0, Camera.main.transform);
                yPos += 1;
                y = yPos;
                MapGenerator.Tile newLocation = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
                newLocation.isOccupiedByMerchant = true;
                newLocation.isOccupiedByNPC = true;
            }
            if (r >= 5)
            {
                this.food = this.food + 2; //que?
            }
        }
    }
}