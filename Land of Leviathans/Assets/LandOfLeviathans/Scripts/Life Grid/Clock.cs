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
                GameSession.singleton.controller.Tick();
                GameSession.singleton.dm.Tick();
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
