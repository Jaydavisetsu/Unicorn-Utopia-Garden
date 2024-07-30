using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotsCount = 20;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotsCount = 9;


    private void Start()
    {
        backpack = new Inventory(backpackSlotsCount);
        toolbar = new Inventory(toolbarSlotsCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public Inventory GetInventoryByName(string name)
    {
        if (inventoryByName.ContainsKey(name))
        {
            return inventoryByName[name];
        }

        return null;
    }

    public void Add(string inventoryName, Item item)
    {
        if (inventoryByName != null)
        {
            if (inventoryByName.ContainsKey(inventoryName))
            {
                inventoryByName[inventoryName].Add(item);
            }
        }
    }
}
// Souce: https://www.youtube.com/watch?v=hG4DS9gLlcA&t=773s
