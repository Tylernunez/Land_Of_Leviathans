using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class MapGenerator : MonoBehaviour
    {

        public Transform tilePrefab;
        public Vector2 mapSize;

        [Range(0, 1)]
        public float outlinePercent;

        public List<Tile> allTileCoords;
        Queue<Tile> shuffledTileCoords;

        public int seed = 10;
        System.Random rng = new System.Random();
        public int StructureChance = 10;


        public Transform[] regionPrefabs = new Transform[8];


        public void GenerateMap()
        {
            allTileCoords = new List<Tile>();
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    allTileCoords.Add(new Tile(x, y));

                }
            }

            string holderName = "Generated Map";
            if (transform.Find(holderName))
            {
                DestroyImmediate(transform.Find(holderName).gameObject);
            }

            Transform mapHolder = new GameObject(holderName).transform;
            mapHolder.parent = transform;

            shuffledTileCoords = new Queue<Tile>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

            Camera Maincam = Camera.main.GetComponent<Camera>();
            Maincam.orthographicSize = (mapSize.x / 2) + 1;

            GenerateTerrain();

            foreach(Tile i in allTileCoords)
            {
                Vector3 tilePosition = CoordToPosition(i.x, i.y);
                Transform newTile = Instantiate(regionPrefabs[i.regionType], tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                i.tile = newTile;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }           

        }
        void GenerateTerrain()
        {
            foreach (Tile i in allTileCoords)
            {
                
                //Determine Region Type, This can be refined and changed at a later date if desired
                i.regionType = rng.Next(1, 8);
                if(rng.Next(1,100) <= StructureChance)
                {
                    i.hasStructure = true;
                    i.structureType = rng.Next(1, 5);
                }
                //select prefab
            }
        }
        Vector3 CoordToPosition(int x, int y)
        {
            return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
        }
        public Tile GetRandomCoord()
        {
            Tile randomCoord = shuffledTileCoords.Dequeue();
            shuffledTileCoords.Enqueue(randomCoord);
            return randomCoord;
        }

        public void EstablishBoundaries()
        {
            foreach(Tile i in allTileCoords)
            {
                if(i.x == 0)
                {
                    i.isBoundary = true;
                    i.isNorth = true;
                }
                if (i.y == mapSize.y - 1)
                {
                    i.isBoundary = true;
                    i.isEast = true;
                }
                if(i.y == 0)
                {
                    i.isBoundary = true;
                    i.isWest = true;
                }
                if(i.x == mapSize.x - 1)
                {
                    i.isBoundary = true;
                    i.isSouth = true;
                }
            }
        }

        public void PoliceBoundaries(GridPlayerState controller)
        {
            
            Tile location = allTileCoords.Find(i => i.x == controller.xPos && i.y == controller.yPos);
            if (location.isBoundary)
            {
                controller.preventMovement(location);
            }
        }

        public void InitPlayer(GridPlayerState controller)
        {
            int x = Random.Range(0, (int)mapSize.x);
            int y = Random.Range(0, (int)mapSize.y);
            Tile location = allTileCoords.Find(i => i.x == x && i.y == y);
            controller.transform.position = location.tile.transform.position;
            controller.xPos = x;
            controller.yPos = y;
        }

        [System.Serializable]
        public class Tile
        {
            public int x;
            public int y;
            public int regionType;
            public int structureType;
            public Transform tile;
            public bool isBoundary = false;
            public bool isNorth = false;
            public bool isSouth = false;
            public bool isEast = false;
            public bool isWest = false;
            public bool hasStructure;
            public bool hasInhabitants;

            public Tile(int _x, int _y)
            {
                x = _x;
                y = _y;
                regionType = 0;
                
            }
        }
    }

}