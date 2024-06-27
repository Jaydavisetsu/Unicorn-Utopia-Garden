using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public GameObject[] growthStages; // Array of growth stages prefabs
    private int currentStage = 0;
    private float growthTimer = 0f;
    public float timeToGrow = 5f; // Time in seconds for each stage

    void Start()
    {
        UpdateGrowthStage();
    }

    void Update()
    {
        growthTimer += Time.deltaTime;
        if (growthTimer >= timeToGrow)
        {
            Grow();
        }
    }

    void Grow()
    {
        if (currentStage < growthStages.Length - 1)
        {
            currentStage++;
            growthTimer = 0f;
            UpdateGrowthStage();
        }
    }

    void UpdateGrowthStage()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject stage = Instantiate(growthStages[currentStage], transform);
        stage.transform.localPosition = Vector3.zero;
    }

    public bool IsMature()
    {
        return currentStage == growthStages.Length - 1;
    }

}
