using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameTimestamp;

public class TimeManager : MonoBehaviour, IDataPersistence
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    //[SerializeField] private WorldTime.WorldTime worldTime;
    [SerializeField] GameTimestamp timestamp;
    public float TimeScale = 1.0f;

    private List<ITimeTracker> listeners = new List<ITimeTracker>(); // List of objects to inform of changes to the time.

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

    void Start() // Start is called before the first frame update.
    {
        //Debug.Log("TimeManager cs Start");

        StartCoroutine(TimeUpdate()); // Cannot use this because of WorldTime.cs
    }

    IEnumerator TimeUpdate()  // Cannot use this because of WorldTime.cs.
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / TimeScale); // Minute increases by every second (or as set in TimeScale).
            Tick();
        }
    }

    public void Tick() // A tick of the in-game time. Cannot use this because of WorldTime.cs.
    {
        timestamp.UpdateClock();

        foreach (ITimeTracker listener in listeners) // Inform each of the listeners of the new time state.
        {
            listener.ClockUpdate(timestamp);
        }
    }
    
    // Handling Listeners-----------

    public void RegisterTracker (ITimeTracker listener) // Add the object to the list of listeners.
    {
        listeners.Add(listener);
    }

    public void UnregisterTracker (ITimeTracker listener) // Remove the object from the list of listeners.
    {
        listeners.Remove(listener);
    }

    public void LoadData(GameData gameData)
    {
        if (timestamp == null)
        {
            timestamp = new GameTimestamp(0, GameTimestamp.Season.Spring, 1, 06, 0); // Initialise the time stamp.
        }
        else if (Instance != null)
        {
            Instance = this;
        }

        this.timestamp.year = gameData.Year;
        this.timestamp.season = gameData.Season;
        this.timestamp.day = gameData.Day;
        this.timestamp.hour = gameData.Hour;
        this.timestamp.minute = gameData.Minute;
    }

    public void SaveData(GameData gameData)
    {
        gameData.Year = this.timestamp.year;
        gameData.Season = this.timestamp.season;
        gameData.Day = this.timestamp.day;
        gameData.Hour = this.timestamp.hour;
        gameData.Minute = this.timestamp.minute;
    }
}
//Source: https://www.youtube.com/watch?v=jNbOXAGJbZ4&list=PLgXA5L5ma2Bu1sWc_-ZGRuPSUEAKPktHF&index=8