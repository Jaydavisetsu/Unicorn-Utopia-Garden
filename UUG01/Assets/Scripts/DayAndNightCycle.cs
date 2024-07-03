using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayAndNightCycle : MonoBehaviour
{
    [System.Serializable]
    public struct DayAndNightMark //A data structure.
    {
        public float timeRatio; // In the 0-1 range.
        public Color color; // The color of sprites.
        public float intensity; // Color intensity of the sprites.
    }

    [SerializeField] private DayAndNightMark[] Marks; // Helps assign the variable of timeRation, color, and intensity in the inspector.
    [SerializeField] private float CycleLength = 24; // Represents the total duration of the cycle from morning to morning in seconds. This number can decrese if we want cycle to go quicker.
    [SerializeField] private Light2D Light; 

    private const float TIME_CHECK_EPSILON = 0.1f; // Might need to play around with this value if marks are closer.


    private float currentCyleTime; // Holds the current time that has elapsed in the cycle.
    private int currentMarkIndex, nextMarkIndex; // Helps update the marks regularly when we pass a mark in the array.
    private float currentMarktime, nextMarkTime;// Extra variables for the exact time in seconds for the current and next marks. They will be used to compare with the currentCycleTime variable to know if mark has been passed or not. 
    
    // Start is called before the first frame update
    void Start()
    {
        currentMarkIndex = -1; // Initializes 
        CycleMarks();
    }

    // Update is called once per frame
    void Update()
    {
        currentCyleTime = (currentCyleTime + Time.deltaTime) % CycleLength; // Incrementing. It counts the total time that has elasped since the game has started (last frame).

        if (Mathf.Abs(currentCyleTime - nextMarkTime) < TIME_CHECK_EPSILON) // Checks if mark has been passed. Using Mathf.Abs because the variables we are looking at are floats and have many decimals inbetween. Unity will not be able to give an exact answer and will cause the mark to overshoot without knowing. 
        {

        }
    }

    private void CycleMarks() 
    {
        currentMarkIndex = (currentMarkIndex + 1) % Marks.Length; // Increments the currentMarkIndex with a modulo to ensure it does not overshoot the length of the marks array. If the index reaches the end of the array, this will wrap it back to zero.
        nextMarkIndex = (currentMarkIndex + 1) % Marks.Length;
        currentMarktime = Marks[currentMarkIndex].timeRatio * CycleLength; // Using two indexes to get back the actual day and night mark objects from the array and store the actual time in the cycle in seconds.
        nextMarkTime = Marks[nextMarkIndex].timeRatio * CycleLength;
    }
}

//Source: https://www.youtube.com/watch?v=O997NxQGQ6A