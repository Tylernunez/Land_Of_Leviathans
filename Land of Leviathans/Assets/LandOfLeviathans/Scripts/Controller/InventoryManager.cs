using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InventoryManager : MonoBehaviour
    {
        public string unarmedId = "unarmed";
        public RuntimeWeapon unarmedRuntime;

        //[HideInInspector]
        public List<int> rh_weapons;
        //[HideInInspector]
        public List<int> lh_weapons;
        public List<string> spell_items;
        public List<int> consumable_items;

        public int r_index;
        public int l_index;
        public int s_index;
        public int c_index;
        List<RuntimeWeapon> r_r_weapons = new List<RuntimeWeapon>();
        List<RuntimeWeapon> r_l_weapons = new List<RuntimeWeapon>();
        List<RuntimeSpellItems> r_spells = new List<RuntimeSpellItems>();
        List<RuntimeConsumable> r_consum = new List<RuntimeConsumable>();

        List<RuntimeConsumable> consum_indexes = new List<RuntimeConsumable>();
        List<RuntimeWeapon> lh_indexes = new List<RuntimeWeapon>();
        List<RuntimeWeapon> rh_indexes = new List<RuntimeWeapon>();

        public RuntimeConsumable curConsumable;
        public RuntimeSpellItems currentSpell;
        public RuntimeWeapon rightHandWeapon;
        public RuntimeWeapon leftHandWeapon;

        RuntimeConsumable emptyItem;

        public GameObject parryCollider;
        public GameObject breathCollider;
        public GameObject blockCollider;

        StateManager states;
        UI.QuickSlot uiSlot;
        GameObject referencesParent;

        [HideInInspector]
        public ArmorManager armorManager;

        public Vector3 leftHandPos = new Vector3(0.37f, 0.188f, 0.313f);
        public Vector3 leftHandEuler = new Vector3(13.876f, -135.488f, -89.122f);

        public void Init(StateManager st)
        {
            states = st;
            uiSlot = UI.QuickSlot.singleton;

            armorManager = GetComponent<ArmorManager>();
       
            LoadLists();
            ClearReferences();
            LoadInventory();
            armorManager.Init();

            ParryCollider pr = parryCollider.GetComponent<ParryCollider>();
            pr.InitPlayer(st);
            CloseParryCollider();
            CloseBreathCollider();
            CloseBlockCollider();
        }

        void LoadLists()
        {
            rh_weapons.Clear();
            lh_weapons.Clear();
            consumable_items.Clear();
            spell_items.Clear();

            for (int i = 0; i < 3; i++)
            {
                rh_weapons.Add(-1);
                lh_weapons.Add(-1);
            }

            for (int i = 0; i < 10; i++)
            {
                consumable_items.Add(-1);
            }

            SessionManager s = SessionManager.singleton;

            for (int i = 0; i < s._eq_rh.Count; i++)
            {
                rh_weapons[i] = s._eq_rh[i];
            }

            for (int i = 0; i < s._eq_lh.Count; i++)
            {
                lh_weapons[i] = s._eq_lh[i];
            }

            for (int i = 0; i < s._eq_con.Count; i++)
            {
                consumable_items[i] = s._eq_con[i];
            }

            armorManager.chestId = s._a_chest;
            armorManager.legsId = s._a_legs;
            armorManager.headId = s._a_head;
            armorManager.handsId = s._a_hands;

            spell_items.AddRange(s.eq_spell_items);
        }
        
        public void ClearReferences()
        {
            if (r_r_weapons != null)
            {
                for (int i = 0; i < r_r_weapons.Count; i++)
                {
                    Destroy(r_r_weapons[i].weaponModel);
                }

                r_r_weapons.Clear();
            }

            if (r_l_weapons != null)
            {
                for (int i = 0; i < r_l_weapons.Count; i++)
                {
                    Destroy(r_l_weapons[i].weaponModel);
                }

                r_l_weapons.Clear();
            }

            leftHandWeapon = null;
            rightHandWeapon = null;

            if (r_consum != null)
            {
                for (int i = 0; i < r_consum.Count; i++)
                {
                    if(r_consum[i] != null)
                        if (r_consum[i].itemModel)
                            Destroy(r_consum[i].itemModel);
                }

                r_consum.Clear();
            }

            if(r_spells != null)
            {
                for (int i = 0; i < r_spells.Count; i++)
                {
                    if (r_spells[i].currentParticle)
                        Destroy(r_spells[i].currentParticle);
                }

                r_spells.Clear();
            }

            if (referencesParent)
                Destroy(referencesParent);
            referencesParent = new GameObject();
        }

        public void LoadInventory( bool updateActions = false)
        {
            SessionManager s = SessionManager.singleton;

            unarmedRuntime = WeaponToRuntimeWeapon(
                ResourcesManager.singleton.GetWeapon(unarmedId), false);
            unarmedRuntime.isUnarmed = true;

            for (int i = 0; i < 3; i++)
            {
                r_r_weapons.Add(unarmedRuntime);
                r_l_weapons.Add(unarmedRuntime);
            }

            for (int i = 0; i < 10; i++)
            {
                r_consum.Add(emptyItem);
            }

            for (int i = 0; i < rh_weapons.Count; i++)
            {
                if (rh_weapons[i] == -1) //-1 == unarmed
                {
                    r_r_weapons[i] = unarmedRuntime;
                }
                else
                {
                    ItemInventoryInstance it = s.GetWeaponItem(rh_weapons[i]);

                    RuntimeWeapon rw = WeaponToRuntimeWeapon(
                          ResourcesManager.singleton.GetWeapon(it.itemId)
                         );

                    r_r_weapons[i] = rw;
                }
            }

            for (int i = 0; i < lh_weapons.Count; i++)
            {
                if (lh_weapons[i] == -1)
                {
                    r_l_weapons[i] = unarmedRuntime;
                }
                else
                {
                    ItemInventoryInstance it = s.GetWeaponItem(lh_weapons[i]);
  
                    RuntimeWeapon rw = WeaponToRuntimeWeapon(
                       ResourcesManager.singleton.GetWeapon(it.itemId)
                       , true);

                    r_l_weapons[i] = rw;
                }
            }

            for (int i = 0; i < spell_items.Count; i++)
            {
                SpellToRuntimeSpell(
                    ResourcesManager.singleton.GetSpell(spell_items[i])
                    );
            }

            emptyItem = ConsumableToRuntime(ResourcesManager.singleton.GetConsumable("empty"));
            emptyItem.isEmpty = true;

            for (int i = 0; i < consumable_items.Count; i++)
            {
                if (consumable_items[i] == -1)
                {
                    r_consum[i] = emptyItem;
                }
                else
                {
                    ItemInventoryInstance it = s.GetConItem(consumable_items[i]);

                    RuntimeConsumable c = ConsumableToRuntime(
                        ResourcesManager.singleton.GetConsumable(it.itemId)
                        );

                    r_consum[i] = c;
                }
            }

            InitAllDamageColliders(states);
            CloseAllDamageColliders();

            MakeIndexesList();
            EquipInventory();
            armorManager.Init();

            if (updateActions)
            {
                states.actionManager.UpdateActionsOneHanded();
            }
        }

        public void EquipInventory()
        {
            if (r_index > rh_indexes.Count - 1)
                r_index = 0;
            rightHandWeapon = rh_indexes[r_index];
            
            if (l_index > lh_indexes.Count - 1)
                l_index = 0;
            leftHandWeapon = lh_indexes[l_index];
            
            EquipWeapon(rightHandWeapon, false);
            EquipWeapon(leftHandWeapon, true);
            //hasLeftHandWeapon = true;

            if (c_index > consum_indexes.Count - 1)
                    c_index = 0;

            EquipConsumable(consum_indexes[c_index]);
        
            if (r_spells.Count > 0)
            {
                if (s_index > r_spells.Count - 1)
                    s_index = 0;

                EquipSpell(r_spells[s_index]);
            }
            else
            {
                uiSlot.ClearSlot(UI.QSlotType.spell);
            }
        }

        public void EquipWeapon(RuntimeWeapon w, bool isLeft = false)
        {
            if (isLeft)
            {
                if(leftHandWeapon != null)
                {
                    leftHandWeapon.weaponModel.SetActive(false);
                }

                leftHandWeapon = w;
            }
            else
            {
                if (rightHandWeapon != null)
                {
                    rightHandWeapon.weaponModel.SetActive(false);
                }

                rightHandWeapon = w;
            }

            string targetIdle = w.instance.oh_idle;
            targetIdle += (isLeft) ? "_l" : "_r";
            states.anim.SetBool(StaticStrings.mirror, isLeft);
            states.anim.Play(StaticStrings.changeWeapon);
            states.anim.Play(targetIdle);

            Item i = ResourcesManager.singleton.GetItem(w.instance.item_id, ItemType.weapon);

            uiSlot.UpdateSlot(
                (isLeft) ?
                UI.QSlotType.lh : UI.QSlotType.rh, i.icon);

            if(w.weaponModel)
                w.weaponModel.SetActive(true);
        }

        public void EquipSpell(RuntimeSpellItems spell)
        {
            currentSpell = spell;

            Item i = ResourcesManager.singleton.GetItem(spell.instance.item_id, ItemType.spell);
            uiSlot.UpdateSlot(UI.QSlotType.spell, i.icon);
        }

        public void EquipConsumable(RuntimeConsumable consum)
        {
            curConsumable = consum;

            Item i = ResourcesManager.singleton.GetItem(consum.instance.item_id, ItemType.consum);
            uiSlot.UpdateSlot(UI.QSlotType.item, i.icon);
        }

        public Weapon GetCurrentWeapon(bool isLeft)
        {
            if (isLeft)
                return leftHandWeapon.instance;
            else
                return rightHandWeapon.instance;
        }

        public void OpenAllDamageColliders()
        {
            if (rightHandWeapon != null)
            {
                if (rightHandWeapon.w_hook != null)
                    rightHandWeapon.w_hook.OpenDamageColliders();
            }

            if (leftHandWeapon != null)
            {
                if (leftHandWeapon.w_hook != null)
                    leftHandWeapon.w_hook.OpenDamageColliders();
            }
        }

        public void CloseAllDamageColliders()
        {
            if (rightHandWeapon != null)
            {
                if (rightHandWeapon.w_hook != null)
                    rightHandWeapon.w_hook.CloseDamageColliders();
            }

            if (leftHandWeapon != null)
            {
                if (leftHandWeapon.w_hook != null)
                    leftHandWeapon.w_hook.CloseDamageColliders();
            }
        }

        public void InitAllDamageColliders(StateManager states)
        {
            if (rightHandWeapon != null)
            {
                if (rightHandWeapon.w_hook != null)
                    rightHandWeapon.w_hook.InitDamageColliders(states);
            }

            if (leftHandWeapon != null)
            {
                if (leftHandWeapon.w_hook != null)
                    leftHandWeapon.w_hook.InitDamageColliders(states);
            }
        }

        public void CloseParryCollider()
        {
            parryCollider.SetActive(false);
        }

        public void OpenParryCollider()
        {
            parryCollider.SetActive(true);
        }

        public RuntimeSpellItems SpellToRuntimeSpell(Spell s, bool isLeft = false)
        {
            GameObject go = new GameObject();
            go.transform.parent = referencesParent.transform;
            RuntimeSpellItems inst = go.AddComponent<RuntimeSpellItems>();
            inst.instance = new Spell();
            StaticFunctions.DeepCopySpell(s, inst.instance);
            go.name = s.item_id;

            r_spells.Add(inst);
            return inst;
        }

        public void CreateSpellParticle(RuntimeSpellItems inst, bool isLeft, bool parentUnderRoot = false)
        {
            if (inst.currentParticle == null)
            {
                inst.currentParticle = Instantiate(inst.instance.particlePrefab) as GameObject;
                inst.p_hook = inst.currentParticle.GetComponentInChildren<ParticleHook>();
                inst.p_hook.Init();
            }

            if (!parentUnderRoot)
            {
                Transform p = states.anim.GetBoneTransform((isLeft) ? HumanBodyBones.LeftHand : HumanBodyBones.RightHand);
                inst.currentParticle.transform.parent = p;
                inst.currentParticle.transform.localRotation = Quaternion.identity;
                inst.currentParticle.transform.localPosition = Vector3.zero;
            }
            else
            {
                inst.currentParticle.transform.parent = transform;
                inst.currentParticle.transform.localRotation = Quaternion.identity;
                inst.currentParticle.transform.localPosition = new Vector3(0, 1.5f, .9f);
            }
        }

        public RuntimeWeapon WeaponToRuntimeWeapon (Weapon w, bool isLeft = false)
        {
            GameObject go = new GameObject();
            go.transform.parent = referencesParent.transform;
            RuntimeWeapon inst = go.AddComponent<RuntimeWeapon>();
            go.name = w.item_id;

            inst.instance = new Weapon();
            StaticFunctions.DeepCopyWeapon(w, inst.instance);

            inst.weaponStats = new WeaponStats();
            WeaponStats w_stats = ResourcesManager.singleton.GetWeaponStats(w.item_id);
            StaticFunctions.DeepCopyWeaponStats(w_stats, inst.weaponStats);

            inst.weaponModel = Instantiate(inst.instance.modelPrefab) as GameObject;
            Transform p = states.anim.GetBoneTransform((isLeft) ? HumanBodyBones.LeftHand : HumanBodyBones.RightHand);
            inst.weaponModel.transform.parent = p;


            if (!isLeft)
            {
                inst.weaponModel.transform.localPosition = Vector3.zero;
                inst.weaponModel.transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                inst.weaponModel.transform.localPosition = leftHandPos;
                inst.weaponModel.transform.localEulerAngles = leftHandEuler;
            }

            inst.weaponModel.transform.localScale = Vector3.one;

            inst.w_hook = inst.weaponModel.GetComponentInChildren<WeaponHook>();
            inst.w_hook.InitDamageColliders(states);
            foreach(GameObject d in inst.w_hook.damageCollider)
            {
                d.GetComponent<DamageCollider>().InitPlayer(states);
            }

            inst.weaponModel.SetActive(false);
            return inst;
        }

        public RuntimeConsumable ConsumableToRuntime(Consumable c)
        {
            GameObject go = new GameObject();
            go.transform.parent = referencesParent.transform;
            RuntimeConsumable inst = go.AddComponent<RuntimeConsumable>();
            go.name = c.item_id;

            inst.instance = new Consumable();
            StaticFunctions.DeepCopyConsumable(ref inst.instance, c);

            if(inst.instance.itemPrefab != null)
            {
                GameObject model = Instantiate(inst.instance.itemPrefab) as GameObject;
                Transform p = states.anim.GetBoneTransform(HumanBodyBones.RightHand);
                model.transform.parent = p;
                model.transform.localPosition = Vector3.zero;
                model.transform.localEulerAngles = Vector3.zero;
                model.transform.localScale = Vector3.one;

                inst.itemModel = model;
                inst.itemModel.SetActive(false);
            }

            return inst;
        }

        public void ChangeToNextWeapon(bool isLeft)
        {
            states.isTwoHanded = false;
            states.HandleTwoHanded();

            if (isLeft)
            {
                if (lh_indexes.Count == 0)
                    return;

                if(l_index < lh_indexes.Count -1)     
                    l_index++;      
                else
                    l_index = 0;
    
                EquipWeapon(lh_indexes[l_index], true);
            }
            else
            {
                if (rh_indexes.Count == 0)
                    return;

                if (r_index < rh_indexes.Count - 1)
                    r_index++;
                else
                    r_index = 0;

                EquipWeapon(rh_indexes[r_index]);
            }

            states.actionManager.UpdateActionsOneHanded();
        }

        public void ChangeToNextSpell()
        {
            if (s_index < r_spells.Count - 1)
                s_index++;
            else
                s_index = 0;

            EquipSpell(r_spells[s_index]);
        }

        public void ChangeToNextConsumable()
        {
            if (c_index < consum_indexes.Count - 1)
                c_index++;
            else
                c_index = 0;

            EquipConsumable(consum_indexes[c_index]);
        }

        void MakeIndexesList()
        {
            consum_indexes.Clear();

            for (int i = 0; i < r_consum.Count; i++)
            {
                if(r_consum[i].isEmpty)
                    continue;

                consum_indexes.Add(r_consum[i]);
            }

            if(consum_indexes.Count < 2)
            {
                consum_indexes.Add(emptyItem);
            }

            rh_indexes.Clear();

            for (int i = 0; i < r_r_weapons.Count; i++)
            {
                if (r_r_weapons[i].isUnarmed)
                    continue;

                rh_indexes.Add(r_r_weapons[i]);
            }

            if(rh_indexes.Count < 2)
            {
                rh_indexes.Add(unarmedRuntime);
            }

            lh_indexes.Clear();

            for (int i = 0; i < r_l_weapons.Count; i++)
            {
                if (r_l_weapons[i].isUnarmed)
                    continue;

                lh_indexes.Add(r_l_weapons[i]);
            }

            if (lh_indexes.Count < 2)
            {
                lh_indexes.Add(unarmedRuntime);
            }
        }

        #region Delegate Calls
        public void OpenBreathCollider()
        {
            breathCollider.SetActive(true);
        }

        public void CloseBreathCollider()
        {
            breathCollider.SetActive(false);
        }

        public void OpenBlockCollider()
        {
            blockCollider.SetActive(true);
        }

        public void CloseBlockCollider()
        {
            blockCollider.SetActive(false);
        }

        public void EmitSpellParticle()
        {
            currentSpell.p_hook.Emit(1);
        }
        #endregion
    }

    [System.Serializable]
    public class Item
    {
        public string item_id;
        public string name_item;
        public string itemDescription;
        public string skillDescription;
        public Sprite icon;
    }

    [System.Serializable]
    public class Weapon
    {
        public string item_id;
        public string oh_idle;
        public string th_idle;

        public List<Action> actions;
        public List<Action> two_handedActions;

        public float parryMultiplier;
        public float backstabMultiplier;
        public bool LeftHandMirror;

        public GameObject modelPrefab;
  
        public Action GetAction(List<Action> l, ActionInput inp)
        {
            if (l == null)
                return null;

            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].GetFirstInput() == inp)
                {
                    return l[i];
                }
            }

            return null;
        }
    }

    [System.Serializable]
    public class Spell
    {
        public string item_id;
        public SpellType spellType;
        public SpellClass spellClass;
        public List<SpellAction> actions = new List<SpellAction>();
        public GameObject projectile;
        public GameObject particlePrefab;
        public string spell_effect;

        public SpellAction GetAction(List<SpellAction> l, ActionInput inp)
        {
            if (l == null)
                return null;

            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].input == inp)
                {
                    return l[i];
                }
            }

            return null;
        }

    }

    [System.Serializable]
    public class Consumable
    {
        public string item_id;
        public string consumableEffect;
        public string targetAnim;
        public GameObject itemPrefab;

    }
    
}