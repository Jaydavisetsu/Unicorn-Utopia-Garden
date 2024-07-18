using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSystem : MonoBehaviour
{
    public LayerMask cropLayer; // Layer for crops to be harvested

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click to harvest
        {
            HarvestCrop();
        }
    }

    void HarvestCrop()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cropLayer))
        {
            Crop crop = hit.collider.GetComponent<Crop>();
            if (crop != null && crop.IsMature())
            {
                // Handle harvest logic (e.g., add crop to inventory)
                Destroy(hit.collider.gameObject);
            }
        }
    }
}

