﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class WeaponHook : MonoBehaviour
    {
        public GameObject[] damageCollider;

        public void OpenDamageColliders()
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(true);
            }
        }

        public void CloseDamageColliders()
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(false);
            }
        }

        public void InitDamageColliders(StateManager st)
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(false);
            }
        }
    }
}
