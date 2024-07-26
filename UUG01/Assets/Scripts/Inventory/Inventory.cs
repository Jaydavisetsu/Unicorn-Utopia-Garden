using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        //public string itemName;
        public int count;
        public int maxAllowed;

        public Sprite icon;

        public Slot()
        {
            type = CollectableType.NONE;

            //itemName = "";
            count = 0;
            maxAllowed = 99;
        }

        public bool CanAddItem()
        {
            if(count < maxAllowed)
            {
                return true;
            }

            return false;
        }

        public void AddItem(Collectable item)
        {
            this.type = item.type;
            this.icon = item.icon;
            count++;
        }

        public void RemoveItem()
        {
            if(count > 0)
            {
                count--;

                if(count == 0)
                {
                    icon = null;
                    type = CollectableType.NONE;
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();
    //public Slot selectedSlot = null;

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            slots.Add(new Slot());
        }
    }

    public void Add(Collectable item)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type == item.type && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach(Slot slot in slots)
        {
            if (slot.type == CollectableType.NONE)
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }

    
}
// Source: https://www.youtube.com/watch?v=rOW17tBiCcY&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=9


