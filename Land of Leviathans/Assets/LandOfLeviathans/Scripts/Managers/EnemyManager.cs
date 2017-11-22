using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class EnemyManager : MonoBehaviour
    {
        public List<NPCtargets> enemyTargets = new List<NPCtargets>();

        public NPCtargets GetEnemy(Vector3 from)
        {
            NPCtargets r = null;
            float minDist = float.MaxValue;
            for (int i = 0; i < enemyTargets.Count; i++)
            {
                float tDist = Vector3.Distance(from, enemyTargets[i].GetTarget().position);
                if(tDist < minDist)
                {
                    minDist = tDist;
                    r = enemyTargets[i];
                }
            }

            return r;
        }


        public static EnemyManager singleton;
        void Awake()
        {
            singleton = this;
        }
    }
}
