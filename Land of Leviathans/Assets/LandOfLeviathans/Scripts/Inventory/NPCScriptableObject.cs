using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class NPCScriptableObject : ScriptableObject
    {
        public NPCDialogue[] npc;
    }

    [System.Serializable]
    public class NPCDialogue
    {
        public string npc_id;
        public Dialogue[] dialogue;

    }
    
    [System.Serializable]
    public class Dialogue
    {
        public string[] dialogText;
        public string specialEvent;
        public bool increaseIndex;
        public string targetAnim;
        public bool playAnim;
    }
    
}
