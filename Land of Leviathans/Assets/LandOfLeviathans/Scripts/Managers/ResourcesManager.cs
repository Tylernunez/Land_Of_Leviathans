using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class ResourcesManager : MonoBehaviour
    {
        Dictionary<string, int> i_spells = new Dictionary<string, int>();
        Dictionary<string, int> i_weapons = new Dictionary<string, int>();
        Dictionary<string, int> i_cons = new Dictionary<string, int>();
        Dictionary<string, int> i_armor = new Dictionary<string, int>();

        Dictionary<string, int> spell_ids = new Dictionary<string, int>();
        Dictionary<string, int> weapon_ids = new Dictionary<string, int>();
        Dictionary<string, int> weaponstats_ids = new Dictionary<string, int>();
        Dictionary<string, int> consum_ids = new Dictionary<string, int>();
        Dictionary<string, int> armor_ids = new Dictionary<string, int>();

        //other
        Dictionary<string, int> inter_ids = new Dictionary<string, int>();
        Dictionary<string, int> npc_ids = new Dictionary<string, int>();

        public static ResourcesManager singleton;

        #region Initialization
        public void Pre_Init()
        {
            singleton = this;
            LoadItems();
            LoadWeaponIds();
            LoadSpellIds();
            LoadConsumables();
            LoadArmor();
        }


        void LoadInteractions()
        {
            InteractionsScriptableObject obj = Resources.Load("SA.InteractionsScriptableObject") as InteractionsScriptableObject;
            if (obj == null)
            {
                Debug.Log("SA.ArmorScriptableObject couldn't be loaded!");
                return;
            }

            for (int i = 0; i < obj.interactions.Length; i++)
            {
                if (inter_ids.ContainsKey(obj.interactions[i].interactionId))
                {
                    Debug.Log(obj.interactions[i].interactionId + " is a duplicate!");
                }
                else
                {
                    inter_ids.Add(obj.interactions[i].interactionId, i);
                }
            }
        }

        void LoadNPC()
        {
            NPCScriptableObject obj = Resources.Load("SA.NPCScriptableObject") as NPCScriptableObject;
            if(obj == null)
            {
                Debug.Log("SA.NPCScriptableObject couldn't be loaded!");
                return;
            }

            for(int i = 0; i < obj.npc.Length; i++)
            {
                if (npc_ids.ContainsKey(obj.npc[i].npc_id))
                {
                    Debug.Log(obj.npc[i].npc_id + " is a duplicate!");
                }
                else
                {
                    inter_ids.Add(obj.npc[i].npc_id, i);
                }
            }
        }


        void LoadArmor()
        {
            ArmorScriptableObject obj = Resources.Load("SA.ArmorScriptableObject") as ArmorScriptableObject;
            if (obj == null)
            {
                Debug.Log("SA.ArmorScriptableObject couldnt be loaded!");
                return;
            }

            for (int i = 0; i < obj.armor_containers.Length; i++)
            {
                if (armor_ids.ContainsKey(obj.armor_containers[i].itemId))
                {
                    Debug.Log(obj.armor_containers[i].itemId + " is a duplicate!");
                }
                else
                {
                    armor_ids.Add(obj.armor_containers[i].itemId, i);
                }
            }
        }

        void LoadItems()
        {
            ItemsScriptableObject obj = Resources.Load("SA.ItemsScriptableObject") as ItemsScriptableObject;
            if (obj == null)
            {
                Debug.Log("SA.ItemsScriptableObject couldnt be loaded!");
                return;
            }

            for (int i = 0; i < obj.spell_items.Count; i++)
            {
                if (i_spells.ContainsKey(obj.spell_items[i].item_id))
                {
                    Debug.Log(obj.spell_items[i].item_id + " item is a duplicate");
                }
                else
                {
                    i_spells.Add(obj.spell_items[i].item_id, i);
                }
            }

            for (int i = 0; i < obj.weapon_items.Count; i++)
            {
                if (i_weapons.ContainsKey(obj.weapon_items[i].item_id))
                {
                    Debug.Log(obj.weapon_items[i].item_id + " item is a duplicate");
                }
                else
                {
                    i_weapons.Add(obj.weapon_items[i].item_id, i);
                }
            }

            for (int i = 0; i < obj.cons_items.Count; i++)
            {
                if (i_cons.ContainsKey(obj.cons_items[i].item_id))
                {
                    Debug.Log(obj.cons_items[i].item_id + " item is a duplicate");
                }
                else
                {
                    i_cons.Add(obj.cons_items[i].item_id, i);
                }
            }

            for (int i = 0; i < obj.armor_items.Count; i++)
            {
                if(i_armor.ContainsKey(obj.armor_items[i].item_id))
                {
                    Debug.Log(obj.armor_items[i].item_id + " item is a duplicate");
                }
                else
                {
                    i_armor.Add(obj.armor_items[i].item_id, i);
                }
            }
        }

        void LoadSpellIds()
        {
            SpellItemScriptableObject obj = Resources.Load("SA.SpellItemScriptableObject") as SpellItemScriptableObject;
            if (obj == null)
            {
                Debug.Log("SA.SpellItemScriptableObject couldnt be loaded!");
                return;
            }

            for (int i = 0; i < obj.spell_items.Count; i++)
            {
                if (spell_ids.ContainsKey(obj.spell_items[i].item_id))
                {
                    Debug.Log(obj.spell_items[i].item_id + " item is a duplicate");
                }
                else
                {
                    spell_ids.Add(obj.spell_items[i].item_id, i);
                }
            }
        }

        void LoadWeaponIds()
        {
            WeaponScriptableObject obj = Resources.Load("SA.WeaponScriptableObject") as WeaponScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.WeaponScriptableObject couldnt be loaded!");
                return;
            }

            for (int i = 0; i < obj.weapons_all.Count; i++)
            {
                if(weapon_ids.ContainsKey(obj.weapons_all[i].item_id))
                {
                    Debug.Log(obj.weapons_all[i].item_id + " item is a duplicate");
                }
                else
                {
                    weapon_ids.Add(obj.weapons_all[i].item_id, i);
                }
            }

            for (int i = 0; i < obj.weaponStats.Count; i++)
            {
                if (weaponstats_ids.ContainsKey(obj.weaponStats[i].weaponId))
                {
                    Debug.Log(obj.weaponStats[i].weaponId + " is a duplicate");
                }
                else
                {
                    weaponstats_ids.Add(obj.weaponStats[i].weaponId, i);
                }
            }
        }

        void LoadConsumables()
        {
            ConsumablesScriptableObject obj = Resources.Load("SA.ConsumablesScriptableObject") as ConsumablesScriptableObject;
            if (obj == null)
            {
                Debug.Log("SA.ConsumablesScriptableObject couldnt be loaded!");
                return;
            }

            for (int i = 0; i < obj.consumables.Count; i++)
            {
                if (consum_ids.ContainsKey(obj.consumables[i].item_id))
                {
                    Debug.Log(obj.consumables[i].item_id + " item is a duplicate");
                }
                else
                {
                    consum_ids.Add(obj.consumables[i].item_id, i);
                }
            }
        }
        #endregion

        public List<Item> GetAllItemsFromList(List<string> l, ItemType t)
        {
            List<Item> r = new List<Item>();
            for (int i = 0; i < l.Count; i++)
            {
                Item it = GetItem(l[i], t);
                r.Add(it);
            }

            return r;
        }

        int GetIndexFromString(Dictionary<string,int> d, string id)
        {
            int index = -1;
            d.TryGetValue(id, out index);
            return index;
        }
        public Item GetItem(string id, ItemType type)
        {
            ItemsScriptableObject obj = Resources.Load("SA.ItemsScriptableObject") as ItemsScriptableObject;

            if(obj == null)
            {
                Debug.Log("SA.ItemsScriptableObject is null!");
            }

            Dictionary<string, int> d = null;
            List<Item> l = null;
 
            switch (type)
            {
                case ItemType.weapon:
                    d = i_weapons;
                    l = obj.weapon_items;
                    break;
                case ItemType.spell:
                    d = i_spells;
                    l = obj.spell_items;
                    break;
                case ItemType.consum:
                    d = i_cons;
                    l = obj.cons_items;
                    break;
                case ItemType.equipment:
                    d = i_armor;
                    l = obj.armor_items;
                    break;
                default:
                    break;
            }

            if (d == null)
                return null;
            if (l == null)
                return null;

            int index = GetIndexFromString(d,id);
            if (index == -1)
                return null;

            return l[index];
        }

        //Weapons
        public Weapon GetWeapon(string id)
        {
            WeaponScriptableObject obj = Resources.Load("SA.WeaponScriptableObject") as WeaponScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.WeaponScriptableObject cant be loaded!");
                return null;
            }

            // int index = GetWeaponIdFromString(id);
            int index = GetIndexFromString(weapon_ids, id);

            if (index == -1)
                return null;

            return obj.weapons_all[index];
        }
        public WeaponStats GetWeaponStats(string id)
        {
            WeaponScriptableObject obj = Resources.Load("SA.WeaponScriptableObject") as WeaponScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.WeaponScriptableObject cant be loaded!");
                return null;
            }

            int index = GetIndexFromString(weaponstats_ids, id);

            if (index == -1)
                return null;

            return obj.weaponStats[index];
        }

        //Spells 
        public Spell GetSpell(string id)
        {
            SpellItemScriptableObject obj = Resources.Load("SA.SpellItemScriptableObject") as SpellItemScriptableObject;
            if(obj == null)
            {
                Debug.Log("SA.SpellItemScriptableObject cant be loaded!");
                return null;
            }
            
            int index = GetIndexFromString(spell_ids, id);
            if (index == -1)
                return null;

            return obj.spell_items[index];
        }
        //Consumables
        public Consumable GetConsumable(string id)
        {
            ConsumablesScriptableObject obj = Resources.Load("SA.ConsumablesScriptableObject") as ConsumablesScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.ConsumablesScriptableObject cant be loaded!");
                return null;
            }
            
            int index = GetIndexFromString(consum_ids, id);
            if (index == -1)
                return null;

            return obj.consumables[index];
        }

        public ArmorContainer GetArmor(string id)
        {
            ArmorScriptableObject obj = Resources.Load("SA.ArmorScriptableObject") as ArmorScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.ArmorScriptableObject cant be loaded!");
                return null;
            }

            // int index = GetWeaponIdFromString(id);
            int index = GetIndexFromString(armor_ids, id);

            if (index == -1)
                return null;

            return obj.armor_containers[index];
        }

        public Interactions GetInteraction(string id)
        {
            InteractionsScriptableObject obj = Resources.Load("SA.InteractionsScriptableObject") as InteractionsScriptableObject;

            if(obj == null)
            {
                Debug.Log("SA.InteractionsScriptableObject cant be loaded!");
                return null;
            }
            int index = GetIndexFromString(inter_ids, id);

            if (index == -1)
                return null;

            return obj.interactions[index];
        }

        public NPCDialogue GetDialogue(string id)
        {
            NPCScriptableObject obj = Resources.Load("SA.NPCScriptableObject") as NPCScriptableObject;

            if (obj == null)
            {
                Debug.Log("SA.NPCScriptableObject cant be loaded!");
                return null;
            }
            int index = GetIndexFromString(npc_ids, id);

            if (index == -1)
                return null;

            return obj.npc[index];
        }

    }

    public enum ItemType
    {
        weapon, spell, consum, equipment
    }

}
