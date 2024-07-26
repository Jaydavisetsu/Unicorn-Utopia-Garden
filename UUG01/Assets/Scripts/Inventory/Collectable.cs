using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

//[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    // Player walks into collectable item and pick it up.
    // Collectable is added to player.
    // Collectable is deleted from the scene.

    public CollectableType type;
    public Sprite icon;

    public Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Player player = collision.gameObject.GetComponent<Player>();

        if (player) //!= null
        {
            player.inventory.Add(this);
            Destroy(this.gameObject);
            /*
            Item item = GetComponent<Item>();

            if (item != null)
            {
               player.inventoryManager.Add("backpack", item);
                Destroy(this.gameObject);
            }
            */
        }
    }
}
public enum CollectableType
{
    NONE,
    CARROT_SEED
}
// Source: https://www.youtube.com/watch?v=rOW17tBiCcY&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=9
