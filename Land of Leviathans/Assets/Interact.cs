using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
public GameObject OpenPanel = null;

	// Use this for initialization
	void Start () {
        OpenPanel.SetActive(false);
	}
	
    void OnTriggerEnter(GameObject other)
    {
        if (other.tag == "Player")
        {
            OpenPanel.SetActive(true);
        }
    }

    void OnTriggerExit(GameObject other)
    {
        if (other.tag == "Player")
        {
            OpenPanel.SetActive(false);
        }

    }

    private bool IsOpenPanelActive
    {
        get
        {
            return OpenPanel.activeInHierarchy;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (IsOpenPanelActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenPanel.SetActive(false);
            }
        }
        if (IsOpenPanelActive == false)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OpenPanel.SetActive(true);
            }
        }
    }
}
