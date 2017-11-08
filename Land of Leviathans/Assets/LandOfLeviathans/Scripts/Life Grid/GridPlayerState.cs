using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class GridPlayerState : MonoBehaviour
    {
        [Header("Init")]
        public GameObject activeModel;

        [Header("Inputs")]
        public float vertical;
        public float horizontal;

        public Vector3 moveDir;
        public bool rt, rb, lt, lb;

        [Header("Stats")]
        public float moveSpeed = 2;
        public float runSpeed = 3.5f;

        [Header("States")]
        public bool onGround;
        public bool run;
        public bool inAction;
        public bool canMove;
        public bool canRotate;
        public bool canAttack;
        public bool isSpellcasting;
        public bool usingItem;
        public bool isInvicible;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public AnimatorHook a_hook;
        [HideInInspector]
        public ActionManager actionManager;
        [HideInInspector]
        public InventoryManager inventoryManager;
        //[HideInInspector]
        //public BoneHelper boneHelper;
        [HideInInspector]
        public PickableItemsManager pickManager;

        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;
        [HideInInspector]
        public LayerMask ignoreForGroundCheck;

        [HideInInspector]
        public Action currentAction;

        [HideInInspector]
        public float airTimer;
        public ActionInput storePrevAction;
        public ActionInput storeActionInput;

        float _actionDelay;
        float _kickTimer;
        public bool canKick;
        public bool holdKick;
        public float moveAmount;
        public float kickMaxTime = 0.5f;
        public float moveAmountThresh = 0.05f;

        public bool enableItem;

        public void Init()
        {
            //Initialize Inventory
            //Retrieve character stats


        }


        public void FixedTick(float d)
        {
            delta = d;

            float targetSpeed = moveSpeed;

            if (onGround && canMove)
                rigid.velocity = moveDir * (targetSpeed * moveAmount);

        }

        public void Tick(float d)
        {
            delta = d;
        }

        public bool IsInput()
        {
            if (rt || rb || lt || lb )
                return true;

            return false;
        }

        public void InteractLogic()
        {
            //Use when interacting with something
        }

    }
}

