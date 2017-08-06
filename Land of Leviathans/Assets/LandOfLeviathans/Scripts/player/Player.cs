using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 playerPosition;
    public GameObject Body { get;set; }

    // Use this for initialization
    void Start () {
		
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
