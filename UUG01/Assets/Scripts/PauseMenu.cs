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
        //When pause button is clicked, it will active home menu.
        PauseMenuu.SetActive(true);

        //Pausing the real time game.
        Time.timeScale = 0;
    }

    public void Cancel3()
    {
        PauseMenuu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Save()
    {
        //Implement later.
        Time.timeScale = 1;
    }

    public void Settings()
    {
        //Implement the made menu for the settings.
        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        //Have to implement a warning sign if game is not saved before going to main menu.

        //When the main menu button is clicked, it will load the main menu
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Exit()
    {
        //Have to implement a warning sign if game is not saved before exiting.

        Application.Quit();
    }
}

//Source: https://www.youtube.com/watch?v=MNUYe0PWNNs&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=26