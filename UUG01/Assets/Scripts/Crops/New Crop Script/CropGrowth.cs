using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    public Sprite[] growthStages; // Array of sprites representing growth stages
    public float growthDuration = 10.0f; // Total time for crop to fully grow in seconds
    private float growthTime = 0.0f; // Time elapsed since the crop started growing
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (growthStages.Length > 0)
        {
            spriteRenderer.sprite = growthStages[0]; // Set initial sprite
        }
    }

    void Update()
    {
        if (growthTime < growthDuration && growthStages.Length > 0)
        {
            growthTime += Time.deltaTime;
            int stageIndex = Mathf.FloorToInt((growthTime / growthDuration) * growthStages.Length);
            stageIndex = Mathf.Clamp(stageIndex, 0, growthStages.Length - 1); // Ensure index is within bounds
            spriteRenderer.sprite = growthStages[stageIndex];
        }
    }



}
