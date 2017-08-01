using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour {

    public string parentScene;
    public GameObject art;
    public string Partner;
    GameObject selectedObject;
    public float grabRange = 5;
    public Collider other;

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

            gameMaster.instance.LastUsedDoor = closestObject;

            AsyncOperation enterPortal = SceneManager.LoadSceneAsync(closestObject.parentScene);
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
