using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Slots_UI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    public void SetItem(Inventory.Slot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }

    public void SetEmpty() 
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    /*public void SetHighlight(bool isOn)
    {
        if (highlight != null)
        {
            highlight.SetActive(isOn);
        }
    }*/
}
// Source: https://www.youtube.com/watch?v=GXr2zNKN-fk&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10