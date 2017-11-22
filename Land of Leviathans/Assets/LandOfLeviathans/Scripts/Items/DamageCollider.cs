using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class DamageCollider : MonoBehaviour
    {
        StateManager states;
        NPCstates estates;

        public void InitPlayer(StateManager st)
        {
            states = st;
            gameObject.layer = 9;
            gameObject.SetActive(false);
        }

        public void InitEnemy(NPCstates st)
        {
            estates = st;
            gameObject.layer = 9;
            gameObject.SetActive(false);
        }

        void OnTriggerEnter(Collider other)
        {
            if (states)
            {
                NPCstates es = other.transform.GetComponentInParent<NPCstates>();

                if (es != null)
                {

                    es.DoDamage(states.currentAction,
                        states.inventoryManager.GetCurrentWeapon(states.currentAction.mirror)
                        );
                }

                StateManager st = other.transform.GetComponentInParent<StateManager>();

                if (st != null)
                {
                    if (st != states)
                    {
                        st.DoDamage(states.currentAction,
                        states.inventoryManager.GetCurrentWeapon(states.currentAction.mirror)
                        );
                    }
                }

                return;
            }

            if (estates)
            {
                //Debug.Log("v");

                StateManager st = other.transform.GetComponentInParent<StateManager>();

                if (st != null)
                {
                    //      Debug.Log("v");
                    st.DoDamage(estates.GetCurAttack());
                }
            }
        }
    }
}
