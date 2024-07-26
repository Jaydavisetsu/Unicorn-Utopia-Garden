using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<Slots_UI> slots = new List<Slots_UI>();

    void Update()
    {
        Refresh();
        /*if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }*/
    }

    /*public void ToggleInventory()
    {
        if(!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Setup();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }*/

    void Refresh()
    {
        if(slots.Count == player.inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].type != CollectableType.NONE)
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slotID)
    {
        Collectable itemToDrop = GameManager.current.itemManager.GetItemByType(player.inventory.slots[slotID].type);

        if(itemToDrop != null)
        {
            player.DropItem(itemToDrop);
            player.inventory.Remove(slotID);
            Refresh();
        }
    }
}
// Source: https://www.youtube.com/watch?v=GXr2zNKN-fk&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10
