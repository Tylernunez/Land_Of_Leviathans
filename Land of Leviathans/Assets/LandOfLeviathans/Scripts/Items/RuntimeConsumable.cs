using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class RuntimeConsumable : MonoBehaviour
    {
        public bool isEmpty;
        public int itemCount = 2;
        public bool unlimitedCount;
        public Consumable instance;
        public GameObject itemModel;
    }
}
