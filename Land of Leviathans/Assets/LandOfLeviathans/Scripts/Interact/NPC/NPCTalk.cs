using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : Interactable {

    public NPC npc;

    public override void Interact()
    {
        base.Interact();
        FindObjectOfType<DialogueManager>().startDialogue(npc.dialogue);
    }
    
}
