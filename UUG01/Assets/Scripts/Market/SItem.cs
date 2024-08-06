/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SItem
{

    public enum ItemType {
        CarrotSeeds,
        CabbageSeeds,
        TomatoSeeds,
        RadishSeeds,
        CornSeeds,
        RedRoseSeeds,
        HeliopsisSeeds,
        DaffodilSeeds
        /*ArmorNone,
        Armor_1,
        Armor_2,
        HelmetNone,
        Helmet,
        HealthPotion,
        Sword_1,
        Sword_2*/
    }

    public static int GetCost(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.CarrotSeeds:        return 15;
        case ItemType.TomatoSeeds:        return 15;
        case ItemType.RadishSeeds:        return 15;
        case ItemType.CornSeeds:        return 15;
        case ItemType.CabbageSeeds:        return 15;
        case ItemType.RedRoseSeeds:        return 15;
        case ItemType.HeliopsisSeeds:        return 15;
        case ItemType.DaffodilSeeds:        return 15;

        /*
        case ItemType.ArmorNone:        return 0;
        case ItemType.Armor_1:          return 30;
        case ItemType.Armor_2:          return 100;
        case ItemType.HelmetNone:       return 0;
        case ItemType.Helmet:           return 90;
        case ItemType.HealthPotion:     return 30;
        case ItemType.Sword_1:          return 0;
        case ItemType.Sword_2:          return 150;
        */
        }
    }

    public static Sprite GetSprite(ItemType itemType) {
        switch (itemType) {
        default:
        case ItemType.CarrotSeeds:    return GameAssets.i.s_CarrotSeed;
        case ItemType.TomatoSeeds:    return GameAssets.i.s_TomatoSeeds;
        case ItemType.RadishSeeds:    return GameAssets.i.s_RadishSeeds;
        case ItemType.CornSeeds:    return GameAssets.i.s_CornSeeds;
        case ItemType.CabbageSeeds:    return GameAssets.i.s_CabbageSeeds;
        case ItemType.RedRoseSeeds:    return GameAssets.i.s_RedRoseSeeds;
        case ItemType.HeliopsisSeeds:    return GameAssets.i.s_HeliopsisSeeds;
        case ItemType.DaffodilSeeds:    return GameAssets.i.s_DaffodilSeeds;

        /*
        case ItemType.ArmorNone:    return GameAssets.i.s_ArmorNone;
        case ItemType.Armor_1:      return GameAssets.i.s_Armor_1;
        case ItemType.Armor_2:      return GameAssets.i.s_Armor_2;
        case ItemType.HelmetNone:   return GameAssets.i.s_HelmetNone;
        case ItemType.Helmet:       return GameAssets.i.s_Helmet;
        case ItemType.HealthPotion: return GameAssets.i.s_HealthPotion;
        case ItemType.Sword_1:      return GameAssets.i.s_Sword_1;
        case ItemType.Sword_2:      return GameAssets.i.s_Sword_2;
        */
        }
    }

}
