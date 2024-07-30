using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;

    public Dictionary<string, Item> collectableItemsDict = new Dictionary<string, Item>();

    private void Awake()
    {
        foreach (Item collectable in items)
        {
            AddItem(collectable);
        }
    }

    private void AddItem(Item item)
    {
        if (!collectableItemsDict.ContainsKey(item.data.itemName))
        {
            collectableItemsDict.Add(item.data.itemName, item);
        }
    }

    public Item GetItemByName(string key)
    {
        if (collectableItemsDict.ContainsKey(key))
        {
            return collectableItemsDict[key];
        }

        return null;
    }
}
// Source: https://www.youtube.com/watch?v=Bdaum2wMM20&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10
