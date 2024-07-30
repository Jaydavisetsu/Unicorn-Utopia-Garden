using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class Toolbar_UI : MonoBehaviour
{
    public List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;

    private void Start()
    {
        SelectSlot(0);
    }

    private void Update()
    {
        CheckAlphaNumericKeys();
    }

    public void SelectSlot(Slot_UI slot)
    {
        SelectSlot(slot.slotID);
    }

    public void SelectSlot(int index)
    {
        if (toolbarSlots.Count == 9)
        {
            if (selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }

            selectedSlot = toolbarSlots[index];
            selectedSlot.SetHighlight(true);

            GameManager.current.player.inventoryManager.toolbar.SelectSlot(index);
        }
    }

    private void CheckAlphaNumericKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlot(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectSlot(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectSlot(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectSlot(8);
        }
    }
}

// Source: https://www.youtube.com/watch?v=DUDmsFmKw8E&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=16