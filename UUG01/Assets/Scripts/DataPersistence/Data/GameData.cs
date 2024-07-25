using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameTimestamp;
using static WorldTime.WorldTimeWatcher;

/// <summary>
/// Class that stores the state of game.
/// Has a constructor to get a new one for a new game (multiple saves).
/// </summary>

[System.Serializable]
public class GameData 
{
    //WHEN THE GAME IS LOADED AGAIN, THE DAYNNIGHT CYCLE SPRITE DOES NOT REFRESH TO IT:(

    public Vector2 PlayerPosition; // From PlayerMovement.cs

    public Dictionary<CurrencyType, int> CurrencyAmounts; // From CurrencySystem.cs

    public int XPNow; // From LevelSystem.cs
    public int Level; // From LevelSystem.cs
    public int xpToNext; // From LevelSystem.cs

    public float DayLength; // From WorldTime.cs
    public TimeSpan CurrentTime; // From WorldTime.cs

    public int Year; // From GameTimestamp.cs
    public Season Season; // From GameTimestamp.cs
    public int Day; // From GameTimestamp.cs
    public int Hour; // From GameTimestamp.cs
    public int Minute; // From GameTimestamp.cs

    public List<Schedule> Schedule; // From WorldTimeWatcher.cs

    public GameData() //The values defined in their constructor will be the default value, so the game starts with when there is no data to load.
    {
        //this.deathCount = 0
        PlayerPosition = Vector2.zero;
        CurrencyAmounts = new Dictionary<CurrencyType, int>();

        XPNow = 0;
        Level = 1;
        xpToNext = 100;

        DayLength = 1440;
        CurrentTime = TimeSpan.FromHours(06);

        Year = 1;
        Season = (Season)1;
        Day = 1;
        Hour = 6;
        Minute = 0;

        Schedule = new List<Schedule>();
    }
}

//Source: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 not using it completely because it uses json utility and not json net.