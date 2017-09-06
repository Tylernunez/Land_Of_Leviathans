using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats{
    [Header("Base Power")]
    public int hp = 100;
    public int fp = 100;
    public int stamina = 100;
    public float equipLoad = 20;
    public float poise = 20;
    public int itemDiscovery = 100;

    [Header("Attack Power")]
    public int R_weapon_1 = 50;
    public int R_weapon_2 = 50;
    public int R_weapon_3 = 50;
    public int L_weapon_1 = 50;
    public int L_weapon_2 = 50;
    public int L_weapon_3 = 50;

    [Header("Defense")]
    public int physical = 100;
    public int vs_strike = 100;
    public int vs_slash = 100;
    public int vs_thrust = 100;
    public int magic = 30;
    public int fire = 30;
    public int lightning = 30;
    public int dark = 30;

    [Header("Resistances")]
    public int bleed = 100;
    public int poison = 100;
    public int frost = 100;
    public int curse = 100;

    public int attunementSlots = 1;
}
[System.Serializable]
public class Attributes
{
    public int level = 1;
    public int vigor = 10;
    public int attunement = 10;
    public int endurance = 10;
    public int vitality = 10;
    public int strength = 10;
    public int dexterity = 10;
    public int intelligence = 10;
    public int faith = 10;
    public int luck = 10;
}
[System.Serializable]
public class WeaponStats
{
    public int physical;
    public int strike;
    public int slash;
    public int thrust;
    public int magic = 0;
    public int fire = 0;
    public int lightning = 0;
    public int dark = 0;
}
