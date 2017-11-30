using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoL
{

    public class GridUI : MonoBehaviour
    {

        public GridPlayerState playerData;
        public Text Region;
        public Text Health;
        public Text Hunger;
        public Text Energy;
        public Text Gold;
        public GameObject Trading;
        public GameObject villageUI;
        public GameObject gameOver;
        // Use this for initialization
        void Start()
        {
            Region.color = Color.black;
            Health.color = Color.black;
            Hunger.color = Color.black;
            Energy.color = Color.black;
            Gold.color = Color.black;
        }

        public void Init(GridPlayerState data)
        {
            playerData = data;
        }


        // Update is called once per frame
        void Update()
        {
            int x = GameSession.singleton.controller.xPos;
            int y = GameSession.singleton.controller.yPos;
            MapGenerator.Tile location = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
            Region.text = "Region: " + determineRegion(location.regionType);
            Health.text = "Health: " + playerData.health;
            Hunger.text = "Food: " + playerData.food;
            Energy.text = "Energy: " + playerData.energy;
            Gold.text = "Gold: " + playerData.gold;
            if (playerData.food < 6)
            {
                Hunger.color = Color.red;
            }
            else
            {
                Hunger.color = Color.black;
            }
            if (playerData.health < 11)
            {
                Health.color = Color.red;
            }
            else
            {
                Health.color = Color.black;
            }
            if (playerData.energy < 10)
            {
                Energy.color = Color.red;
            }
            else
            {
                Energy.color = Color.black;
            }
        }

        public void EnableTrading()
        {
            Trading.SetActive(true);
        }

        public void DisableTrading()
        {
            Trading.SetActive(false);
        }
        public void EnableVillage()
        {
            villageUI.SetActive(true);
        }

        public void DisableVillage()
        {
            villageUI.SetActive(false);
        }

        public string determineRegion(int region)
        {
            int caseSwitch = region;
            switch (caseSwitch)
            {
                case 0:
                    return "Desert";
                case 1:
                    return "Forest";
                case 2:
                    return "Grassland";
                case 3:
                    return "Hills";
                case 4:
                    return "Mesa";
                case 5:
                    return "Mountains";
                case 6:
                    return "Ocean";
                case 7:
                    return "Tundra";
            }
            return "oops";
        }


        public void GameOver()
        {
            gameOver.SetActive(true);
        }

    }
}
