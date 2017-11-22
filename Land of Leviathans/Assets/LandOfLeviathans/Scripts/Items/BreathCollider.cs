using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class BreathCollider : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            NPCstates es = other.GetComponentInParent<NPCstates>();
            if(es != null)
            {
                es.DoDamage_();
                SpellEffectsManager.singleton.UseSpellEffect("onFire", null, es);
            }
        }
    }
}
