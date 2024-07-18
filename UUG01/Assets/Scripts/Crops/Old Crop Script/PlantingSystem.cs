using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingSystem : MonoBehaviour
{
    public GameObject cropPrefab;
    public LayerMask soilLayer; // Layer for soil to plant crops on

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlantCrop();
        }
    }

    void PlantCrop()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, soilLayer))
        {
            Instantiate(cropPrefab, hit.point, Quaternion.identity);
        }
    }
}
