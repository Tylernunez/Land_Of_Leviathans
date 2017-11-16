using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoL
{
    public class SessionToken : MonoBehaviour
    {

        public MapGenerator.Tile location;
        public bool inOpenField;
        public bool inVillage;
        public Village village;

        public static SessionToken singleton;
        public void Awake()
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }

    }
}

