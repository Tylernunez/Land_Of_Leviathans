using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class GameSession : MonoBehaviour
    {
        public MapGenerator worldGenerator;
        public Calendar time;

        public void Init()
        {
            worldGenerator = GetComponentInChildren<MapGenerator>();
            
        }
        public void Start()
        {
            Init();
        }

    }
}

