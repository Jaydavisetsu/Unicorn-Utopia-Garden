using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerA : MonoBehaviour
{
    // singletone pattern
    public static GameManagerA current;

    // Save Panel
    public GameObject panel;

    private void Awake()
    {
        //initialize fields
        current = this;
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
