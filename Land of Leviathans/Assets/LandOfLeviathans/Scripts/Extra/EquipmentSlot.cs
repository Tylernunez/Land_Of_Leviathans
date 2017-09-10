using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.UI
{
    public class EquipmentSlot : MonoBehaviour
    {
        public string slotName;
        public IconBase icon;
        public EqSlotType slotType;
        public Vector2 slotPos;
        public int itemPosition;
        public ArmorType armorType;

        public void Init( InventoryUI ui)
        {
            icon = GetComponent<IconBase>();
            ui.equipSlotsUI.AddSlotOnList(this);
        }
    }
}
