﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    [System.Serializable]
    public class CharacterStats
    {
        [Header("Current")]
        public float _health;
        public float _focus;
        public float _stamina;
        public int _souls;

        public float _healthRecoverValue = 60;
        public float _focusRecoverValue = 80;
   
        [Header("Base Power")]
        public int hp = 100;
        public int fp = 100;
        public int stamina = 100;
        public float equipLoad = 20;
        public float poise = 20;
        public int itemDiscover = 111;

        [Header("Attack Power")]
        public int R_weapon_1 = 51;
        public int R_weapon_2 = 51;
        public int R_weapon_3 = 51;
        public int L_weapon_1 = 51;
        public int L_weapon_2 = 51;
        public int L_weapon_3 = 51;

        [Header("Defence")]
        public int physical = 87;
        public int vs_strike = 87;
        public int vs_slash = 87;
        public int vs_thrust = 87;
        public int magic = 30;
        public int fire = 30;
        public int lighting = 30;
        public int dark = 30;

        [Header("Resistances")]
        public int bleed = 100;
        public int poison = 100;
        public int frost = 100;
        public int curse = 100;

        public int attunemntSlots = 0;

        public void InitCurrent()
        {
            if (statEffects != null)
            {
                statEffects();
            }

       //     _health = hp;
         //   _focus = fp;
           // _stamina = stamina;
   
        }

        public delegate void StatEffects();
        public StatEffects statEffects;

        public void AddHealth()
        {
            hp += 5;
        }

        public void RemoveHealth()
        {
            hp -= 5;
        }
    }

    public enum AttributeType
    {
        level,
        vigor,
        attunement,
        endurance,
        vitality,
        strength,
        dexterity,
        intelligence,
        faith,
        luck,
        hp,
        fp,
        stamina,
        equip_load,
        poise,
        item_discovery,
        attunement_slots

    }

    public enum AttackDefenseType
    {
        physical,magic,fire,lighting,dark,critical,stability,bleed,curse,frost,magicBuff,
        strike,slash,thrust,poison
    }

    public enum WeaponDamageType
    {
        sum, vs_strike, vs_slash, vs_thrust
    }

    [System.Serializable]
    public class Attributes
    {
        public int level = 1;
        public int souls = 0;
        public int vigor = 11;
        public int attunement = 11;
        public int endurance = 11;
        public int vitality = 11;
        public int strength = 11;
        public int dexterity = 11;
        public int intelligence = 11;
        public int faith = 11;
        public int luck = 11;
    }

    [System.Serializable]
    public class WeaponStats
    {
        public string weaponId;
        public int a_physical;
        public int a_strike;
        public int a_slash;
        public int a_thrust;
        public int a_magic = 0;
        public int a_fire = 0;
        public int a_lighting = 0;
        public int a_dark = 0;
        public int a_frost;
        public int a_curse;
        public int a_poison;
        public int critical;

        public float d_physical;
        public float d_strike;
        public float d_slash;
        public float d_thrust;
        public float d_magic;
        public float d_fire;
        public float d_lighting;
        public float d_dark;
        public float d_frost;
        public float d_curse;
        public float d_poison;
        public float stability;


        public string weaponType;
        public string damageType;
        public string skillName;
        public float weightCost = 5;
        public float maxDurability = 80;
    }

    
}
