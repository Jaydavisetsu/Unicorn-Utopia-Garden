using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencySystem : MonoBehaviour, IDataPersistence
{
    public static CurrencySystem Instance { get; private set; }

    // All player's treasures.
    private static Dictionary<CurrencyType, int> CurrencyAmounts = new Dictionary<CurrencyType, int>();

    // Prefab for xp rewards when buying
    [SerializeField] private GameObject XpDisplayPrefab;
    private TextMeshProUGUI XpRewardAmountText;

    // Prefab for not enough currency when buying
    [SerializeField] private GameObject NotEnoughtCurrencyPrefab;

    // Currency texts.
    [SerializeField] private List<GameObject> texts;

    // Currency texts in a dictionary (for easier access).
    private Dictionary<CurrencyType, TextMeshProUGUI> currencyTexts =
        new Dictionary<CurrencyType, TextMeshProUGUI>();

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;

            // Reparenting the GameObject AudioManager because DontDestroyOnLoad only works on root GameObjects and not child game objects.
            Instance.transform.SetParent(null); // Making it a root GameObject.
            DontDestroyOnLoad(gameObject); // Optional: Persist this instance across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Initialize dictionaries.
        for (int i = 0; i < texts.Count; i++)
        {
            CurrencyAmounts.Add((CurrencyType)i, 0);
            currencyTexts.Add((CurrencyType)i, texts[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }

        XpRewardAmountText = XpDisplayPrefab.transform.Find("XpRewardAmountText").GetComponent<TextMeshProUGUI>();
    }
    public void LoadData(GameData data) //Method from IDataPersistence.
    {
        if (CurrencyAmounts == null)
        {
            CurrencyAmounts = new Dictionary<CurrencyType, int>();
        }

        CurrencyAmounts.Clear();

        foreach (var kvp in data.CurrencyAmounts)
        {
            CurrencyAmounts.Add(kvp.Key, kvp.Value);
        }

        //------------------------------------

        //give the player some currency
        //CurrencyAmounts[CurrencyType.Silver] = 100;
        
        // Check if Silver currency is already present in loaded data
        if (!CurrencyAmounts.ContainsKey(CurrencyType.Silver))
        {
            // If not present, initialize Silver to 100
            CurrencyAmounts[CurrencyType.Silver] = 80;
        }

        //pdate UI texts to reflect the right amount
        UpdateUI();
    }

    public void SaveData(GameData data) //Method from IDataPersistence.
    {
        data.CurrencyAmounts = new Dictionary<CurrencyType, int>();

        // The foreach might not be needed?
        foreach (var kvp in CurrencyAmounts)
        {
            data.CurrencyAmounts.Add(kvp.Key, kvp.Value);
        }
    }

    //This might not be needed?
    private void Start()
    {
        //add listeners for currency change events and not enough currency
        EventManager.Instance.AddListener<CurrencyChangeGameEvent>(OnCurrencyChange);
        EventManager.Instance.AddListener<NotEnoughCurrencyGameEvent>(OnNotEnough);
    }

    private void UpdateUI()
    {
        // This might not be needed?
        
        /*//set new currency amounts
        for (int i = 0; i < texts.Count; i++)
        {
            currencyTexts[(CurrencyType)i].text = CurrencyAmounts[(CurrencyType)i].ToString();
        }*/

        foreach (var kvp in CurrencyAmounts)
        {
            if (currencyTexts.ContainsKey(kvp.Key))
            {
                currencyTexts[kvp.Key].text = kvp.Value.ToString();
                Debug.Log($"CurrencySystem.cs: Updated UI for {kvp.Key}: {kvp.Value}");
            }
        }
    }

    public bool TrySpendCurrency(int spendAmount, CurrencyType currencyType) // This is to use with the market.
    {
        Debug.Log($"CurrencySystem.cs: Attempting to spend {spendAmount} of {currencyType}");

        if (CurrencyAmounts.ContainsKey(currencyType) && CurrencyAmounts[currencyType] >= spendAmount)
        {
            CurrencyAmounts[currencyType] -= spendAmount;

            Debug.Log($"CurrencySystem.cs: Spent {spendAmount} of {currencyType}. New amount: {CurrencyAmounts[currencyType]}");

            UpdateUI();

            // This is only for level one to two.
            XPAddedGameEvent info = new XPAddedGameEvent(18);
            EventManager.Instance.QueueEvent(info);
            ActivateForDuration(1f, 18);

            return true;
        }
        else
        {
            ActivateForDuration(2f);
            Debug.Log($"CurrencySystem.cs: Not enough {currencyType}. Current amount: {CurrencyAmounts[currencyType]}");

            return false;
        }
    }

    public void AddCurrency(int amount, CurrencyType currencyType)
    {
        if (CurrencyAmounts.ContainsKey(currencyType))
        {
            CurrencyAmounts[currencyType] += amount;
            UpdateUI();
        }
    }

    // This method can be called to activate the XpRewardPrefab for a specific duration
    public void ActivateForDuration(float duration, int xpreward)
    {
        // Start the coroutine with the specified duration
        StartCoroutine(ActivateForTime(duration, xpreward));
    }

    private IEnumerator ActivateForTime(float duration, int xpreward)
    {
        // Ensure the GameObject is active
        XpDisplayPrefab.SetActive(true);

        XpRewardAmountText.text = xpreward.ToString();

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the GameObject
        XpDisplayPrefab.SetActive(false);
    }

    // This method can be called to activate the NotEnoughCurrencyPrefab for a specific duration
    public void ActivateForDuration(float duration)
    {
        // Start the coroutine with the specified duration
        StartCoroutine(ActivateForTime(duration));
    }

    private IEnumerator ActivateForTime(float duration)
    {
        // Ensure the GameObject is active
        NotEnoughtCurrencyPrefab.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the GameObject
        NotEnoughtCurrencyPrefab.SetActive(false);
    }

    //This might not be needed. These are the original methods.
    private void OnCurrencyChange(CurrencyChangeGameEvent info)
    {
        /*//if the player's trying to spend currency
        if (info.amount < 0)
        {
            if (CurrencyAmounts[info.currencyType] < Math.Abs(info.amount))
            {
                EventManager.Instance.QueueEvent(new NotEnoughCurrencyGameEvent(info.amount, info.currencyType));
                return;
            }

            EventManager.Instance.QueueEvent(new EnoughCurrencyGameEvent());
        }*/

        CurrencyAmounts[info.currencyType] += info.amount;

        UpdateUI();
    }

    private void OnNotEnough(NotEnoughCurrencyGameEvent info)
    {
        // Display that the player doesn't have any currency
        Debug.Log($"CurrencySystem.cs (OnNotEnough): You don't have enough of {info.amount} {info.currencyType}");
    }
}

public enum CurrencyType
{
    Silver
}
// Source: https://www.youtube.com/watch?v=Txx_uCxIpdE&list=LL&index=2&t=428s