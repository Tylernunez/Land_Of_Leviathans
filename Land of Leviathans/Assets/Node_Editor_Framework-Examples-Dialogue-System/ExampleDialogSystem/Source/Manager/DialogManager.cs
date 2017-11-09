using System.Collections.Generic;
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
        NPCStates npc_state;
        public Transform playerObject;
        Transform origin;

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
            origin = HandleNPC();
            if (!dialogueActive)
                return;
            if (origin == null)
                return;

            float d = Vector3.Distance(playerObject.transform.position, origin.transform.position);
            if (d <= 6)
            {
                if (a_input)
                {
                    //Press selected button
                    //ShowDialogWithId(DialogId, true);
                }
            }

           
        }
        public Transform HandleNPC()
        {
            return null;
        }

        public void ShowDialogWithId(int dialogIdToLoad, bool goBackToBeginning)
        {
            if (_messageBoxes.ContainsKey(dialogIdToLoad))
            {
                return;
            }

            DialogNodeCanvas nodeCanvas;
            if (_dialogIdTracker.TryGetValue(dialogIdToLoad, out nodeCanvas))
            {
                nodeCanvas.ActivateDialog(dialogIdToLoad, goBackToBeginning);
            }
            else
                Debug.LogError("ShowDialogWithId Not found Dialog with ID : " + dialogIdToLoad);

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

