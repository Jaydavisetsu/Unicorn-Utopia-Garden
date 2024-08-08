using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap interactableMap;
    public Tile hiddenInteractableTile;
    public Tile plowedTile;

    private bool hasCalledOnce;

    void Start()
    {
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
        CallMethodOnce();
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, plowedTile);
    }

    public string GetTileName(Vector3Int position)
    {
        if (interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null)
            {
                return tile.name;
            }
        }

        return "";
    }

    private void Update()
    {
        if (hasCalledOnce == true)
        {
            CallMethodOnce();
        }
    }

    void CallMethodOnce()
    {
        ExampleCoroutine();
        //Debug.Log("doing the recievexoncemethod");

        if (interactableMap.ContainsTile(plowedTile))
        {
            XPAddedGameEvent info = new XPAddedGameEvent(5);
            EventManager.Instance.QueueEvent(info);
            CurrencySystem.Instance.ActivateForDuration(2f, 5);
            Debug.Log("xp added");
            hasCalledOnce = false;
        }
        else if (!interactableMap.ContainsTile(plowedTile))
        {
            hasCalledOnce = true;
        }
    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(10);
    }
}

// Source: https://www.youtube.com/watch?v=1NTNIm0tcXw&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=12
