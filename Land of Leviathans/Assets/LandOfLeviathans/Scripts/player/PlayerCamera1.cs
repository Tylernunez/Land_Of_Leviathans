using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera1 : MonoBehaviour
{

    public static PlayerCamera1 instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public PlayerCamera playerScript;                       //Store a reference to our BoardManager which will set up the level.                            


    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        playerScript = GetComponent<PlayerCamera>();

    }

    //Update is called every frame.
    void Update()
    {

    }
}
