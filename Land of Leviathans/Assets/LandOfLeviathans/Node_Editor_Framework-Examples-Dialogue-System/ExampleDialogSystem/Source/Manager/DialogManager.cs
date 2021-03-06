﻿using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class DialogManager : MonoBehaviour
    {
        private Dictionary<int, DialogNodeCanvas> _dialogIdTracker;

        [SerializeField]
        private GameObject _messageBoxPrefab;
        private Dictionary<int, MessageBoxHud> _messageBoxes;

        [SerializeField]
        private RectTransform _canvasObject;

        public DialogNodeCanvas dialogCanvas;
        public static DialogManager singleton;

        public bool dialogueActive;
        public Transform playerObject;
        GameObject origin;
        float talkDistance = 5f;
        bool resetDict;

        public void Init(Transform po)
        {
            playerObject = po;
        }

        public void Awake()
        {

            singleton = this;
            _messageBoxes = new Dictionary<int, MessageBoxHud>();
            _dialogIdTracker = new Dictionary<int, DialogNodeCanvas>();
            _dialogIdTracker.Clear();

            if (dialogCanvas)
            {
                foreach (int id in dialogCanvas.GetAllDialogId())
                {
                    _dialogIdTracker.Add(id, dialogCanvas);
                }
            }
            else
            {
                foreach (DialogNodeCanvas nodeCanvas in Resources.LoadAll<DialogNodeCanvas>("Saves/"))
                {
                    foreach (int id in nodeCanvas.GetAllDialogId())
                    {
                        _dialogIdTracker.Add(id, nodeCanvas);
                    }
                }
            }
        }

        public void Tick(bool a_input)
        {

            if (a_input)
            {
                origin = HandleNPC();
                if (origin == null)
                    return;
                NPC npc = origin.GetComponent<NPC>();

                if (!dialogueActive)
                {
                    if (npc)
                    {
                            InputHandler input = playerObject.GetComponent<InputHandler>();
                            input.disableInput = true;
                            dialogueActive = true;
                            //resetDict = true;
                            ShowDialogWithId(npc.DialogId, true);   
                    }
                }
               
                    
            }

           
        }
        public GameObject HandleNPC()
        {
            GameObject[] closeNPC = GameObject.FindGameObjectsWithTag("NPC");
            GameObject closestNPC = null;
            foreach (GameObject g in closeNPC)
            {
                if (!closestNPC)
                {
                    closestNPC = g;
                }
                //compare distances
                if (Vector3.Distance(transform.position, g.transform.position) <= Vector3.Distance(transform.position, closestNPC.transform.position))
                {
                    closestNPC = g;
                }

            }
            if (!closestNPC)
            {
                return null;
            }
            if (Vector3.Distance(playerObject.position, closestNPC.transform.position) <= talkDistance)
            {
                return closestNPC;
            }


            return null;
        }

        public void ShowDialogWithId(int dialogIdToLoad, bool goBackToBeginning)
        {


            DialogNodeCanvas nodeCanvas;
            if (_dialogIdTracker.TryGetValue(dialogIdToLoad, out nodeCanvas))
            {
                nodeCanvas.ActivateDialog(dialogIdToLoad, goBackToBeginning);
            }

            MessageBoxHud messageBox = GameObject.Instantiate(_messageBoxPrefab).GetComponent<MessageBoxHud>();
            messageBox.Construct(dialogIdToLoad, this);
            messageBox.transform.SetParent(_canvasObject, false);
            messageBox.SetData(GetNodeForId(dialogIdToLoad));
            _messageBoxes.Add(dialogIdToLoad, messageBox);
        }

        private BaseDialogNode GetNodeForId(int dialogIdToLoad)
        {
            DialogNodeCanvas nodeCanvas;
            if (_dialogIdTracker.TryGetValue(dialogIdToLoad, out nodeCanvas))
            {
                return nodeCanvas.GetDialog(dialogIdToLoad);
            }
            else
                Debug.LogError("getNodeForId Not found Dialog with ID : " + dialogIdToLoad);
            return null;
        }

        private void GiveInputToDialog(int dialogIdToLoad, int inputValue)
        {
            DialogNodeCanvas nodeCanvas;
            if (_dialogIdTracker.TryGetValue(dialogIdToLoad, out nodeCanvas))
            {
                nodeCanvas.InputToDialog(dialogIdToLoad, inputValue);
            }
            else
                Debug.LogError("GiveInputToDialog Not found Dialog with ID : " + dialogIdToLoad);
        }

        public void OkayPressed(int dialogId)
        {
            GiveInputToDialog(dialogId, (int)EDialogInputValue.Next);
            _messageBoxes[dialogId].SetData(GetNodeForId(dialogId));
        }

        public void BackPressed(int dialogId)
        {
            GiveInputToDialog(dialogId, (int)EDialogInputValue.Back);
            _messageBoxes[dialogId].SetData(GetNodeForId(dialogId));
        }

        public void RemoveMessageBox(int dialogId)
        {
            _messageBoxes.Remove(dialogId);
            InputHandler input = playerObject.GetComponent<InputHandler>();
            input.disableInput = false;
            dialogueActive = false;
        }

        public void OptionSelected(int dialogId, int optionSelected)
        {
            GiveInputToDialog(dialogId, optionSelected);
            _messageBoxes[dialogId].SetData(GetNodeForId(dialogId));
        }
    }

    public enum EDialogInputValue
    {
        Next = -2,
        Back = -1,
    }
	
}

