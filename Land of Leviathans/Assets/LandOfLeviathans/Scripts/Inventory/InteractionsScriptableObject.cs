using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class InteractionsScriptableObject : ScriptableObject
    {
        public Interactions[] interactions;
    }

    [System.Serializable]
    public class Interactions
    {
        public string interactionId;
        public string anim;
        public bool oneShot;
        public string specialEvent;
    }
}
