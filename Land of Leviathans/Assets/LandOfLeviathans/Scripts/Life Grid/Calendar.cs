using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class Calendar
    {


    }
    [System.Serializable]
    public class Hour
    {
        public int current;
    }
    [System.Serializable]
    public class Day
    {
        public Hour current;
    }
    [System.Serializable]
    public class Week
    {
        public Day current;
    }
    [System.Serializable]
    public class Month
    {
        public Week current;
    }
    [System.Serializable]
    public class Year
    {
        public Year current;
    }
}

