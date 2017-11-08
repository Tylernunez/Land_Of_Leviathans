using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class Village
    {
        public List<Person> people = new List<Person>();
        public List<Resource> resources = new List<Resource>();
        public int Hunger;
        public Morale morale;
        public List<Structure> specialBuildings = new List<Structure>(); 

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

