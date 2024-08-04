/* 
    ------------------- Code Monkey -------------------
               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer {

    void BoughtItem(SItem.ItemType itemType);
    bool TrySpendGoldAmount(int goldAmount);

}
