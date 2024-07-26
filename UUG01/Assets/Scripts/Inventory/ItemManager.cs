using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public Collectable[] collectableItems;

    private Dictionary<CollectableType, Collectable> collectableItemsDict = new Dictionary<CollectableType, Collectable>();

    private void Awake()
    {
        foreach(Collectable item in collectableItems)
        {
            AddItem(item);
        }
    }

    private void AddItem(Collectable item)
    {
        if(!collectableItemsDict.ContainsKey(item.type))
        {
            collectableItemsDict.Add(item.type, item);
        }
    }

    public Collectable GetItemByType(CollectableType type)
    {
        if(collectableItemsDict.ContainsKey(type))
        {
            return collectableItemsDict[type];
        }

        return null;   
    }

    /*public Item[] items;

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
    }*/
}
// Source: https://www.youtube.com/watch?v=Bdaum2wMM20&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10
