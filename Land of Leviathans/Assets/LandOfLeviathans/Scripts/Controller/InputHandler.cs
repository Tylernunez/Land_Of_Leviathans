using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class InputHandler : MonoBehaviour
    {
        float vertical;
        float horizontal;
        bool b_input;
        bool a_input;
        bool x_input;
        bool y_input;

        bool rb_input;
        float rt_axis;
        bool rt_input;
        bool lb_input;
        float lt_axis;
        bool lt_input;

        float d_y;
        float d_x;
        bool d_up;
        bool d_down;
        bool d_right;
        bool d_left;

        bool p_d_up;
        bool p_d_down;
        bool p_d_left;
        bool p_d_right;

        bool leftAxis_down;
        bool rightAxis_down;

        float b_timer;
        float rt_timer;
        float lt_timer;

        public bool disableInput;

        StateManager states;
        CameraManager camManager;
        UIManager uiManager;
        DialogManager dialogueManager;
        LoL.UI.InventoryUI invUI;

        bool isGesturesOpen;

        float delta;

        public static InputHandler singleton;
        void Awake()
        {
            singleton = this;
        }

        void Start()
        {
            UI.QuickSlot.singleton.Init();

            states = GetComponent<StateManager>();
            states.Init();

            camManager = CameraManager.singleton;
            camManager.Init(states);

            uiManager = UIManager.singleton;
            invUI = LoL.UI.InventoryUI.singleton;
            invUI.Init(states.inventoryManager);

            dialogueManager = DialogManager.singleton;
        }

        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput();
            HandleUI();
            UpdateStates();
            states.FixedTick(delta);
            camManager.Tick(delta);
        }

        bool preferItem;

        void Update()
        {
            delta = Time.deltaTime;
            states.Tick(delta);


            if (invUI.isMenu)
            {
                uiManager.CloseAnnounceType();
                invUI.Tick();
            }
            /*
            else
            {
                if (!dialogueManager.dialogueActive)
                {
                    if (states.pickManager.itemCanidate != null || states.pickManager.interCandidate)
                    {
                        if (states.pickManager.itemCanidate && states.pickManager.interCandidate)
                        {
                            if (preferItem)
                            {
                                PickupItem();
                            }
                            else
                            {
                                Interact();
                            }
                        }
                        else
                        {

                            if (states.pickManager.itemCanidate && !states.pickManager.interCandidate)
                            {
                                PickupItem();
                            }

                            if (states.pickManager.interCandidate && states.pickManager.itemCanidate == null)
                            {
                                Interact();
                            }
                        }
                    }
                    else
                    {
                        uiManager.CloseAnnounceType();

                        if (a_input)
                        {
                            uiManager.CloseCards();
                            a_input = false;
                        }
                    }
                }
                else
                {
                    uiManager.CloseAnnounceType();
                }
            }
            */

            dialogueManager.Tick(a_input);
            states.MonitorStats();
            ResetInputNStates();

            uiManager.Tick(states.characterStats, delta);    
        }
        /*
        void PickupItem()
        {
                uiManager.OpenAnnounceType(UIActionType.pickup);

                if (a_input)
                {
                    Debug.Log("picked up");
                    Vector3 td = states.pickManager.itemCanidate.transform.position - transform.position;
                    states.SnapToRotation(td);
                    states.pickManager.PickCanidate();
                    states.PlayAnimation(StaticStrings.pick_up);
                    a_input = false;
                }
           
        }

        void Interact()
        {
                uiManager.OpenAnnounceType(states.pickManager.interCandidate.actionType);


                if (a_input)
                {
                    Debug.Log("interaction");
                    states.InteractLogic();
                    a_input = false;
                }
        }
        */
        void GetInput()
        {
            if (!disableInput)
            {
                vertical = Input.GetAxis(StaticStrings.Vertical);
                horizontal = Input.GetAxis(StaticStrings.Horizontal);

                //  if (invUI.isMenu)
                //{ 

                bool menu = Input.GetButtonUp(StaticStrings.start);

                if (menu)
                {
                    invUI.isMenu = !invUI.isMenu;

                    if (invUI.isMenu)
                    {
                        isGesturesOpen = false;
                        invUI.OpenUI();
                    }
                    else
                    {
                        invUI.CloseUI();
                    }
                }

                b_input = Input.GetButton(StaticStrings.B);
                a_input = Input.GetButtonUp(StaticStrings.A);
                y_input = Input.GetButtonUp(StaticStrings.Y);
                x_input = Input.GetButton(StaticStrings.X);
                rt_input = Input.GetButton(StaticStrings.RT);
                rt_axis = Input.GetAxis(StaticStrings.RT);
                if (rt_axis != 0)
                    rt_input = true;

                lt_input = Input.GetButton(StaticStrings.LT);
                lt_axis = Input.GetAxis(StaticStrings.LT);
                if (lt_axis != 0)
                    lt_input = true;
                rb_input = Input.GetButton(StaticStrings.RB);
                lb_input = Input.GetButton(StaticStrings.LB);

                leftAxis_down = Input.GetButtonUp(StaticStrings.L) || Input.GetKeyUp(KeyCode.Alpha6);
                rightAxis_down = Input.GetButtonUp(StaticStrings.R) || Input.GetKeyUp(KeyCode.T);

                if (b_input)
                    b_timer += delta;

                d_x = Input.GetAxis(StaticStrings.Pad_x);
                d_y = Input.GetAxis(StaticStrings.Pad_y);

                d_up = Input.GetKeyUp(KeyCode.Alpha1) || d_y > 0;
                d_down = Input.GetKeyUp(KeyCode.Alpha2) || d_y < 0;
                d_left = Input.GetKeyUp(KeyCode.Alpha3) || d_x < 0;
                d_right = Input.GetKeyUp(KeyCode.Alpha4) || d_x > 0;

                bool gesturesMenu = Input.GetButtonUp(StaticStrings.select);


                if (gesturesMenu)
                {
                    isGesturesOpen = !isGesturesOpen;
                }
            }

           

           
           
        }

        void HandleUI()
        {
            uiManager.gestures.HandleGestures(isGesturesOpen);

            if (isGesturesOpen)
                curUIstate = UIState.gestures;
            else
                curUIstate = UIState.game;

            if (invUI.isMenu)
            {
                curUIstate = UIState.inventory;
            }


            switch (curUIstate)
            {
                case UIState.game:
                    HandleQuickSlotChanges();
                    break;
                case UIState.gestures:
                    HandleGesturesUI();
                    break;
                case UIState.inventory:
                    break;
                default:
                    break;
            }

        }

        UIState curUIstate;
        enum UIState
        {
            game,gestures,inventory
        }

        void HandleGesturesUI()
        {
            if (d_left)
            {
                if (!p_d_left)
                {
                    uiManager.gestures.SelectGesture(false);
                    p_d_left = true;
                }
            }
            if (d_right)
            {
                if (!p_d_right)
                {
                    uiManager.gestures.SelectGesture(true);
                    p_d_right = true;
                }
            }

            if (!d_left)
                p_d_left = false;
            if (!d_right)
                p_d_right = false;

            if(a_input)
            {
                isGesturesOpen = false;
                states.usingItem = true;

                if(uiManager.gestures.closeWeapons)
                    states.closeWeapons = true;
               
                states.PlayAnimation(uiManager.gestures.gestureAnim, false);
            }
        }

        void UpdateStates()
        {
            if (!disableInput)
            {
                states.horizontal = horizontal;
                states.vertical = vertical;

                Vector3 v = vertical * camManager.transform.forward;
                Vector3 h = horizontal * camManager.transform.right;
                states.moveDir = (v + h).normalized;
                float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                states.moveAmount = Mathf.Clamp01(m);

                // if (invUI.isMenu)
                //   return;

                if (x_input)
                    b_input = false;

                if (b_input && b_timer > 0.5f)
                {
                    states.run = (states.moveAmount > 0) && states.characterStats._stamina > 0;
                }

                if (b_input == false && b_timer > 0 && b_timer < 0.5f)
                    states.rollInput = true;

                states.itemInput = x_input;
                states.rt = rt_input;
                states.lt = lt_input;
                states.rb = rb_input;
                states.lb = lb_input;

                if (y_input)
                {
                    //Two handing
                    /*
                    if (states.pickManager.itemCanidate && states.pickManager.interCandidate)
                    {
                        preferItem = !preferItem;
                    }
                    else
                    {
                        states.isTwoHanded = !states.isTwoHanded;
                        states.HandleTwoHanded();
                    }
                    */

                    states.Jump();
                }

                if (states.lockOnTarget != null)
                {
                    if (states.lockOnTarget.eStates.isDead)
                    {
                        states.lockOn = false;
                        states.lockOnTarget = null;
                        states.lockOnTransform = null;
                        camManager.lockon = false;
                        camManager.lockonTarget = null;
                    }
                }
                else
                {
                    states.lockOn = false;
                    states.lockOnTarget = null;
                    states.lockOnTransform = null;
                    camManager.lockon = false;
                    camManager.lockonTarget = null;
                }


                if (rightAxis_down)
                {
                    states.lockOn = !states.lockOn;
                    states.lockOnTarget = EnemyManager.singleton.GetEnemy(transform.position);

                    if (states.lockOnTarget == null)
                    {
                        states.lockOn = false;
                    }
                    else
                    {
                        camManager.lockonTarget = states.lockOnTarget;
                        states.lockOnTransform = states.lockOnTarget.GetTarget();
                        camManager.lockonTransform = states.lockOnTransform;
                        camManager.lockon = states.lockOn;
                    }
                }
            }

           
        }

        void HandleQuickSlotChanges()
        {
            if (states.isSpellcasting || states.usingItem)
                return;

            if (d_up)
            {
                if (!p_d_up)
                {
                    p_d_up = true;
                    states.inventoryManager.ChangeToNextSpell();
                }
            }

            if(d_down)
            {
                if(!p_d_down)
                {
                    p_d_down = true;
                    states.inventoryManager.ChangeToNextConsumable();
                }
            }

            if (!d_up)
                p_d_up = false;
            if (!d_down)
                p_d_down = false;

            if (states.onEmpty == false)
                return;
//            if (states.isTwoHanded)
  //              return;

            if (d_left)
            {
                if (!p_d_left)
                {
                    states.inventoryManager.ChangeToNextWeapon(true);
                    p_d_left = true;
                }
            }
            if (d_right)
            {
                if (!p_d_right)
                {
                    states.inventoryManager.ChangeToNextWeapon(false);
                    p_d_right = true;
                }
            }

         
            if (!d_left)
                p_d_left = false;
            if (!d_right)
                p_d_right = false;
        }

        void ResetInputNStates()
        {
            if (a_input)
                a_input = false;

            if (b_input == false)
                b_timer = 0;
            if (states.rollInput)
                states.rollInput = false;
            if (states.run)
                states.run = false;
        }
    }
}
