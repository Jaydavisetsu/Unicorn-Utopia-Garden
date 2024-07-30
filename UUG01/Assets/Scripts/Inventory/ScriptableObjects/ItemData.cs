using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon = null;
}
// Source: https://www.youtube.com/watch?v=_eaMfRepWNY&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=13
