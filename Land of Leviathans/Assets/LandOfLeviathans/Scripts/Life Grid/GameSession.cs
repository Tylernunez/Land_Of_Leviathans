using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeGrid
{
    public class GameSession : MonoBehaviour
    {
        public Cell[,] World;
        public Calendar time;
        public int worldSize;

        public void Init()
        {
            //instantiate world cells
            World = new Cell[worldSize, worldSize];
            //Get Coordinates
            for(int i = 0; i < worldSize; i++)
            {
               for(int j = 0; j < worldSize; j++)
                {
                    if(i == 0)
                    {
                        World[i, j] = new Cell(0);
                    }
                    else if(j == 0)
                    {
                        World[i, j] = new Cell(0);
                    }
                    else if (j == worldSize - 1)
                    {
                        World[i, j] = new Cell(0);
                    }
                    else if (i == worldSize - 1)
                    {
                        World[i, j] = new Cell(0);
                    }
                    else
                    {
                        World[i, j] = new Cell();
                    }
                    
                    
                }
            }
            Debug.Log("Success! Here is your Grid Master.");
            for (int i = 0; i < worldSize; i++)
            {
                for (int j = 0; j < worldSize; j++)
                {
                    if(World[i,j].regionType == 0)
                    {

                    }
                    else
                    {
                        World[i, j].North = World[--i, j];
                        World[i, j].South = World[++i, j];
                        World[i, j].East = World[i, ++j];
                        World[i, j].West = World[i, --j];
                    }
                    
                }
            }

            foreach(Cell i in World)
            {
                Debug.Log(i.regionType);
            }
        }
        public void Start()
        {
            Init();
        }

    }
}

