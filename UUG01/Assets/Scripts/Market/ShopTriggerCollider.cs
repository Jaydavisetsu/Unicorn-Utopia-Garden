using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class ShopTriggerCollider : MonoBehaviour
{

    [SerializeField] private UI_Shop uiShop;
    [SerializeField] private GameObject marketPanel;
    [SerializeField] private GameObject uiOptionsMenuPanel;

    private void Start()
    {
        marketPanel.SetActive(false);
        IShopCustomer shopCustomer = GetComponent<IShopCustomer>();
        Debug.Log("ShopTriggerCollider.cs Started already");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            uiShop.Show(shopCustomer);
            marketPanel.gameObject.SetActive(true);
            uiOptionsMenuPanel.gameObject.SetActive(false);
            Debug.Log("Showing shopcustomer and market panel");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            uiShop.Hide();
            marketPanel.gameObject.SetActive(false);
            uiOptionsMenuPanel.gameObject.SetActive(true);
            Debug.Log("Hiding shopcustomer and market panel");
        }
    }

}
