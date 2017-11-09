using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class CursorMovement : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("w")) transform.Translate(0, 1, 0, Camera.main.transform);
            if (Input.GetKeyDown("a")) transform.Translate(-1, 0, 0, Camera.main.transform);
            if (Input.GetKeyDown("s")) transform.Translate(0, -1, 0, Camera.main.transform);
            if (Input.GetKeyDown("d")) transform.Translate(1, 0, 0, Camera.main.transform);
        }
    }
}

