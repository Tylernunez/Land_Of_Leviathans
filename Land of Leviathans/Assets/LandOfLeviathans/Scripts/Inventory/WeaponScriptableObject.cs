﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class WeaponScriptableObject : ScriptableObject
    {
        public List<Weapon> weapons_all = new List<Weapon>();
        public List<WeaponStats> weaponStats = new List<WeaponStats>();
    }
}
