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

        public List<Coord> allTileCoords;
        Queue<Coord> shuffledTileCoords;

        public int seed = 10;

        public Transform[] regionPrefabs = new Transform[2];

        private void Start()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            allTileCoords = new List<Coord>();
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    allTileCoords.Add(new Coord(x, y));

                }
            }
            shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

            GenerateTerrain();

            string holderName = "Generated Map";
            if (transform.Find(holderName))
            {
                DestroyImmediate(transform.Find(holderName).gameObject);
            }

            Transform mapHolder = new GameObject(holderName).transform;
            mapHolder.parent = transform;

            foreach(Coord i in allTileCoords)
            {
                Vector3 tilePosition = CoordToPosition(i.x, i.y);
                int rand = Random.Range(0, regionPrefabs.Length);
                Transform newTile = Instantiate(regionPrefabs[rand], tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                i.Tile = newTile;
                i.tilePrefab = regionPrefabs[rand];
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }           

        }
        void GenerateTerrain()
        {
            foreach (Coord i in allTileCoords)
            {
                System.Random rng = new System.Random();
                //Determine Region Type, This can be refined and changed at a later date if desired
                i.regionType = rng.Next(1, 9);
                //select prefab
            }
        }
        Vector3 CoordToPosition(int x, int y)
        {
            return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
        }
        public Coord GetRandomCoord()
        {
            Coord randomCoord = shuffledTileCoords.Dequeue();
            shuffledTileCoords.Enqueue(randomCoord);
            return randomCoord;
        }

        public void InitPlayer(GridPlayerState controller)
        {
            int x = Random.Range(0, (int)mapSize.x);
            int y = Random.Range(0, (int)mapSize.y);
            Coord location = allTileCoords.Find(i => i.x == x && i.y == y);
            controller.transform.position = location.Tile.transform.position;
        }

        [System.Serializable]
        public class Coord
        {
            public int x;
            public int y;
            public int regionType;
            public Transform tilePrefab;
            public Transform Tile;

            public Coord(int _x, int _y)
            {
                x = _x;
                y = _y;
                regionType = 0;
                //this line may cause problems vvv
            }
        }
    }

}