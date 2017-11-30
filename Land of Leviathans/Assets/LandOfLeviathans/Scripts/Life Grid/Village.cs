using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class Village : MonoBehaviour
    {
        public List<Person> people = new List<Person>();
        public List<Resource> resources = new List<Resource>();
        public int Hunger;
        public Morale morale;
        public int xPos;
        public int yPos;
        public List<Structure> specialBuildings = new List<Structure>();
        System.Random rng = new System.Random();
        public List<string> names = new List<string>();


        public void Init(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            names.Add("Bob");
            names.Add("Kaitlyn");
            names.Add("Egwene");
            names.Add("Rand");
            int villagers = rng.Next(2, 10);
            for(int i = 0; i <= villagers; i++)
            {
                string name = GenerateName();
                Person villager = new Person();
                villager.name = name;
                people.Add(villager);
            }
        }

        public string GenerateName()
        {
            int r = rng.Next(names.Count);
            string name = ((string)names[r]);

            return name;
        }

    }

    [System.Serializable]
    public class Person
    {
        public string name;

    }
    [System.Serializable]
    public class Structure
    {


    }
}

