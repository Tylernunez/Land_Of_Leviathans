using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoL
{
    public class GameSession : MonoBehaviour
    {
        public MapGenerator worldGenerator;
        public Calendar time;
        public GridPlayerState controller;
        public Clock clock;
        public DM dm;
        GameObject session;

        public static GameSession singleton;
        void Awake()
        {
            session = GameObject.FindGameObjectWithTag("GameSession");
            singleton = this;
            DontDestroyOnLoad(this);
        }

        public void Init()
        {
            worldGenerator = GetComponentInChildren<MapGenerator>();
            clock = GetComponentInChildren<Clock>();
            dm = GetComponentInChildren<DM>();
            controller = FindObjectOfType<GridPlayerState>();
            controller.Init();
            worldGenerator.GenerateMap();
            worldGenerator.EstablishBoundaries();
            worldGenerator.InitPlayer(controller);
            
        }

        public void Start()
        {
            Init();
        }
        public void Update()
        {
            controller.inputhandler.GetInput();
            controller.inputhandler.updateMovement();
            controller.inputhandler.TileInteract();
            worldGenerator.PoliceBoundaries(controller);
            clock.TrackTime();
        }

        public void GenerateLocation()
        {
            MapGenerator.Tile location = worldGenerator.allTileCoords.Find(i => i.x == controller.xPos && i.y == controller.yPos);
            if(location.hasStructure)
            {
                //Unload LifeGrid
                session.SetActive(false);
                //Load scene according to structure/inhabitants
                SceneManager.LoadScene("testTile");
            }
        }

    }
}

