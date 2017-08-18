using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC : ScriptableObject
{

    new public string name = "New NPC";
    public int Health;
    public int Stamina;
    public int Mana;
    public int Strength;
    public int Endurance;
    public int Dexterity;
    public int Agility;
    public int Intelligence;
    public int Wisdom;
    public int Charisma;
    public Dialogue dialogue;


}
