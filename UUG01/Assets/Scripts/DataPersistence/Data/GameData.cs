using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that stores the state of game.
/// Has a constructor to get a new one for a new game (multiple saves).
/// </summary>

[System.Serializable]
public class GameData 
{
    //DEMO HERE...death count for character is still tenative (complete death will only respawn character at their home will loss of inventory items).
    //public int deathCount;

    public Vector2 PlayerPosition;

    public GameData() //The values defined in their constructor will be the default value, so the game starts with when there is no data to load.
    {
        //this.deathCount = 0;
        PlayerPosition = Vector2.zero;
    }
}

//Source: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 not using it completely because it uses json utility and not json net.