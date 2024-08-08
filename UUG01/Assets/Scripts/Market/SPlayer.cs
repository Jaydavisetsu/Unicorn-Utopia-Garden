using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayer : MonoBehaviour, IShopCustomer
{
    public static SPlayer Instance { get; private set; }

    public event EventHandler OnGoldAmountChanged;

    private int goldAmount;

    private void Awake()
    {
        Instance = this;
    }

    public void AddGoldAmount(int addGoldAmount)
    {
        goldAmount += addGoldAmount;
        CurrencySystem.Instance.AddCurrency(addGoldAmount, CurrencyType.Silver);
        OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public void BoughtItem(SItem.ItemType itemType) //COULD MAKE OBJECT GO TO INVENTORY? OR BE THROWN ON THE GROUND?
    {
        Debug.Log("Bought item: " + itemType);
        switch (itemType)
        {
            //case SItem.ItemType.CarrotSeeds: /*EquipArmorNone()*/; break;
            case SItem.ItemType.CarrotSeeds: break;
        }
    }

    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        if (GetGoldAmount() >= spendGoldAmount)
        {
            goldAmount -= spendGoldAmount;
            CurrencySystem.Instance.TrySpendCurrency(spendGoldAmount, CurrencyType.Silver);
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }

}

// From BoughtItem
/*
case SItem.ItemType.ArmorNone: EquipArmorNone(); break;
case SItem.ItemType.Armor_1: EquipArmor_1(); break;
case SItem.ItemType.Armor_2: EquipArmor_2(); break;
case SItem.ItemType.HelmetNone: EquipHelmetNone(); break;
case SItem.ItemType.Helmet: EquipHelmet(); break;
case SItem.ItemType.HealthPotion: AddHealthPotion(); break;
case SItem.ItemType.Sword_2: EquipWeapon_Sword2(); break;
*/