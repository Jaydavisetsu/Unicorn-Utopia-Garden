using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Connecting the function to the buttons as an event funtion.
    [SerializeField] private GameObject PauseMenuu;

    void Start()
    {
        if (PauseMenuu == null)
        {
            Debug.LogError("PauseMenuu is not assigned in the Inspector at Start.");
        }
        else
        {
            Debug.Log("PauseMenuu is correctly assigned in the Inspector.");
        }
    }

    public void Pause()
    {
        AudioManager.Instance.PlaySFX("Selecting");

        if (PauseMenuu == null)
        {
            Debug.LogError("PauseMenuu is not assigned in the Inspector during Pause.");
            return;
        }

        PauseMenuu.SetActive(true); //When pause button is clicked, it will activate pause menu.
        Time.timeScale = 0; //Pausing the real time game.
    }

    public void Cancel3()
    {
        AudioManager.Instance.PlaySFX("Selecting");

        PauseMenuu.SetActive(false);
        Time.timeScale = 1; // Resuming the real time game.
    }

    /* void Save()
    {
        AudioManager.Instance.PlaySFX("Selecting");

        DataPersistenceManager.Instance.SaveGame(); //From the DataPersistence Manager.
    }*/

    public void Settings()
    {
        AudioManager.Instance.PlaySFX("Selecting");

        //This is done using "on click" in unity.
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX("Selecting");

        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync("MainMenu"); //When the main menu button is clicked, it will load the main menu
    }

    public void Exit()
    {
        AudioManager.Instance.PlaySFX("Selecting");

        Application.Quit();
    }
}

//Source: https://www.youtube.com/watch?v=MNUYe0PWNNs&list=PLf6aEENFZ4Fv0ifncKE3T05qrI450U_aD&index=26
//Source: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 for saving and other methods.