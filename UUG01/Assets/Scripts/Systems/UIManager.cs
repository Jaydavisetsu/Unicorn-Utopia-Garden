using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldTime;
using static GameTimestamp;

// XP, Level, Coins, and Time are handled in their own managers.

public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager Instance { get; private set; }

    [Header("Date Text")]
    [SerializeField] private TMP_Text DateText; // Day of the Week, Date (In Info Panel).
    [SerializeField] private TMP_Text TimeText; // Time Text (In Info Panel).
    [SerializeField] private TMP_Text DaySeasonYearText; // In InvenCustoPanel.

    private void Awake()
    {
        if (Instance != null && Instance != this) // If there is more than one instance, destroy the extra.
        {
            Destroy(this);
        }
        else
        {
            Instance = this; // Sets the static instance to this instance.
        }
    }

    private void Start() // Start is called before the first frame update.
    {
        Debug.Log("UIManager Start");

        TimeManager.Instance.RegisterTracker(this); // Adding UIManager to the list of objects TimeManage will notify when the time updates.
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        // Handles the time.--------------------------------------------
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        // AM OR PM
        string amOrPm = " AM";

        // Convert hours to 12 hour clock.
        if (hours >= 12)
        {
            amOrPm = " PM"; // Time becomes PM
            Debug.Log("UIManager.cs - Hours:" + hours);
        }

        if (hours > 12)
        {
            hours = hours - 12;
        }

        TimeText.text = hours + ":" + minutes.ToString("00") + amOrPm;

        // Handles the Date.--------------------------------------------
        string season = timestamp.season.ToString();
        string dayOfTheWeek = timestamp.GetDayOfTheWeek().ToString();
        string year = timestamp.year.ToString();

        DateText.text = dayOfTheWeek + ". " + timestamp.day; // Format for the date display.
        DaySeasonYearText.text = "Day " + timestamp.day + " of " + season + ", " + "Year " + year; // Format for the date.
    }
}
// Source: https://www.youtube.com/watch?v=0nq1ZFxuEJY