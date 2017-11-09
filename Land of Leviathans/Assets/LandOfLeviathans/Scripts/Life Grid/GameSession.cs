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
        public GridPlayerState controller;

        public void Init()
        {
            worldGenerator = GetComponentInChildren<MapGenerator>();
            controller = FindObjectOfType<GridPlayerState>();
            worldGenerator.InitPlayer(controller);
        }


        public void Start()
        {
            Init();
        }

    }
}

