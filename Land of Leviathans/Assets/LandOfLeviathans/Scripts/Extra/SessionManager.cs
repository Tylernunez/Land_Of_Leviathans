using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public class SessionManager : MonoBehaviour
    {
        [Header("NPC States")]
        public NPCStates[] npcStates;

        [Header("Equiped Items")]
        public List<string> eq_rh_weapons = new List<string>();
        public List<string> eq_lh_weapons = new List<string>();
        public List<string> eq_con_items = new List<string>();
        public List<string> eq_spell_items = new List<string>();

        public string a_chestPiece;
        public string a_legPiece;
        public string a_handsPiece;
        public string a_headPiece;

        [HideInInspector]
        public int _a_chest;
        [HideInInspector]
        public int _a_legs;
        [HideInInspector]
        public int _a_hands;
        [HideInInspector]
        public int _a_head;

        [HideInInspector]
        public List<int> _eq_rh = new List<int>();
        [HideInInspector]
        public List<int> _eq_lh = new List<int>();
        [HideInInspector]
        public List<int> _eq_con = new List<int>();

        [Header("In Inventory")]
        public List<string> weapon_items = new List<string>();
        public List<string> cons_items = new List<string>();
        public List<string> spell_items = new List<string>();
        public List<string> armor_items = new List<string>();

        int m_w_item_index;
        int m_c_item_index;
        int m_a_item_index;
        List<ItemInventoryInstance> _w_items = new List<ItemInventoryInstance>();
        List<ItemInventoryInstance> _c_items = new List<ItemInventoryInstance>();
        List<ItemInventoryInstance> _a_items = new List<ItemInventoryInstance>();
        ItemInventoryInstance unarmedItem = new ItemInventoryInstance();
        ItemInventoryInstance emptyItem = new ItemInventoryInstance();

        Dictionary<string, int> event_ids = new Dictionary<string, int>();
        Dictionary<string, int> npc_ids = new Dictionary<string, int>();

        public List<ItemInventoryInstance> GetItemsInstanceList(ItemType t)
        {
            switch (t)
            {
                case ItemType.weapon:
                    return _w_items;
                    
                
                case ItemType.consum:
                    return _c_items;
                 
                case ItemType.spell:
                    return null;
                case ItemType.equipment:
                    return _a_items;
                default:
                    return null;
            }
        }

        public List<Item> GetItemsAsList(ItemType t)
        {
            switch (t)
            {
                case ItemType.weapon:
                    return ResourcesManager.singleton.GetAllItemsFromList(weapon_items, t);
                case ItemType.spell:
                    return ResourcesManager.singleton.GetAllItemsFromList(spell_items, t);
                case ItemType.consum:
                    return ResourcesManager.singleton.GetAllItemsFromList(cons_items, t);
                case ItemType.equipment:
                default:
                    return null;
            }
         
        }

        public ItemInventoryInstance GetArmorItem(int uniqueId)
        {
            if(uniqueId == -1)
            {
                return emptyItem;
            }

            return GetItem(_a_items, uniqueId);
        }

        public ItemInventoryInstance GetWeaponItem(int uniqueId)
        {
            if (uniqueId == -1)
            {
                return unarmedItem;
            }
            return GetItem(_w_items, uniqueId);
        }

        public ItemInventoryInstance GetConItem(int uniqueId)
        {
            if (uniqueId == -1)
                return emptyItem;

            return GetItem(_c_items, uniqueId);
        }

        public ItemInventoryInstance GetItem(List<ItemInventoryInstance> l, int uniqueId)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].uniqueId == uniqueId)
                    return l[i];
            }

            Debug.Log("null item");
            return null;
        }

        public ItemInventoryInstance StringToItemInst(List<ItemInventoryInstance> l, string s)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (string.Equals(l[i].itemId , s))
                    return l[i];
            }

            return null;
        }

        public UI.InventoryUI inventoryUI;
        public ResourcesManager rm;

        void InitEmpties()
        {
            unarmedItem = new ItemInventoryInstance();
            unarmedItem.itemId = "unarmed";
            unarmedItem.uniqueId = -1;

            emptyItem = new ItemInventoryInstance();
            emptyItem.itemId = "empty";
            emptyItem.uniqueId = -1;
        }


        public static SessionManager singleton;
        void Awake()
        {
            singleton = this;
            InitEmpties();
            rm.Pre_Init();
            inventoryUI.Pre_Init();
            #region Init Weapons

            for (int i = 0; i < eq_rh_weapons.Count; i++)
            {
                weapon_items.Add(eq_rh_weapons[i]);
            }

            for (int i = 0; i < eq_lh_weapons.Count; i++)
            {
                weapon_items.Add(eq_lh_weapons[i]);
            }

            for (int i = 0; i < eq_con_items.Count; i++)
            {
                cons_items.Add(eq_con_items[i]);
            }

            for (int i = 0; i < weapon_items.Count; i++)
            {
                ItemInventoryInstance it = new ItemInventoryInstance();
                it.itemId = weapon_items[i];
                it.uniqueId = m_w_item_index;
                m_w_item_index++;
                _w_items.Add(it);
            }

            for (int i = 0; i < eq_rh_weapons.Count; i++)
            {
                ItemInventoryInstance it = StringToItemInst(_w_items, eq_rh_weapons[i]);
                _eq_rh.Add(it.uniqueId);
                it.slot = inventoryUI.equipSlotsUI.GetWeaponSlot(i);
                it.eq_index = i;
            }

            for (int i = 0; i < eq_lh_weapons.Count; i++)
            {
                ItemInventoryInstance it = StringToItemInst(_w_items, eq_lh_weapons[i]);
                _eq_lh.Add(it.uniqueId);
                int targetIndex = i + 3;
                it.slot = inventoryUI.equipSlotsUI.GetWeaponSlot(targetIndex);
                it.eq_index = targetIndex;
            }

         
            for (int i = 0; i < cons_items.Count; i++)
            {
                ItemInventoryInstance it = new ItemInventoryInstance();
                it.itemId = cons_items[i];
                it.uniqueId = m_c_item_index;
                m_c_item_index++;
                _c_items.Add(it);
            }

            for (int i = 0; i < eq_con_items.Count; i++)
            {
                ItemInventoryInstance it = StringToItemInst(_c_items, eq_con_items[i]);
                _eq_con.Add(it.uniqueId);
                it.slot = inventoryUI.equipSlotsUI.GetConSlot(i);
                it.eq_index = i;
            }

            #endregion


            AddArmorItem(a_chestPiece, ArmorType.chest, true);
            AddArmorItem(a_handsPiece,ArmorType.hands, true);
            AddArmorItem(a_legPiece, ArmorType.legs,true);
            AddArmorItem(a_headPiece,ArmorType.head,true);
           
            for (int i = 0; i < armor_items.Count; i++)
            {
                AddArmorItem(armor_items[i]);
            }

            for (int i = 0; i < npcStates.Length; i++)
            {
                npc_ids.Add(npcStates[i].npc_id, i);
                
            }
            
        }

        void AddEvents()
        {
            event_ids.Add("npc1_a", 0);
        }

        public void PlayEvent(string id)
        {
            int index = -1;
            event_ids.TryGetValue(id, out index);

            switch (index)
            {
                case -1:
                    return;
                case 0:
                    NPCStates npc = GetNpcState("npc1");
                    npc.dialogueIndex = 3;
                    break;
                default:
                    break;
            }
        }

        void AddArmorItem(string id, ArmorType t = ArmorType.chest, bool isEquiped = false)
        {
            if(string.IsNullOrEmpty(id) || string.Equals(id,"empty"))
            {
                if(isEquiped)
                {
                    switch (t)
                    {
                        case ArmorType.chest:
                            _a_chest = -1;
                            break;
                        case ArmorType.legs:
                            _a_legs = -1;
                            break;
                        case ArmorType.hands:
                            _a_hands = -1;
                            break;
                        case ArmorType.head:
                            _a_head = -1;
                            break;
                        default:
                            break;
                    }
                }

                return;
            }

            ItemInventoryInstance item = new ItemInventoryInstance();
            item.itemId = id;
            m_a_item_index++;
            item.uniqueId = m_a_item_index;
            _a_items.Add(item);

            if(isEquiped)
            {
                switch (t)
                {
                    case ArmorType.chest:
                        _a_chest = item.uniqueId;                       
                        break;
                    case ArmorType.legs:
                        _a_legs = item.uniqueId;
                        break;
                    case ArmorType.hands:
                        _a_hands = item.uniqueId;
                        break;
                    case ArmorType.head:
                        _a_head = item.uniqueId;
                        break;
                    default:
                        break;
                }

           //     item.armorType = t;
            }
        }

        public void AddItem(string id, ItemType t)
        {
            switch (t)
            {
                case ItemType.weapon:
                    weapon_items.Add(id);
                    ItemInventoryInstance it = new ItemInventoryInstance();
                    it.itemId = id;
                    it.uniqueId = m_w_item_index;
                    m_w_item_index++;
                    _w_items.Add(it);

                    break;
                case ItemType.spell:
                    break;
                case ItemType.consum:
                    ItemInventoryInstance c_it = new ItemInventoryInstance();
                    c_it.itemId =id;
                    c_it.uniqueId = m_c_item_index;
                    m_c_item_index++;
                    _c_items.Add(c_it);
                    break;
                case ItemType.equipment:
                    AddArmorItem(id);
                    break;
                default:
                    break;
            }

            Item i = rm.GetItem(id, t);
            UIManager.singleton.AddAnnounceCard(i);
        }

        public NPCStates GetNpcState(string id)
        {
            int index = -1;
            npc_ids.TryGetValue(id, out index);
            if (index == -1)
                return null;

            return npcStates[index];
        }
    }

    [System.Serializable]
    public class NPCStates
    {
        public string npc_id;
        public int dialogueIndex;
        
    }

    [System.Serializable]
    public class ItemInventoryInstance
    {
        public int uniqueId;
        public int eq_index;
        public string itemId;
       // public ArmorType armorType;
        public UI.EquipmentSlot slot;
    }
}
