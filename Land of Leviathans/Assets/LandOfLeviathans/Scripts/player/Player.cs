using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Camera cam;
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
    public Interactable focus;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
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
    void Update()
    {
        playerPosition = this.transform.position;

        

        //if ray hits
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }

            }
            else
            {
                RemoveFocus();
            }
        }
        
	}
    private void OnLevelWasLoaded(int level)
    {
        if (gameMaster.instance.LastUsedDoor != null)
        {
            GameObject search = GameObject.Find(gameMaster.instance.EntryDoor);
            Transform spawn = search.transform.GetChild(0);
            this.transform.position = spawn.transform.position;
            Transform graphic = this.transform.GetChild(0);
            this.transform.RotateAround(spawn.transform.position,Vector3.up,0.0f);
            
            RemoveFocus();
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null; 
    }
 
}
