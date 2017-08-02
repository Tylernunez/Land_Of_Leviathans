using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

public class Player1 : MonoBehaviour
{

    public static Player1 instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public Player playerScript;                       //Store a reference to our BoardManager which will set up the level.                            


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
        playerScript = GetComponent<Player>();

    }

    //Update is called every frame.
    void Update()
    {

    }
}