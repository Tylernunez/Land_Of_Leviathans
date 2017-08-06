using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour {

    public string parentScene;
    public GameObject art;
    public string Partner;
    public float grabRange = 3.0f;
    public Collider other;
    public GameObject spawnPoint;

    // Use this for initialization
    void Start () {
		
	}
    public void enter()
    {
        door[] closeItem = FindObjectsOfType(typeof(door)) as door[];
        door closestObject = null;
        foreach (door g in closeItem)
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
        if (Vector3.Distance(transform.position, closestObject.transform.position) < grabRange)
        {
            
            gameMaster.instance.LastUsedDoor = closestObject.name;
            gameMaster.instance.EntryDoor = closestObject.Partner;
            gameMaster.instance.Load(closestObject.parentScene);
        }

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enter();
        }
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
