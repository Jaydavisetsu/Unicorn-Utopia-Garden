using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Connecting the funcition to the buttons as an event funtion.
    [SerializeField] GameObject PauseMenuu;

    public void Pause()
    {
        PauseMenuu.SetActive(true); //When pause button is clicked, it will active pause menu.
        Time.timeScale = 0; //Pausing the real time game.
    }

    public void Cancel3()
    {
        PauseMenuu.SetActive(false);
        Time.timeScale = 1; // Resuming the real time game.
    }

    public void Save()
    {
        DataPersistenceManager.Instance.SaveGame(); //From the DataPersistence Manager.
    }

    public void Settings()
    {
        //This is done using "on click" in unity.
    }

    public void MainMenu()
    {
        //TODO: Have to implement a warning sign if game is not saved before going to main menu.

        SceneManager.LoadScene("MainMenu"); //When the main menu button is clicked, it will load the main menu
    }

    public void Exit()
    {
        //TODO: Have to implement a warning sign if game is not saved before exiting.
        Application.Quit();
    }
}

//Source: https://www.youtube.com/watch?v=MNUYe0PWNNs&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=26
//Source: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 for saving and other methods.