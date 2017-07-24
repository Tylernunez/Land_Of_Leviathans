using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour {

    public GameObject inventory;
    public Transform inventoryParent;
    public bool isHolding = false;
    public float grabRange = 5.0f;
    public Collider other;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isHolding && Input.GetKeyDown(KeyCode.E))
        {
            pickup();
        }

        if (isHolding && Input.GetKeyDown(KeyCode.Q))
        {
            drop();
        }
         
	}
    public void pickup()
    {
        GameObject[] closeItem = GameObject.FindGameObjectsWithTag("pickup");
        GameObject closestObject = null;
        foreach(GameObject g in closeItem)
        {
            if (!closestObject)
            {
                closestObject = g;
            }
            //compare distances
            if (Vector3.Distance(transform.position, g.transform.position) <= Vector3.Distance(transform.position, closestObject.transform.position))
            {
                closestObject = g;
            }

        }
        if(Vector3.Distance(transform.position, closestObject.transform.position) < grabRange)
        {
                Debug.Log("Pickup!");
                inventory = closestObject;
                inventoryParent = this.transform;
                inventory.transform.parent = this.transform;
                //Places the item grabbed in front of player.
                inventory.transform.position = this.transform.position + this.transform.TransformDirection(new Vector3(0, 0, 1));
                isHolding = true;
                //Rigidbody pickup = inventory.GetComponent<Rigidbody>();
                //pickup.freezeRotation = true;
                inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }
    public void drop()
    {    
        Debug.Log("Drop!");
        inventory.transform.parent = null;
        inventoryParent = null;
        inventory.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if (other != null)
        {

        }
        inventory = null;
        isHolding = false;
    }

    public void OnTriggerEnter(Collider col)
    {
        other = col;
    }
    public void OnTriggerExit(Collider col)
    {
        other = null;
    }
}
