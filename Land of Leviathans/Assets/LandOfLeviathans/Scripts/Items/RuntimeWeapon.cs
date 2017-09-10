using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class RuntimeWeapon : MonoBehaviour
    {
        public bool isUnarmed;
        public Weapon instance;
        public GameObject weaponModel;
        public WeaponHook w_hook;
        public WeaponStats weaponStats;
    }
}
