using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{

    public class DM : MonoBehaviour
    {
        public GameObject Monster;
        public GameObject Merchant;

        public void SpawnMonster()
        {
            MapGenerator.Tile spawnLocation = CheckForOccupancy();
            if (spawnLocation != null)
            {
                int x = (int)spawnLocation.x;
                int y = (int)spawnLocation.y;
                Vector3 npcPosition = GameSession.singleton.worldGenerator.CoordToPosition(x, y);
                GameObject newNPC = Instantiate(Monster, npcPosition, Quaternion.Euler(Vector3.right * 90));
                newNPC.transform.position = spawnLocation.tile.transform.position;
                spawnLocation.isOccupiedByNPC = true;
            }
        }

        public void SpawnMerchant()
        {
            MapGenerator.Tile spawnLocation = CheckForOccupancy();
            if (spawnLocation != null)
            {
                int x = (int)spawnLocation.x;
                int y = (int)spawnLocation.y;
                Vector3 npcPosition = GameSession.singleton.worldGenerator.CoordToPosition(x, y);
                GameObject newNPC = Instantiate(Merchant, npcPosition, Quaternion.Euler(Vector3.right * 90));
                newNPC.transform.position = spawnLocation.tile.transform.position;
                spawnLocation.isOccupiedByNPC = true;
                MerchantMovement(x, y);
            }
        }

        public void MerchantMovement(int x, int y)
        {
            //some kind of movement handling, need to reference THIS merchant somehow
        }

        public MapGenerator.Tile CheckForOccupancy()
        {
            int x = Random.Range(0, (int)GameSession.singleton.worldGenerator.mapSize.x);
            int y = Random.Range(0, (int)GameSession.singleton.worldGenerator.mapSize.y);
            MapGenerator.Tile location = GameSession.singleton.worldGenerator.allTileCoords.Find(i => i.x == x && i.y == y);
            if (location.isOccupiedByNPC || location.isOccupiedByPlayer)
            {
                return null;
            }
            return location;
        }

    }

}
