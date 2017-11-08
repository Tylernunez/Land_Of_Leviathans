using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public static class StaticFunctions
    {
        public static void DeepCopyWeapon(Weapon from, Weapon to)
        {
            to.item_id = from.item_id;
            to.oh_idle = from.oh_idle;
            to.th_idle = from.th_idle;

            to.actions = new List<Action>();
            for (int i = 0; i < from.actions.Count; i++)
            {
                Action a = new Action();

                DeepCopyActionToAction(a, from.actions[i]);
                to.actions.Add(a);
            }

            to.two_handedActions = new List<Action>();
            for (int i = 0; i < from.two_handedActions.Count; i++)
            {
                Action a = new Action();
                DeepCopyActionToAction(a, from.two_handedActions[i]);
                to.two_handedActions.Add(a);
            }

            to.parryMultiplier = from.parryMultiplier;
            to.backstabMultiplier = from.backstabMultiplier;
            to.LeftHandMirror = from.LeftHandMirror;
            to.modelPrefab = from.modelPrefab;
        }

        public static void DeepCopyActionToAction(Action to, Action from)
        {
            to.firstStep = new ActionAnim();
            to.firstStep.input = from.firstStep.input;
            to.firstStep.targetAnim = from.firstStep.targetAnim;

            to.comboSteps = new List<ActionAnim>();

            to.type = from.type;
            to.spellClass = from.spellClass;
            to.canParry = from.canParry;
            to.canBeParried = from.canBeParried;
            to.changeSpeed = from.changeSpeed;
            to.animSpeed = from.animSpeed;
            to.canBackStab = from.canBackStab;
            to.ovverideDamageAnim = from.ovverideDamageAnim;
            to.damageAnim = from.damageAnim;
            to.overrideKick = from.overrideKick;
            to.kickAnim = from.kickAnim;

            DeepCopyStepsList(from, to);
        }

        public static void DeepCopyStepsList(Action from, Action to)
        {
            for (int i = 0; i < from.comboSteps.Count; i++)
            {
                ActionAnim a = new ActionAnim();
                a.input = from.comboSteps[i].input;
                a.targetAnim = from.comboSteps[i].targetAnim;
                to.comboSteps.Add(a);
            }
        }

        public static void DeepCopyAction(Weapon w, ActionInput inp, ActionInput assign, List<Action> actionList, bool isLeftHand = false)
        {
            Action a = GetAction(assign, actionList);
            Action from = w.GetAction(w.actions, inp);
            if (from == null)
            {
                //   Debug.Log("no weapon action found");
                return;
            }

            a.firstStep.targetAnim = from.firstStep.targetAnim;
            a.comboSteps = new List<ActionAnim>();
            DeepCopyStepsList(from, a);

            a.type = from.type;
            a.spellClass = from.spellClass;
            a.canBeParried = from.canBeParried;
            a.changeSpeed = from.changeSpeed;
            a.animSpeed = from.animSpeed;
            a.canBackStab = from.canBackStab;
            a.ovverideDamageAnim = from.ovverideDamageAnim;
            a.damageAnim = from.damageAnim;
            a.parryMultiplier = w.parryMultiplier;
            a.backstabMultiplier = w.backstabMultiplier;

            a.overrideKick = from.overrideKick;
            a.kickAnim = from.kickAnim;

            if (isLeftHand)
            {
                a.mirror = true;
            }
        }

        public static void DeepCopyWeaponStats(WeaponStats from, WeaponStats to)
        {
            if (from == null)
            {
                Debug.Log(to.weaponId + " weapon stats weren't found, assinging everything as zero");
                return;
            }

            to.weaponId = from.weaponId;
        }

        public static Action GetAction(ActionInput inp, List<Action> actionSlots)
        {
            for (int i = 0; i < actionSlots.Count; i++)
            {
                if (actionSlots[i].GetFirstInput() == inp)
                    return actionSlots[i];
            }

            return null;
        }

        public static void DeepCopySpell(Spell from, Spell to)
        {
            to.item_id = from.item_id;
           // to.itemDescription = from.itemDescription;
           // to.icon = from.icon;
            to.spellType = from.spellType;
            to.spellClass = from.spellClass;
            to.projectile = from.projectile;
            to.spell_effect = from.spell_effect;
            to.particlePrefab = from.particlePrefab;

            to.actions = new List<SpellAction>();
            for (int i = 0; i < from.actions.Count; i++)
            {
                SpellAction a = new SpellAction();
                DeepCopySpellAction(a, from.actions[i]);
                to.actions.Add(a);
            }
        }

        public static void DeepCopySpellAction(SpellAction to, SpellAction from)
        {
            to.input = from.input;
            to.targetAnim = from.targetAnim;
            to.throwAnim = from.throwAnim;
            to.castTime = from.castTime;
            to.staminaCost = from.staminaCost;
            to.focusCost = from.focusCost;
        }

        public static void DeepCopyConsumable(ref Consumable to, Consumable from)
        {
            to.consumableEffect = from.consumableEffect;
            to.targetAnim = from.targetAnim;
            //to.icon = from.icon;
          //  to.itemDescription = from.itemDescription;
            to.item_id = from.item_id;
            to.itemPrefab = from.itemPrefab;
        }
    }
}
