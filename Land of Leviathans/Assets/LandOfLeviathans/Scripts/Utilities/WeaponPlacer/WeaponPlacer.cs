using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    [ExecuteInEditMode]
    public class WeaponPlacer : MonoBehaviour
    {
        public string itemId;

        public GameObject targetModel;

        public bool leftHand;
        public bool saveItem;
        public SaveType saveType;

        public enum SaveType
        {
            weapon,item
        }

        private void Update()
        {
            if (!saveItem)
            {
                return;
            }
            saveItem = false;

            switch (saveType)
            {
                case SaveType.weapon:
                    SaveWeapon();
                    break;
                case SaveType.item:
                    SaveConsumable();
                    break;
                default:
                    break;
            }
            
        }
        void SaveWeapon()
        {
            if (targetModel == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(itemId))
            {
                return;
            }


            WeaponScriptableObject obj = Resources.Load("SA.WeaponScriptableObject") as WeaponScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.WeaponScriptableObject couldnt be loaded!");
                return;
            }

            for (int i = 0; i < obj.weapons_all.Count; i++)
            {
                if (obj.weapons_all[i].item_id == itemId)
                {
                    Weapon w = obj.weapons_all[i];
                    return;
                }
            }
        }

        void SaveConsumable()
        {
            if (targetModel == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(itemId))
            {
                return;
            }


            ConsumablesScriptableObject obj = Resources.Load("SA.ConsumablesScriptableObject") as ConsumablesScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.WeaponScriptableObject couldnt be loaded!");
                return;
            }

            for (int i = 0; i < obj.consumables.Count; i++)
            {
                if (obj.consumables[i].item_id == itemId)
                {
                    Consumable w = obj.consumables[i];
                    return;
                }
            }
        }
    }
}

