using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory(20);
    }

    public void DropItem(Collectable item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

        Collectable droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity); // Uses the roation of item prefab.
        
        droppedItem.rb2d.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    }

    //public InventoryManager inventoryManager;
    //private TileManager tileManager;
    /*
    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
    }*/
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tileManager != null)
            {
                Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);

                string tileName = tileManager.GetTileName(position);

                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    if (tileName == "interactable" && inventoryManager.toolbar.selectedSlot.itemName == "Hoe")
                    {
                        tileManager.SetInteracted(position);
                    }
                }
            }
        }
    }*/
    /*
    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.rb2d.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    }*/

    /*public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }*/
}
// Source: https://www.youtube.com/watch?v=GXr2zNKN-fk&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10
