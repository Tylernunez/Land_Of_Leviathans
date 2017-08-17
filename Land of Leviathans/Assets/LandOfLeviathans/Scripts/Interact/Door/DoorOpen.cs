using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : Interactable {

    public Door door;

    // Use this for initialization
    void Start () {

    }
    public override void Interact()
    {
        base.Interact();
        gameMaster.instance.EntryDoor = door.Partner;
        gameMaster.instance.LastUsedDoor = door.name;
        Enter();

    }

    void Enter()
    {
        AsyncOperation enterPortal = SceneManager.LoadSceneAsync(door.destination);
    }

}
