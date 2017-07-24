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
		
	}

    public static explicit operator Player(GameObject v)
    {
        throw new NotImplementedException();
    }
}
