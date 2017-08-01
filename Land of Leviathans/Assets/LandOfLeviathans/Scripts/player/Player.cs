using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 playerPosition;
    public GameObject Body { get;set; }
    public Vector3 startingPosition;

    // Use this for initialization
    void Start () {
		if(gameMaster.instance.LastUsedDoor != null)
        {
            startingPosition == gameMaster.instance.LastUsedDoor.art.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static explicit operator Player(GameObject v)
    {
        throw new NotImplementedException();
    }
}
