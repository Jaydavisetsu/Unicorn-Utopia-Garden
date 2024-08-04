using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using Unity.Properties;
public class UI_Shop : MonoBehaviour
{
    public Canvas canvas;

    private Transform container;
    private Transform shopItemTemplate;
    //private IShopCustomer shopCustomer;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        Debug.Log("UI_Shop.cs Awakened");
    }

    private void Start() // This is where to add new item buttons on the UI.
    {
        // Create a parent GameObject for the grid
        GameObject gridParent = new GameObject("container");
        gridParent.transform.SetParent(canvas.transform, false);

        // Add GridLayoutGroup component to the parent
        GridLayoutGroup gridLayoutGroup = gridParent.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical;
        gridLayoutGroup.childAlignment = TextAnchor.UpperLeft;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;

        // Adjust grid cell size and spacing as needed
        gridLayoutGroup.cellSize = new Vector2(450, 130); // Adjust to your desired button size
        gridLayoutGroup.spacing = new Vector2(10, 10); // Adjust for spacing between buttons

        // Setting container to the new grid parent
        //container = gridParent.transform;
        Transform shopItemTransform = Instantiate(shopItemTemplate);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        shopItemTransform.transform.SetParent(gridParent.transform, false);


        CreateItemButton(SItem.ItemType.CarrotSeeds, SItem.GetSprite(SItem.ItemType.CarrotSeeds), "Carrot Seeds", SItem.GetCost(SItem.ItemType.CarrotSeeds), 0);

        CreateItemButton(SItem.ItemType.CarrotSeeds, SItem.GetSprite(SItem.ItemType.CarrotSeeds), "Carrot Seeds", SItem.GetCost(SItem.ItemType.CarrotSeeds), 1);
        /*
        CreateItemButton(SItem.ItemType.Armor_1, SItem.GetSprite(SItem.ItemType.Armor_1), "Armor 1", SItem.GetCost(SItem.ItemType.Armor_1), 0);
        CreateItemButton(SItem.ItemType.Armor_2, SItem.GetSprite(SItem.ItemType.Armor_2), "Armor 2", SItem.GetCost(SItem.ItemType.Armor_2), 1);
        CreateItemButton(SItem.ItemType.Helmet, SItem.GetSprite(SItem.ItemType.Helmet), "Helmet", SItem.GetCost(SItem.ItemType.Helmet), 2);
        CreateItemButton(SItem.ItemType.Sword_2, SItem.GetSprite(SItem.ItemType.Sword_2), "Sword", SItem.GetCost(SItem.ItemType.Sword_2), 3);
        CreateItemButton(SItem.ItemType.HealthPotion, SItem.GetSprite(SItem.ItemType.HealthPotion), "HealthPotion", SItem.GetCost(SItem.ItemType.HealthPotion), 4);*/

        //Hide();
    }

    private void CreateItemButton(SItem.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        //button.transform.SetParent(gridParent.transform, false);
        /*
        float shopItemHeight = 90f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        */

        // Updating UI elements with item details
        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () => {
            // Clicked on shop item button
            TryBuyItem(itemType, itemCost);
        };

        Debug.Log("UI_Shop.cs - Button Created.");
    }

    private void TryBuyItem(SItem.ItemType itemType, int itemCost)
    {
        if (CurrencySystem.Instance.TrySpendCurrency(itemCost, CurrencyType.Silver))
        {
            // Successfully purchased item
            Debug.Log($"Successfully bought {itemType} for {itemCost} gold.");
            // Inform the customer (if needed)
            //shopCustomer.BoughtItem(itemType);
        }
        else
        {
            Debug.Log("Not enough silver!");
            // Display a message or tooltip for insufficient funds
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        // Optionally, if you need to interact with the shop customer
        //this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    // These were the original methods.
    /*
    private void TryBuyItem(SItem.ItemType itemType)
    {
        if (shopCustomer.TrySpendGoldAmount(SItem.GetCost(itemType)))
        {
            // Can afford cost
            shopCustomer.BoughtItem(itemType);
        }
        else
        {
            Debug.Log("Not enough!");
            //Tooltip_Warning.ShowTooltip_Static("Cannot afford " + SItem.GetCost(itemType) + "!");
        }
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }
    */

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}