using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Leading level one.
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //
    public void QuitGame()
    {
        Application.Quit();
    }
}

//Source: https://www.youtube.com/watch?v=DX7HyN7oJjE
