using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoL
{
    public class DialogueManager : MonoBehaviour
    {
        public Text dialogueText;
        public GameObject textObj;
        Transform origin;
        NPCDialogue npc_d;
        NPCStates npc_state;
        public bool dialogueActive;
        bool updateDialog;
        int lineIndex;
        public Transform playerObject;

        public void Init(Transform po)
        {
            playerObject = po;
        }

        public void InitDialogue(Transform o, string id)
        {
            origin = o;
            npc_d = ResourcesManager.singleton.GetDialogue(id);
            npc_state = SessionManager.singleton.GetNpcState(id);
            dialogueActive = true;
            textObj.SetActive(true);
            updateDialog = false;
            lineIndex = 0;
        }

        public void Tick(bool a_input)
        {
            if (!dialogueActive)
                return;
            if (origin == null)
                return;

            float d = Vector3.Distance(playerObject.transform.position, origin.transform.position);
            if(d > 6)
            {
                CloseDialogue();
            }


            if (!updateDialog)
            {
                updateDialog = true;
                dialogueText.text = npc_d.dialogue[npc_state.dialogueIndex].dialogText[lineIndex];

                if(npc_d.dialogue[npc_state.dialogueIndex].playAnim)
                {
                    Animator anim = origin.GetComponentInChildren<Animator>();
                    anim.Play(npc_d.dialogue[npc_state.dialogueIndex].targetAnim);
                }
            }

            if(a_input)
            {
                a_input = false;
                updateDialog = false;
                lineIndex++;
             
                if (lineIndex > npc_d.dialogue[npc_state.dialogueIndex].dialogText.Length-1)
                {
                    if(npc_d.dialogue[npc_state.dialogueIndex].increaseIndex)
                    {
                        npc_state.dialogueIndex++;

                        if(npc_state.dialogueIndex > npc_d.dialogue.Length-1)
                        {
                            npc_state.dialogueIndex = npc_d.dialogue.Length-1;
                        }
                    }

                    CloseDialogue();
                }
            }
        }

        void CloseDialogue()
        {
            dialogueActive = false;
            textObj.SetActive(false);
        }



        public static DialogueManager singleton;
        void Awake()
        {
            singleton = this;
            textObj.SetActive(false);
        }

    }
}
