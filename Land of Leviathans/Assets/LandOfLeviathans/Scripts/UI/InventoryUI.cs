using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SA;

namespace SA.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public EquipmentLeft eq_left;
        public CenterOverlay c_overlay;
        public WeaponInfo weaponInfo;
        public PlayerStatus playerStatus;
        public GameObject gameMenu,
            inventory, centerMain, centerRight, centerOverlay;
        public GameObject equipmentScreen;
        public GameObject inventoryScreen;
        public GameObject gameUI;

        List<IconBase> iconSlotsCreated = new List<IconBase>();
        public EquipmentSlotsUI equipSlotsUI;

        public Transform eqSlotsParent;
       // List<EquipmentSlot> equipSlots = new List<EquipmentSlot>();
        EquipmentSlot[,] equipSlots;
        public Vector2 curSlotPos;
        public int curInvIndex;
        List<IconBase> curCreatedItems;
        IconBase curInvIcon;
        int prevInvIndex;
        int maxInvIndex;

        float inputT;
        bool dontMove;

        public Color unselected;
        public Color selected;
        EquipmentSlot curEqSlot;
        EquipmentSlot prevEqSlot;

        float inpTimer;
        public float inpDelay = 0.4f;

        InputUI inp;
        InventoryManager invManager;
        public bool isSwitching;
        public bool isMenu;

        SessionManager session;

        void ClearOnIndex(int ind)
        {
            int ix = ind;
            if(ix > 2)
            {
                ix -= 3;
                invManager.lh_weapons[ix] = -1;
            }
            else
            {
                invManager.rh_weapons[ix] = -1;
            }

        }

        void HandleSlotInput(InputUI inp)
        {
            if (curEqSlot == null)
                return;

            #region A Input
            if(inp.a_input)
            {
                isSwitching = !isSwitching;

                if(isSwitching)
                {
                    CloseCreatedItems();//close any previously created item slots
                    ItemType t = ItemTypeFromSlotType(curEqSlot.slotType);
                    LoadCurrentItems(t);
                }
                else
                {
                    if (curInvIcon != null) //check for icon
                    {
                        //if the current icon is disabled, that means the selector isn't placed over a valid item
                        if (curInvIcon.icon.isActiveAndEnabled)
                        {
                            ItemType t = ItemTypeFromSlotType(curEqSlot.slotType);

                            switch (t)
                            {
                                case ItemType.weapon:
                                    ChangeWeapon();
                                    break;
                                case ItemType.spell:
                                    break;
                                case ItemType.consum:
                                    ChangeConsumable();
                                    break;
                                case ItemType.equipment:
                                    ChangeArmor();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    LoadEquipment(invManager, true);
                }

                ChangeToSwitching();
            }

            #endregion

            if (inp.b_input)
            {
                if (isSwitching)
                {
                    isSwitching = false;
                    ChangeToSwitching();
                }
                else
                {
                    isMenu = false;
                    CloseUI();
                }
            }

            #region X Input
            if (inp.x_input)
            {
                if (isSwitching)
                {
                   
                }
                else
                {
                    ItemType t = ItemTypeFromSlotType(curEqSlot.slotType);

                    switch (t)
                    {
                        case ItemType.weapon:
                            ClearWeapon();
                            break;
                        case ItemType.spell:
                            break;
                        case ItemType.consum:
                            ClearConsumable();
                            break;
                        case ItemType.equipment:
                            ClearArmor();
                            break;
                        default:
                            break;
                    }

                    LoadEquipment(invManager, true);
                }
            }
            #endregion
        }

        void ClearWeapon()
        {
            int targetIndex = curEqSlot.itemPosition;
            bool isLeft = (curEqSlot.itemPosition > 2) ? true : false;
            if (isLeft)
            {
                targetIndex -= 3;
                invManager.lh_weapons[targetIndex] = -1;

                ItemInventoryInstance inst = session.GetWeaponItem(invManager.lh_weapons[targetIndex]);
                if (inst.slot != null)
                {
                    equipSlotsUI.ClearEqSlot(inst.slot, ItemType.weapon);
                }
            }
            else
            {
                invManager.rh_weapons[targetIndex] = -1;
                ItemInventoryInstance inst = session.GetWeaponItem(invManager.rh_weapons[targetIndex]);

                if (inst.slot != null)
                {
                    equipSlotsUI.ClearEqSlot(inst.slot, ItemType.weapon);
                }
            }
        }

        void ClearConsumable()
        {
            int targetIndex = curEqSlot.itemPosition;

            if (targetIndex < invManager.consumable_items.Count)
            {
                invManager.consumable_items[curEqSlot.itemPosition] = -1;

                ItemInventoryInstance inst = session.GetWeaponItem(invManager.consumable_items[targetIndex]);

                if (inst.slot != null)
                {
                    equipSlotsUI.ClearEqSlot(inst.slot, ItemType.consum);
                }
            }
        }

        void ClearArmor()
        {
            switch (curEqSlot.armorType)
            {
                case ArmorType.chest:
                    invManager.armorManager.chestId = -1;
                    break;
                case ArmorType.legs:
                    invManager.armorManager.legsId = -1;
                    break;
                case ArmorType.hands:
                    invManager.armorManager.handsId = -1;
                    break;
                case ArmorType.head:
                    invManager.armorManager.headId = -1;
                    break;
                default:
                    break;
            }
        }

        void ChangeWeapon()
        {

            int targetIndex = curEqSlot.itemPosition;
            bool isLeft = (curEqSlot.itemPosition > 2) ? true : false;
            if (isLeft)
            {
                targetIndex -= 3;
                invManager.lh_weapons[targetIndex] = curInvIcon.id;

                ItemInventoryInstance inst = session.GetWeaponItem(invManager.lh_weapons[targetIndex]);

                if (inst.slot != null)
                {
                    if (inst.slot != curEqSlot)
                    {
                        equipSlotsUI.ClearEqSlot(inst.slot, ItemType.weapon);
                        ClearOnIndex(inst.eq_index);
                    }
                }
            }
            else
            {
                invManager.rh_weapons[targetIndex] = curInvIcon.id;

                ItemInventoryInstance inst = session.GetWeaponItem(invManager.rh_weapons[targetIndex]);
                if (inst.slot != null)
                {
                    if (inst.slot != curEqSlot)
                    {
                        equipSlotsUI.ClearEqSlot(inst.slot, ItemType.weapon);
                        ClearOnIndex(inst.eq_index);
                    }
                }
            }
        }

        void ChangeConsumable()
        {
            ItemInventoryInstance inst = session.GetConItem(curInvIcon.id);
            if (inst.slot != null)
            {
                equipSlotsUI.ClearEqSlot(inst.slot, ItemType.consum);
                invManager.consumable_items[inst.eq_index] = -1;
            }

            invManager.consumable_items[curEqSlot.itemPosition] = curInvIcon.id;
        }

        void ChangeArmor()
        {
            if (curInvIcon == null)
                return;

            switch (curEqSlot.armorType)
            {
                case ArmorType.chest:
                    invManager.armorManager.chestId = curInvIcon.id;
                    break;
                case ArmorType.legs:
                    invManager.armorManager.legsId = curInvIcon.id;
                    break;
                case ArmorType.hands:
                    invManager.armorManager.handsId = curInvIcon.id;
                    break;
                case ArmorType.head:
                    invManager.armorManager.headId = curInvIcon.id;
                    break;
                default:
                    break;
            }


        }

        ItemType ItemTypeFromSlotType(EqSlotType t)
        {
            switch (t)
            {
                case EqSlotType.weapons:
                    return ItemType.weapon;
                case EqSlotType.equipment:
                    return ItemType.equipment;
                case EqSlotType.arrows:
                case EqSlotType.bolts:
              
                case EqSlotType.rings:
                case EqSlotType.covenant:
                default:
                    return ItemType.spell;
                case EqSlotType.consumables:
                    return ItemType.consum;
            }
        }

        void HandleSlotMovement(InputUI inp)
        {
            int x = Mathf.RoundToInt(curSlotPos.x);
            int y = Mathf.RoundToInt(curSlotPos.y);

            bool up = (inp.vertical > 0);
            bool down = (inp.vertical < 0);
            bool left = (inp.horizontal < 0);
            bool right =(inp.horizontal > 0);

            if(!up && !down && !left && !right)
            {
                inpTimer = 0;
            }
            else
            {
                inpTimer -= Time.deltaTime;
            }

            if (inpTimer < 0)
                inpTimer = 0;

            if (inpTimer > 0)
                return;

            if (up)
            {
                y--;
                inpTimer = inpDelay;
            }
            if (down)
            {
                y++;
                inpTimer = inpDelay;
            }
            if(left)
            {
                x--;
                inpTimer = inpDelay;
            }
            if(right)
            {
                x++;
                inpTimer = inpDelay;
            }

            if (x > 4)
                x = 0;
            if (x < 0)
                x = 4;
            if (y > 5)
                y = 0;
            if (y < 0)
                y = 5;

           if (curEqSlot != null)
                curEqSlot.icon.background.color = unselected;

            if (x == 4 && y == 3)
            {
                x = 4;
                y = 2;
            }

            curEqSlot = equipSlots[x, y];

            curSlotPos.x = x;
            curSlotPos.y = y;

            if (curEqSlot != null)
                curEqSlot.icon.background.color = selected;
        }

        void HandleInventoryMovement(InputUI inp)
        {
            bool up = (inp.vertical > 0);
            bool down = (inp.vertical < 0);
            bool left = (inp.horizontal < 0);
            bool right = (inp.horizontal > 0);

            if (!up && !down && !left && !right)
            {
                inpTimer = 0;
            }
            else
            {
                inpTimer -= Time.deltaTime;
            }

            if (inpTimer < 0)
                inpTimer = 0;

            if (inpTimer > 0)
                return;

            if(up)
            {
                curInvIndex -= 5;
                inpTimer = inpDelay;
            }

            if(down)
            {
                curInvIndex += 5;
                inpTimer = inpDelay;
            }

            if(left)
            {
                curInvIndex -= 1;
                inpTimer = inpDelay;
            }

            if(right)
            {
                curInvIndex += 1;
                inpTimer = inpDelay;
            }

            if (curInvIndex > maxInvIndex-1)
                curInvIndex = 0;
            if (curInvIndex < 0)
                curInvIndex = maxInvIndex-1;
        }

        #region Init

        public void Pre_Init()
        {
            session = SessionManager.singleton;    
            CreateUIElements();
            InitEqSlots();
        }

        public void Init(InventoryManager inv)
        {
            inp = InputUI.singleton;
            invManager = inv;
        }

        void InitEqSlots()
        {
            EquipmentSlot[] eq = eqSlotsParent.GetComponentsInChildren<EquipmentSlot>();
            equipSlots = new EquipmentSlot[5, 6];
            
            for (int i = 0; i < eq.Length; i++)
            {
                eq[i].Init(this);
                int x = Mathf.RoundToInt(eq[i].slotPos.x);
                int y = Mathf.RoundToInt(eq[i].slotPos.y);
                equipSlots[x, y] = eq[i];
            }
        }

        void CreateUIElements()
        {
            WeaponInfoInit();
            PlayerStatusInit();
            WeaponStatusInit();
        }
        
        void WeaponInfoInit()
        {
            for (int i = 0; i < 6; i++)
            {
                CreateAttDefUIElement(weaponInfo.ap_slots, weaponInfo.ap_grid, (AttackDefenseType)i);
            }

            for (int i = 0; i < 5; i++)
            {
                CreateAttDefUIElement(weaponInfo.g_absorb, weaponInfo.g_abs_grid, (AttackDefenseType)i);
            }

            CreateAttDefUIElement(weaponInfo.g_absorb, weaponInfo.g_abs_grid, AttackDefenseType.stability);
            CreateAttDefUIElement_Mini(weaponInfo.a_effects, weaponInfo.a_effects_grid, AttackDefenseType.bleed);
            CreateAttDefUIElement_Mini(weaponInfo.a_effects, weaponInfo.a_effects_grid, AttackDefenseType.curse);
            CreateAttDefUIElement_Mini(weaponInfo.a_effects, weaponInfo.a_effects_grid, AttackDefenseType.frost);

            CreateAttributeElement_Mini(weaponInfo.att_bonus, weaponInfo.att_grid, AttributeType.strength);
            CreateAttributeElement_Mini(weaponInfo.att_bonus, weaponInfo.att_grid, AttributeType.dexterity);
            CreateAttributeElement_Mini(weaponInfo.att_bonus, weaponInfo.att_grid, AttributeType.intelligence);
            CreateAttributeElement_Mini(weaponInfo.att_bonus, weaponInfo.att_grid, AttributeType.faith);

            CreateAttributeElement_Mini(weaponInfo.att_req, weaponInfo.att_req_grid, AttributeType.strength);
            CreateAttributeElement_Mini(weaponInfo.att_req, weaponInfo.att_req_grid, AttributeType.dexterity);
            CreateAttributeElement_Mini(weaponInfo.att_req, weaponInfo.att_req_grid, AttributeType.intelligence);
            CreateAttributeElement_Mini(weaponInfo.att_req, weaponInfo.att_req_grid, AttributeType.faith);
        }

        void PlayerStatusInit()
        {
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.level, "Level");
            CreateEmptySlot(playerStatus.attGrid);

            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.vigor, "Vigor");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.attunement, "Attunement");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.endurance, "Endurance");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.vitality, "Vitality");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.strength, "Strength");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.dexterity, "Dexterity");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.intelligence, "Intelligence");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.faith, "Faith");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.luck, "Luck");

            CreateEmptySlot(playerStatus.attGrid);


            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.hp, "HP");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.fp, "FP");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.stamina, "Stamina");
            
            CreateEmptySlot(playerStatus.attGrid);

            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.equip_load, "Equip Load");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.poise, "Stamina");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.item_discovery, "Item Discovery");
            CreateAttributeElement(playerStatus.attSlots, playerStatus.attGrid, AttributeType.attunement_slots, "Attunement Slots");
   
        }

        void WeaponStatusInit()
        {
            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.physical, "Physical");
            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.strike, "VS Strike");
            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.slash, "VS Slash");
            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.thrust, "VS Thrust");

            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.magic, "Magic");
            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.fire, "Fire");
            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.lighting, "Lighting");
            CreateWeaponStatusSlot(playerStatus.defSlots, playerStatus.defGrid, AttackDefenseType.dark, "Dark");

            CreateWeaponStatusSlot(playerStatus.resSlots, playerStatus.resGrid, AttackDefenseType.bleed, "Bleed");
            CreateWeaponStatusSlot(playerStatus.resSlots, playerStatus.resGrid, AttackDefenseType.poison, "Poison");
            CreateWeaponStatusSlot(playerStatus.resSlots, playerStatus.resGrid, AttackDefenseType.frost, "Frost");
            CreateWeaponStatusSlot(playerStatus.resSlots, playerStatus.resGrid, AttackDefenseType.curse, "Curse");

            CreateAttackPowerSlot(playerStatus.apSlots, playerStatus.apGrid, "R Weapon 1");
            CreateAttackPowerSlot(playerStatus.apSlots, playerStatus.apGrid, "R Weapon 2");
            CreateAttackPowerSlot(playerStatus.apSlots, playerStatus.apGrid, "R Weapon 3");

            CreateAttackPowerSlot(playerStatus.apSlots, playerStatus.apGrid, "L Weapon 1");
            CreateAttackPowerSlot(playerStatus.apSlots, playerStatus.apGrid, "L Weapon 2");
            CreateAttackPowerSlot(playerStatus.apSlots, playerStatus.apGrid, "L Weapon 3");
        }

        void CreateWeaponStatusSlot(List<PlayerStatusDef> l, Transform p, AttackDefenseType t, string txt1Text = null)
        {
            PlayerStatusDef w = new PlayerStatusDef();
            GameObject g = Instantiate(playerStatus.doubleSlot_template) as GameObject;
            g.SetActive(true);
            g.transform.SetParent(p);
            w.type = t;
            w.slot = g.GetComponent<InventoryUIDoubleSlot>();
            if (string.IsNullOrEmpty(txt1Text))
                w.slot.txt1.text = t.ToString();
            else
                w.slot.txt1.text = txt1Text;
            w.slot.txt2.text = "30";
            w.slot.txt3.text = "30";
        }

        void CreateAttackPowerSlot(List<AttackPowerSlot> l, Transform p, string id)
        {
            AttackPowerSlot a = new AttackPowerSlot();
            l.Add(a);

            GameObject g = Instantiate(weaponInfo.slot_template) as GameObject;
            g.transform.SetParent(p);
            a.slot = g.GetComponent<InvetoryUISlot>();
            a.slot.txt1.text = id;
            a.slot.txt2.text = "30";
            g.SetActive(true);
        }

        void CreateAttDefUIElement(List<AttDefType> l, Transform p, AttackDefenseType t)
        {
            AttDefType a = new AttDefType();
            a.type = t;
            l.Add(a);

            GameObject g = Instantiate(weaponInfo.slot_template) as GameObject;
            g.transform.SetParent(p);
            a.slot = g.GetComponent<InvetoryUISlot>();
            a.slot.txt1.text = a.type.ToString();
            a.slot.txt2.text = "30";
            g.SetActive(true);
        }

        void CreateAttDefUIElement_Mini(List<AttDefType> l, Transform p, AttackDefenseType t)
        {
            AttDefType a = new AttDefType();
            a.type = t;
            l.Add(a);

            GameObject g = Instantiate(weaponInfo.slot_mini) as GameObject;
            g.transform.SetParent(p);
            a.slot = g.GetComponent<InvetoryUISlot>();
            a.slot.txt1.text = "0";
            g.SetActive(true);
        }

        void CreateAttributeElement_Mini(List<AttributeSlot> l, Transform p, AttributeType t)
        {
            AttributeSlot a = new AttributeSlot();
            a.type = t;
            l.Add(a);

            GameObject g = Instantiate(weaponInfo.slot_mini) as GameObject;
            g.transform.SetParent(p);
            a.slot = g.GetComponent<InvetoryUISlot>();
            a.slot.txt1.text = "-";
            g.SetActive(true);
        }

        void CreateAttributeElement(List<AttributeSlot> l, Transform p, AttributeType t, string txt1Text = null)
        {
            AttributeSlot a = new AttributeSlot();
            a.type = t;
            l.Add(a);

            GameObject g = Instantiate(playerStatus.slot_template) as GameObject;
            g.transform.SetParent(p);
            a.slot = g.GetComponent<InvetoryUISlot>();
            if (string.IsNullOrEmpty(txt1Text))
                a.slot.txt1.text = t.ToString();
            else
                a.slot.txt1.text = txt1Text;
            a.slot.txt2.text = "30";
            g.SetActive(true);
        }

        void CreateEmptySlot(Transform p)
        {
            GameObject g = Instantiate(playerStatus.emptySlot) as GameObject;
            g.transform.SetParent(p);
            g.SetActive(true);
        }

        #endregion

        public UIState curState;
        
        public void CloseCreatedItems()
        {
            for (int i = 0; i < iconSlotsCreated.Count; i++)
            {
                iconSlotsCreated[i].gameObject.SetActive(false);
            }
        }

        public void LoadCurrentItems(ItemType t)
        {
            //List<Item> itemList = session.GetItemsAsList(t);
            List<ItemInventoryInstance> itemList = session.GetItemsInstanceList(t);

            if (itemList == null)
                return;
         
            List<ItemInventoryInstance> canidateList = new List<ItemInventoryInstance>();
           
            if(t == ItemType.equipment)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    ArmorContainer armor = ResourcesManager.singleton.GetArmor(itemList[i].itemId);

                    if(armor.armorType == curEqSlot.armorType)
                    {
                        canidateList.Add(itemList[i]);
                    }
                }
            }
            else
            {
                canidateList.AddRange(itemList);
            }

            if (canidateList.Count == 0)
                return;

            GameObject prefab = eq_left.inventory.slotTemplate;
            Transform p = eq_left.inventory.slotGrid;

            int dif = iconSlotsCreated.Count - canidateList.Count;
            int extra = (dif>0)? dif: 0;

            maxInvIndex = canidateList.Count;

            curCreatedItems = new List<IconBase>();
            curInvIndex = 0;

            for (int i = 0; i < canidateList.Count + extra; i++)
            {
                if(i > canidateList.Count-1)
                {
                    iconSlotsCreated[i].gameObject.SetActive(false);
                    continue;
                }

                Item item = ResourcesManager.singleton.GetItem(canidateList[i].itemId, t);

                IconBase icon = null;
                if (iconSlotsCreated.Count-1 < i)
                {
                    GameObject g = Instantiate(prefab) as GameObject;
                    g.SetActive(true);
                    g.transform.SetParent(p);
                    icon = g.GetComponent<IconBase>();
                    iconSlotsCreated.Add(icon);
                }
                else
                {
                    icon = iconSlotsCreated[i];
                }

                curCreatedItems.Add(icon);
                icon.gameObject.SetActive(true);
                icon.icon.enabled = true;
                icon.icon.sprite = item.icon;
                icon.id = canidateList[i].uniqueId;
            }
        }

        void HandleUIState(InputUI inp)
        {
            switch (curState)
            {
                case UIState.equipment:
                    if (!isSwitching)
                        HandleSlotMovement(inp);
                    else
                        HandleInventoryMovement(inp);

                    HandleSlotInput(inp);
                    break;
                case UIState.inventory:
                    HandleInventoryMovement(inp);
                    break;
                case UIState.attributes:
                    break;
                case UIState.messages:
                    break;
                case UIState.options:
                    break;
                default:
                    break;
            }
        }

        void ChangeToSwitching()
        {
            if (isSwitching)
            {
                //reset indexes
                curInvIndex = 0;
                prevInvIndex = -1;

                equipmentScreen.SetActive(false);
                inventoryScreen.SetActive(true);
            }
            else
            {
                equipmentScreen.SetActive(true);
                inventoryScreen.SetActive(false);
            }
        }

        public void OpenUI()
        {
            LoadEquipment(invManager);
            gameMenu.SetActive(false);
            inventory.SetActive(true);
            gameUI.SetActive(false);
            prevEqSlot = null;

            if (curEqSlot != null)
                curEqSlot.icon.background.color = unselected;
            curSlotPos = Vector2.zero;
            curEqSlot = equipSlots[0, 0];

            curInvIndex = 0;
            prevInvIndex = -1;
        }

        public void CloseUI()
        {
            gameMenu.SetActive(false);
            inventory.SetActive(false);
            gameUI.SetActive(true);
            prevEqSlot = null;
            curInvIndex = 0;
            prevInvIndex = -1;
        }

        public void Tick()
        {
            inp.Tick();
            HandleUIState(inp);

            if(inp.rightAxis_down)
            {
                centerRight.SetActive(!centerRight.activeInHierarchy);
            }

            if (prevEqSlot != curEqSlot)
            {
                if (curEqSlot != null)
                {
                    eq_left.slotName.text = curEqSlot.slotName;
                    LoadItemFromSlot(curEqSlot.icon);
               }
            }

            if (curCreatedItems != null)
            {
                if (curCreatedItems.Count > 0)
                {
                    if (prevInvIndex != curInvIndex)
                    {
                        if (curInvIcon)
                            curInvIcon.background.color = unselected;

                        if (curInvIndex < curCreatedItems.Count)
                        {
                            curInvIcon = curCreatedItems[curInvIndex];
                            curInvIcon.background.color = selected;
                            LoadItemFromSlot(curInvIcon);
                        }
                    }
                }
            }

            prevEqSlot = curEqSlot;
            prevInvIndex = curInvIndex;
        }

        public void LoadEquipment(InventoryManager inv , bool loadOnCharacter = false)
        {
            if(loadOnCharacter)
                inv.ClearReferences();

            #region Weapons
            for (int i = 0; i < inv.rh_weapons.Count; i++)
            {
                if(i > 2)
                    break;

                EquipmentSlot slot = equipSlotsUI.weaponSlots[i];

                if (inv.rh_weapons[i] == -1)
                {
                    equipSlotsUI.ClearEqSlot(slot, ItemType.weapon);
                }
                else
                {
                    ItemInventoryInstance inst = session.GetWeaponItem(inv.rh_weapons[i]);
                    inst.slot = slot;
                    inst.eq_index = i;

                    equipSlotsUI.UpdateEqSlot(inst.uniqueId, slot, ItemType.weapon);
                }
            }

            for (int i = 0; i < inv.lh_weapons.Count; i++)
            {
                if (i > 2)
                    break;

                EquipmentSlot slot = equipSlotsUI.weaponSlots[i + 3];

                if (inv.lh_weapons[i] == -1)
                {
                    equipSlotsUI.ClearEqSlot(slot, ItemType.weapon);
                }
                else
                {
                    ItemInventoryInstance inst = session.GetWeaponItem(inv.lh_weapons[i]);
                    inst.slot = slot;
                    inst.eq_index = i+3;

                    equipSlotsUI.UpdateEqSlot(inst.uniqueId, slot, ItemType.weapon);
                }
            }

            #endregion

            for (int i = 0; i < inv.consumable_items.Count; i++)
            {
                if (i > 9)
                    break;

                EquipmentSlot slot = equipSlotsUI.cons[i];
                if (inv.consumable_items[i] == -1)
                {
                    equipSlotsUI.ClearEqSlot(slot, ItemType.consum);
                }
                else
                {
                    ItemInventoryInstance inst = session.GetConItem(inv.consumable_items[i]);
                    inst.slot = slot;
                    inst.eq_index = i;
                    equipSlotsUI.UpdateEqSlot(inv.consumable_items[i], slot, ItemType.consum);
                }
                
            }

            LoadArmor();

            if (loadOnCharacter)
            {         
                invManager.LoadInventory(true);      
            }
        }

        void LoadArmor()
        {
            UpdateArmorSlot(equipSlotsUI.equipment[0], ArmorType.head, invManager.armorManager.headId);
            UpdateArmorSlot(equipSlotsUI.equipment[1], ArmorType.chest, invManager.armorManager.chestId);
            UpdateArmorSlot(equipSlotsUI.equipment[2], ArmorType.hands, invManager.armorManager.handsId);
            UpdateArmorSlot(equipSlotsUI.equipment[3], ArmorType.legs, invManager.armorManager.legsId);
        }

        void UpdateArmorSlot(EquipmentSlot slot, ArmorType t, int id)
        {
            if(id == -1)
            {
                equipSlotsUI.ClearEqSlot(slot, ItemType.equipment);
                return;
            }

            ItemInventoryInstance inst = session.GetArmorItem(id);
            inst.slot = slot;
            equipSlotsUI.UpdateEqSlot(inst.uniqueId, slot, ItemType.equipment);
        }

        void LoadItemFromSlot(IconBase icon)
        {
           // eq_left.slotName.text = curEqSlot.slotName;
            ResourcesManager rm = ResourcesManager.singleton;

            switch (curEqSlot.slotType)
            {
                case EqSlotType.weapons:
                    LoadWeaponItem(rm,icon);
                    break;
                case EqSlotType.arrows:
                    break;
                case EqSlotType.bolts:
                    break;
                case EqSlotType.equipment:
                    break;
                case EqSlotType.rings:
                    break;
                case EqSlotType.covenant:
                    break;
                case EqSlotType.consumables:
                    LoadConsumableItem(rm);
                    break;
                default:
                    break;
            }
        }

        void LoadWeaponItem(ResourcesManager rm, IconBase icon)
        {
            ItemInventoryInstance inst = session.GetWeaponItem(icon.id);
            string weaponId = inst.itemId;
            WeaponStats stats = rm.GetWeaponStats(weaponId);
            Item item = rm.GetItem(weaponId, ItemType.weapon);
            eq_left.curItem.text = item.name_item;
            UpdateCenterOverlay(item);

            //Center
            weaponInfo.smallIcon.sprite = item.icon;
            weaponInfo.itemName.text = item.name_item;
            weaponInfo.weaponType.text = stats.weaponType;
            weaponInfo.damageType.text = stats.damageType;
            weaponInfo.skillName.text = stats.skillName;
            weaponInfo.weightCost.text = stats.weightCost.ToString();
            //change this later
            weaponInfo.durability_min.text = stats.maxDurability.ToString();
            weaponInfo.durability_max.text = stats.maxDurability.ToString();

            c_overlay.skillName.text = stats.skillName;

            UpdateUIAttackElement(AttackDefenseType.physical, weaponInfo.ap_slots, stats.a_physical.ToString());
            UpdateUIAttackElement(AttackDefenseType.magic,weaponInfo.ap_slots, stats.a_magic.ToString());
            UpdateUIAttackElement(AttackDefenseType.fire, weaponInfo.ap_slots, stats.a_fire.ToString());
            UpdateUIAttackElement(AttackDefenseType.lighting, weaponInfo.ap_slots, stats.a_lighting.ToString());
            UpdateUIAttackElement(AttackDefenseType.dark, weaponInfo.ap_slots, stats.a_dark.ToString());
            UpdateUIAttackElement(AttackDefenseType.critical, weaponInfo.ap_slots, stats.critical.ToString());

            UpdateUIAttackElement(AttackDefenseType.frost, weaponInfo.a_effects, stats.a_frost.ToString(), true);
            UpdateUIAttackElement(AttackDefenseType.curse, weaponInfo.a_effects, stats.a_curse.ToString(), true);
        //    UpdateUIAttackElement(AttackDefenseType.poison, weaponInfo.ap_slots, stats.poison.ToString());
            UpdateUIAttackElement(AttackDefenseType.frost, weaponInfo.a_effects, stats.a_frost.ToString(), true);

            UpdateUIAttackElement(AttackDefenseType.physical, weaponInfo.g_absorb, stats.d_physical.ToString());
            UpdateUIAttackElement(AttackDefenseType.magic, weaponInfo.g_absorb, stats.d_magic.ToString());
            UpdateUIAttackElement(AttackDefenseType.fire, weaponInfo.g_absorb, stats.d_fire.ToString());
            UpdateUIAttackElement(AttackDefenseType.lighting, weaponInfo.g_absorb, stats.d_lighting.ToString());
            UpdateUIAttackElement(AttackDefenseType.dark, weaponInfo.g_absorb, stats.d_dark.ToString());
            UpdateUIAttackElement(AttackDefenseType.stability, weaponInfo.g_absorb, stats.stability.ToString());

        }

        void UpdateUIAttackElement(AttackDefenseType t, List<AttDefType> l, string value, bool onTxt1 = false)
        {
            AttDefType s1 = weaponInfo.GetAttDefSlot(l, t);
            if (!onTxt1)
                s1.slot.txt2.text = value;
            else
                s1.slot.txt1.text = value;
        }

        void UpdateCenterOverlay(Item item)
        {
            c_overlay.bigIcon.sprite = item.icon;
            c_overlay.itemName.text = item.name_item;
            c_overlay.itemDescription.text = item.itemDescription;
            c_overlay.skillDescription.text = item.skillDescription;
            c_overlay.skillName.text = "-";
        }

        void LoadConsumableItem(ResourcesManager rm)
        {
         /*   string weaponId = curEqSlot.icon.id;
            Item item = rm.GetItem(curEqSlot.icon.id, ItemType.consum);

            UpdateCenterOverlay(item);*/
        }

        public static InventoryUI singleton;
        void Awake()
        {
            singleton = this;
        }
    }

    public enum EqSlotType
    {
        weapons,arrows,bolts,equipment,rings,covenant,consumables
    }

    public enum UIState
    {
        equipment,inventory,attributes,messages,options
    }

    [System.Serializable]
    public class EquipmentSlotsUI
    {
        public List<EquipmentSlot> weaponSlots = new List<EquipmentSlot>();
        public List<EquipmentSlot> arrows = new List<EquipmentSlot>();
        public List<EquipmentSlot> equipment = new List<EquipmentSlot>();
        public List<EquipmentSlot> rings = new List<EquipmentSlot>();
        public List<EquipmentSlot> cons = new List<EquipmentSlot>();
        public EquipmentSlot convenant;

        public void ClearEqSlot(EquipmentSlot s, ItemType itemType)
        {
            s.icon.icon.enabled = false;
            s.icon.id = -1;
        }

        public void UpdateEqSlot(int uniqueId, EquipmentSlot s, ItemType itemType)
        {
            ItemInventoryInstance inst = null;

            switch (itemType)
            {
                case ItemType.weapon:
                    inst = SessionManager.singleton.GetWeaponItem(uniqueId);
                    break;
                case ItemType.spell:
                    break;
                case ItemType.consum:
                    inst = SessionManager.singleton.GetConItem(uniqueId);
                    break;
                case ItemType.equipment:
                    inst = SessionManager.singleton.GetArmorItem(uniqueId);
                    break;
                default:
                    break;
            }

            if (inst == null)
            {
                return;
            }

            Item item = ResourcesManager.singleton.GetItem(inst.itemId, itemType);
            s.icon.icon.sprite = item.icon;
            s.icon.icon.enabled = true;
            s.icon.id = uniqueId;
          
        }

        public void AddSlotOnList(EquipmentSlot eq)
        {
            switch (eq.slotType)
            {
                case EqSlotType.weapons:
                    weaponSlots.Add(eq);
                    break;
                case EqSlotType.arrows:
                case EqSlotType.bolts:
                    arrows.Add(eq);
                    break;
                case EqSlotType.equipment:
                    equipment.Add(eq);
                    break;
                case EqSlotType.rings:
                    rings.Add(eq);
                    break;
                case EqSlotType.covenant:
                    convenant = eq;
                    break;
                case EqSlotType.consumables:
                    cons.Add(eq);
                    break;
                default:
                    break;
            }
        }

        public EquipmentSlot GetWeaponSlot(int index)
        {
            return weaponSlots[index];
        }

        public EquipmentSlot GetConSlot(int index)
        {
            return cons[index];
        }
    }

    [System.Serializable]
    public class EquipmentLeft
    {
        public Text slotName;
        public Text curItem;
        public Left_Inventory inventory;
    }
    
    [System.Serializable]
    public class PlayerStatus
    {
        public GameObject slot_template;
        public GameObject doubleSlot_template;
        public GameObject emptySlot;
        public Transform attGrid;
        public Transform apGrid;
        public Transform defGrid;
        public Transform resGrid;
        public List<AttributeSlot> attSlots = new List<AttributeSlot>();
        public List<AttackPowerSlot> apSlots = new List<AttackPowerSlot>();
        public List<PlayerStatusDef> defSlots = new List<PlayerStatusDef>();
        public List<PlayerStatusDef> resSlots = new List<PlayerStatusDef>();
    }

    [System.Serializable]
    public class Left_Inventory
    {
        public Slider invSlider;
        public GameObject slotTemplate;
        public Transform slotGrid;
    }

    [System.Serializable]
    public class CenterOverlay
    {
        public Image bigIcon;
        public Text itemName;
        public Text itemDescription;
        public Text skillName;
        public Text skillDescription;
    }

    [System.Serializable]
    public class WeaponInfo
    {
        public Image smallIcon;
        public GameObject slot_template;
        public GameObject slot_mini;
        public GameObject breakSlot;
        public Text itemName;
        public Text weaponType;
        public Text damageType;
        public Text skillName;
        public Text fpCost;
        public Text weightCost;
        public Text durability_min;
        public Text durability_max;

        public Transform ap_grid;
        public List<AttDefType> ap_slots = new List<AttDefType>();
        public Transform g_abs_grid;
        public List<AttDefType> g_absorb = new List<AttDefType>();
        public Transform a_effects_grid;
        public List<AttDefType> a_effects = new List<AttDefType>();
        public Transform att_grid;
        public List<AttributeSlot> att_bonus = new List<AttributeSlot>();
        public Transform att_req_grid;
        public List<AttributeSlot> att_req = new List<AttributeSlot>();

        public AttributeSlot GetAttSlot(List<AttributeSlot> l, AttributeType type)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].type == type)
                    return l[i];
            }

            return null;
        }

        public AttDefType GetAttDefSlot(List<AttDefType> l, AttackDefenseType type)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].type == type)
                    return l[i];
            }

            return null;
        }
    }

    [System.Serializable]
    public class ItemDetails
    {

    }

    [System.Serializable]
    public class AttributeSlot
    {
        public bool isBreak;
        public AttributeType type;
        public InvetoryUISlot slot;
    }

    [System.Serializable]
    public class AttDefType
    {
        public bool isBreak;
        public AttackDefenseType type;
        public InvetoryUISlot slot;
    }

    [System.Serializable]
    public class AttackPowerSlot
    {
        public InvetoryUISlot slot;
    }

    [System.Serializable]
    public class PlayerStatusDef
    {
        public AttackDefenseType type;
        public InventoryUIDoubleSlot slot;
    }
}
