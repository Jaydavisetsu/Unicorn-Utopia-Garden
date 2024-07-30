using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
    public int slotID = -1;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public GameObject highlight;

    public Inventory inventory;

    public void SetItem(Inventory.Slot slot)
    {
        itemIcon.sprite = slot.icon;
        itemIcon.color = new Color(1, 1, 1, 1);
        quantityText.text = slot.count.ToString();
    }

    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void SetHighlight(bool isOn)
    {
        if (highlight != null)
        {
            highlight.SetActive(isOn);
        }
    }
}
// Source: https://www.youtube.com/watch?v=GXr2zNKN-fk&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10