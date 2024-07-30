using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    // Player walks into collectable item and pick it up.
    // Collectable is added to player.
    // Collectable is deleted from the scene.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Item item = GetComponent<Item>();

            if (item != null)
            {
                player.inventoryManager.Add("Backpack", item);
                Destroy(this.gameObject);
            }
        }
    }
}
// Source: https://www.youtube.com/watch?v=rOW17tBiCcY&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=9
