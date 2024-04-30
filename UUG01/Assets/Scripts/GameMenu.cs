using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    //Array to hold all game buttons.
    public Button[] Buttons;

    private void Awake()
    {
        int SavedGame = PlayerPrefs.GetInt("UnlockedGame", 1); //WILL COME BACK AND CHANGE THIS. DO NOT WANT TO USE PLAYERPREFS.

        //Disabling buttons that do not have saves.
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }

        //Reenabling buttons when game is saved.
        for (int i = 0; i < SavedGame; i++)
        {
            Buttons[i].interactable = true;
        }
    }

    //Loads the level (for now until there will be a game manager to save the different games) when the button is clicked.
    public void OpenGame(int gameId)
    {
        string GameName = "Level" + gameId; //Calling it level for now. Until there are more levels added, then I will create a level manager for it.
        SceneManager.LoadScene(GameName);
    }
}

//Source: https://www.youtube.com/watch?v=2XQsKNHk1vk&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=24&t=272s
