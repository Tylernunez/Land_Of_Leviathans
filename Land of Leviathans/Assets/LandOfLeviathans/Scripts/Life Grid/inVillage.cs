using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class inVillage : MonoBehaviour
    {

        Village data;
        public GameObject npcprefab;

        // Use this for initialization
        void Start()
        {
            data = SessionToken.singleton.village;
            Init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Init()
        {
            foreach(Person i in data.people)
            {
                Vector3 position = new Vector3(Random.Range(280, 290), 4, Random.Range(300, 320));
                Instantiate(npcprefab, position, Quaternion.identity);
            }
        }
    }

}
