using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{

    public class NPC : MonoBehaviour
    {
        public int xPos;
        public int yPos;

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