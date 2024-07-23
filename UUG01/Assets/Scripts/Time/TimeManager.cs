using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField]
    GameTimestamp timestamp;

    [SerializeField] 
    private WorldTime.WorldTime worldTime;

    public float TimeScale = 1.0f;

    // List of objects to inform of changes to the time.
    List<ITimeTracker> listeners;

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

    void Start() // Start is called before the first frame update
    {
        if (worldTime != null)
        {
            worldTime.WorldTimeChanged += OnWorldTimeChanged;
            UpdateTimestamp(worldTime.CurrentTime);
        }
        else
        {
            Debug.LogError("WorldTime is not assigned.");
        }
        //StartCoroutine(TimeUpdate()); // Cannot use this because of WorldTime.cs
    }

    private void OnWorldTimeChanged(object sender, TimeSpan currentTime)
    {
        UpdateTimestamp(currentTime);
    }

    private void UpdateTimestamp(TimeSpan currentTime)
    {
        int hour = currentTime.Hours;
        int minute = currentTime.Minutes;

        // Initialize or update the timestamp
        if (timestamp == null)
        {
            timestamp = new GameTimestamp(1, GameTimestamp.Season.Spring, 1, hour, minute);
        }
        else
        {
            timestamp = new GameTimestamp(1, GameTimestamp.Season.Spring, 1, hour, minute);
        }

        Debug.Log($"Timestamp updated to: {timestamp.hour}:{timestamp.minute}");
    }

    /*IEnumerator TimeUpdate()  // Cannot use this because of WorldTime.cs
    {
        while (true)
        {
            yield return new WaitForSeconds(1/TimeScale); // Minute increases by every second (or as set in TimeScale).
            Tick();
        }
    }

    public void Tick() // A tick of the in-game time.
    {
        timestamp.UpdateClock();
    }*/  // Cannot use this because of WorldTime.cs
}
//Source: https://www.youtube.com/watch?v=jNbOXAGJbZ4&list=PLgXA5L5ma2Bu1sWc_-ZGRuPSUEAKPktHF&index=8