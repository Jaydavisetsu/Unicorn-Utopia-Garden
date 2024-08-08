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
        //container = transform.Find("container");
        //shopItemTemplate = container.Find("shopItemTemplate");
        //shopItemTemplate.gameObject.SetActive(false);
        Debug.Log("UI_Shop.cs Awakened");

        container = transform.Find("container").GetComponent<RectTransform>();
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);

        // Setup GridLayoutGroup
        GridLayoutGroup gridLayoutGroup = container.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical; // or Horizontal
        gridLayoutGroup.childAlignment = TextAnchor.UpperLeft;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount; // or FixedColumnCount/FixedRowCount
        gridLayoutGroup.cellSize = new Vector2(450, 130); // Adjust button size
        gridLayoutGroup.spacing = new Vector2(20, 10); // Adjust spacing

        // Optionally use ContentSizeFitter
        ContentSizeFitter contentSizeFitter = container.gameObject.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter == null)
        {
            contentSizeFitter = container.gameObject.AddComponent<ContentSizeFitter>();
        }
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    private void Start() // This is where to add new item buttons on the UI.
    {
        CreateItemButton(SItem.ItemType.CarrotSeeds, SItem.GetSprite(SItem.ItemType.CarrotSeeds), "Carrot Seeds", SItem.GetCost(SItem.ItemType.CarrotSeeds), 0);

        CreateItemButton(SItem.ItemType.TomatoSeeds, SItem.GetSprite(SItem.ItemType.TomatoSeeds), "Tomato Seeds", SItem.GetCost(SItem.ItemType.TomatoSeeds), 1);

        CreateItemButton(SItem.ItemType.RadishSeeds, SItem.GetSprite(SItem.ItemType.RadishSeeds), "Radish Seeds", SItem.GetCost(SItem.ItemType.RadishSeeds), 2);

        CreateItemButton(SItem.ItemType.CornSeeds, SItem.GetSprite(SItem.ItemType.CornSeeds), "Corn Seeds", 
            SItem.GetCost(SItem.ItemType.CornSeeds), 3);

        CreateItemButton(SItem.ItemType.CabbageSeeds, SItem.GetSprite(SItem.ItemType.CabbageSeeds), "Cabbage Seeds", SItem.GetCost(SItem.ItemType.CabbageSeeds), 4);

        CreateItemButton(SItem.ItemType.RedRoseSeeds, SItem.GetSprite(SItem.ItemType.RedRoseSeeds), "Red Rose Seeds", SItem.GetCost(SItem.ItemType.RedRoseSeeds), 5);

        CreateItemButton(SItem.ItemType.HeliopsisSeeds, SItem.GetSprite(SItem.ItemType.HeliopsisSeeds), "Heliopsis Seeds", SItem.GetCost(SItem.ItemType.HeliopsisSeeds), 6);

        CreateItemButton(SItem.ItemType.DaffodilSeeds, SItem.GetSprite(SItem.ItemType.DaffodilSeeds), "Daffodil Seeds", SItem.GetCost(SItem.ItemType.DaffodilSeeds), 7);

        //Hide();
    }

    private void CreateItemButton(SItem.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

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
            Debug.Log($"UI_Shop.cs: Successfully bought {itemType} for {itemCost} gold.");
            // Inform the customer (if needed)
            //shopCustomer.BoughtItem(itemType);
        }
        else
        {
            Debug.Log("UI_Shop.cs: Not enough silver!");
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