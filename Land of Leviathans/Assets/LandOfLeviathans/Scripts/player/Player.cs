using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 playerPosition;
    public GameObject Body { get;set; }
    public String Name;
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

    // Use this for initialization
    void Start () {
		
	}
    private void Awake()
    {
        //default vitals
        Health = 10;
        Stamina = 10;
        Mana = 10;
        //default attributes
        Strength = 3;
        Endurance = 3;
        Dexterity = 3;
        Agility = 3;
        Intelligence = 3;
        Wisdom = 3;
        Charisma = 3;
    }
	// Update is called once per frame
	void Update () {
        playerPosition = this.transform.position;
	}
    private void OnLevelWasLoaded(int level)
    {
        if (gameMaster.instance.LastUsedDoor != null)
        {
            GameObject search = GameObject.Find(gameMaster.instance.EntryDoor);
            door entry = search.GetComponent<door>();
            this.transform.position = entry.spawnPoint.transform.position;
            this.transform.rotation = entry.spawnPoint.transform.rotation;
        }
    }

 
}
