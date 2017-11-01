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
                        World[i, j].Init(0);
                    }
                    else if(j == 0)
                    {
                        World[i, j].Init(0);
                    }
                    else if (j == --worldSize)
                    {
                        World[i, j].Init(0);
                    }
                    else if (i == --worldSize)
                    {
                        World[i, j].Init(0);
                    }
                    else
                    {
                        World[i, j].North = World[--i, j];
                        World[i, j].South = World[i, ++j];
                        World[i, j].East = World[++i, j];
                        World[i, j].West = World[i, --j];
                        World[i, j].Init();
                    }
                    
                    
                }
            }
        }

    }
}

