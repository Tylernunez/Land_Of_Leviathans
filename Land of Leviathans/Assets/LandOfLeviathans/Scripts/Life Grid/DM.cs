using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{

    public class DM : MonoBehaviour
    {
        public GameObject Monster;
        public GameObject Merchant;
        public List<GameObject> NPCs;


        public void SpawnMonster()
        {
            MapGenerator.Tile spawnLocation = CheckForOccupancy();
            if (spawnLocation != null && spawnLocation.isBoundary)
            {
                Vector3 npcPosition = GameSession.singleton.worldGenerator.CoordToPosition(spawnLocation.x, spawnLocation.y);
                GameObject newNPC = Instantiate(Monster, npcPosition, Quaternion.Euler(Vector3.right * 90));
                newNPC.transform.position = spawnLocation.tile.transform.position;
                spawnLocation.isOccupiedByNPC = true;
                NPC monster = newNPC.GetComponent<NPC>();
                monster.Init(true,false);
                NPCs.Add(newNPC);
            }
        }

        public void SpawnMerchant()
        {
            MapGenerator.Tile spawnLocation = CheckForOccupancy();
            if (spawnLocation != null && spawnLocation.isBoundary)
            {
                Vector3 npcPosition = GameSession.singleton.worldGenerator.CoordToPosition(spawnLocation.x, spawnLocation.y);
                GameObject newNPC = Instantiate(Merchant, npcPosition, Quaternion.Euler(Vector3.right * 90));
                newNPC.transform.position = spawnLocation.tile.transform.position;
                spawnLocation.isOccupiedByNPC = true;
                NPC monster = newNPC.GetComponent<NPC>();
                monster.Init(false,true);
                NPCs.Add(newNPC);
            }
        }

        public void Tick()
        {
            int chanceToSpawnMonster = Random.Range(0, 30);
            int chanceToSpawnMerchant = Random.Range(0, 20);
            if (chanceToSpawnMerchant == 1)
            {
                SpawnMerchant();
            }
            if (chanceToSpawnMonster == 1)
            {
                SpawnMonster();
            }
            foreach (GameObject i in NPCs)
            {
                NPC npc = i.GetComponent<NPC>();
                npc.updateBehavior();
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
