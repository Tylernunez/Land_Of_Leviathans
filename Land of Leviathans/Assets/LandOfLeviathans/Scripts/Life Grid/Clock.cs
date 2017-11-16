using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{

    public class Clock : MonoBehaviour
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

                

                int chanceToSpawnMonster = Random.Range(0, 30);
                int chanceToSpawnMerchant = Random.Range(0, 20);
                if (chanceToSpawnMerchant == 1)
                {
                GameSession.singleton.dm.SpawnMerchant();
                }
                if (chanceToSpawnMonster == 1)
                {
                GameSession.singleton.dm.SpawnMonster();
                }
            }

            public void TrackTime()
            {
                if (this.hour >= 24)
                {
                    this.day++;
                    this.hour = 0;
                }
                if (this.day >= 7)
                {
                    this.week++;
                    this.day = 0;
                }
            }
        }
    }
