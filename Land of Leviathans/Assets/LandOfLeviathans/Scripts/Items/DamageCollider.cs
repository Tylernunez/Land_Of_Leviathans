using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class DamageCollider : MonoBehaviour
    {
        StateManager states;
        EnemyStates estates;

        public void InitPlayer(StateManager st)
        {
            states = st;
            gameObject.layer = 9;
            gameObject.SetActive(false);
        }

        public void InitEnemy(EnemyStates st)
        {
            estates = st;
            gameObject.layer = 9;
            gameObject.SetActive(false);
        }

        void OnTriggerEnter(Collider other)
        {
            if (states)
            {
                EnemyStates es = other.transform.GetComponentInParent<EnemyStates>();

                if (es != null)
                {
                        es.DoDamage(states.currentAction,
                   states.inventoryManager.GetCurrentWeapon(states.currentAction.mirror)
                   );
                }
                return;
            }

            if (estates)
            {
                StateManager st = other.transform.GetComponentInParent<StateManager>();

                if(st != null)
                {
                    Debug.Log("v");
                    st.DoDamage(estates.GetCurAttack());
                }
            }
           

        }
    }
}
