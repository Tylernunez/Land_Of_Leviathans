using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class ParryCollider : MonoBehaviour
    {
        StateManager states;
        NPCstates eStates;

        public float maxTimer = 0.6f;
        float timer;

        public void InitPlayer(StateManager st)
        {
            states = st;
        }

        void Update()
        {
            if(states)
            {
                timer += states.delta;

                if(timer > maxTimer)
                {
                    timer = 0;
                    gameObject.SetActive(false);
                }
            }

            if(eStates)
            {
                timer += eStates.delta;

                if (timer > maxTimer)
                {
                    timer = 0;
                    gameObject.SetActive(false);
                }
            }
        }

        public void InitEnemy(NPCstates st)
        {
            eStates = st;
        }

        void OnTriggerEnter(Collider other)
        {
           // DamageCollider dc = other.GetComponent<DamageCollider>();
            //if (dc == null)
             //   return;

            if (states)
            {
                NPCstates e_st = other.transform.GetComponentInParent<NPCstates>();

                if (e_st != null)
                {
                    e_st.CheckForParry(transform.root, states);
                }
            }

            if(eStates)
            {
                //check for player
            }
        }
    }
}
