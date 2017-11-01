using System.Collections;
using System.Collections.Generic;
using System;

namespace LifeGrid
{
    public class Cell
    {
        public Cell North;
        public Cell South;
        public Cell East;
        public Cell West;
        public int regionType;
        public Resource localResource;
        public bool isDifficultTerrain;
        public Object Inhabitants;

        public void Init()
        {
            Random rng = new Random();
            //Determine Region Type, This can be refined and changed at a later date if desired
            this.regionType = rng.Next(1, 9);


        }
        public void Init(int regionType)
        {
            this.regionType = regionType;
        }
    }
}

