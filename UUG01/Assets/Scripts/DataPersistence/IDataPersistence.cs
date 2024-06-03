using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is what scripts will need to implement.
/// </summary>
public interface IDataPersistence
{
    void LoadData(GameData gameData); //When data is loaded, the implementing script only cares about reading that data.
    void SaveData(GameData gameData); //When data is saved, we want the implement script to modify the data.
}

//Source: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 not using it completely because it uses json utility and not json net.
