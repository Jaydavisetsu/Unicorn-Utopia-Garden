using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singletone pattern
    public static GameManager current;

    // Save Panel
    public GameObject panel;

    public ItemManager itemManager;

    private void Awake()
    {
        if(current != null && current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //initialize fields
            current = this;
        }

        DontDestroyOnLoad(this.gameObject);
        itemManager = GetComponent<ItemManager>();
    }

    public void GetXP(int amount)
    {
        XPAddedGameEvent info = new XPAddedGameEvent(amount);
        EventManager.Instance.QueueEvent(info);
    }

    public void GetCoins(int amount)
    {
        CurrencyChangeGameEvent info = new CurrencyChangeGameEvent(amount, CurrencyType.Silver);

        EventManager.Instance.QueueEvent(info);  
    }
}
// Source: https://www.youtube.com/watch?v=Txx_uCxIpdE&list=LL&index=2&t=428s
// Source: https://www.youtube.com/watch?v=Bdaum2wMM20&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10