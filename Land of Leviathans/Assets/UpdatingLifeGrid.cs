using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoL
{



    public class UpdatingLifeGrid : MonoBehaviour
    {

        public GameObject playerData;
        public Text Region;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int x = GameSession.singleton.controller.xPos;
            int y = GameSession.singleton.controller.yPos;
            MapGenerator.Tile location = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
            Region.text = "Region: " + determineRegion(location.regionType);
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

    }
}
