using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class PickableItemsManager : MonoBehaviour
    {
        public List<WorldInteraction> inters = new List<WorldInteraction>();
        public List<PickableItem> pick_items = new List<PickableItem>();
        public PickableItem itemCanidate;
        public WorldInteraction interCandidate;

        int frameCount;
        public int frameCheck = 15;

        public void Tick()
        {
            if (frameCount < frameCheck)
            {
                frameCount++;
                return;
            }
            frameCount = 0;

            for (int i = 0; i < pick_items.Count; i++)
            {
                float d = Vector3.Distance(pick_items[i].transform.position, transform.position);

                if(d < 2)
                {
                    itemCanidate = pick_items[i];
                }
                else
                {
                    if (itemCanidate == pick_items[i])
                        itemCanidate = null;
                }               
            }

            for (int i = 0; i < inters.Count; i++)
            {
                float d = Vector3.Distance(inters[i].transform.position, transform.position);
                if(d < 2)
                {
                    interCandidate = inters[i];
                }
                else
                {
                    if (interCandidate == inters[i])
                        interCandidate = null;
                }
            }
        }

        public void PickCanidate()
        {
            if (itemCanidate == null)
                return;

            SessionManager s = SessionManager.singleton;

            for (int i = 0; i < itemCanidate.items.Length; i++)
            {
                PickItemContainer c = itemCanidate.items[i];
                s.AddItem(c.itemId, c.itemType);
            }

            if (pick_items.Contains(itemCanidate))
                pick_items.Remove(itemCanidate);

            Destroy(itemCanidate.gameObject);
            itemCanidate = null;
        }

    }
}