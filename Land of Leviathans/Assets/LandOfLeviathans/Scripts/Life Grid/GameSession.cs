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
        public Village village;
        public SessionToken token;
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
            token = FindObjectOfType<SessionToken>();
            worldGenerator = GetComponentInChildren<MapGenerator>();
            controller = FindObjectOfType<GridPlayerState>();
            controller.Init();
            worldGenerator.GenerateMap();
            worldGenerator.EstablishBoundaries();
            worldGenerator.InitPlayer(controller);
            village.Init();
           
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
            if(location.hasStructure || location.hasInhabitants)
            {
                //Unload LifeGrid
                session.SetActive(false);
                //Load scene according to structure/inhabitants
                token.location = location;
                token.inOpenField = true;
                SceneManager.LoadScene("testTile");
                
            }
            if (location.isVillage)
            {
                session.SetActive(false);
                token.location = location;
                token.inVillage = true;
                token.village = village;
                SceneManager.LoadScene("village");
            }
        }

    }

    [System.Serializable]
    public class Clock
    {
        public int hour;
        public int day;
        public int week;

        public void Tick()
        {

        }

        public void Tick(int hours)
        {
            this.hour += hours;
            GameSession.singleton.controller.Tick();
        }

        public void TrackTime()
        {
            if(this.hour >= 24)
            {
                this.day++;
                this.hour = 0;
            }
            if(this.day >= 7)
            {
                this.week++;
                this.day = 0;
            }
        }
    }
}

