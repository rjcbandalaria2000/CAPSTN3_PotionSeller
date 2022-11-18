using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject mainMenuPromt;

    public void gamePause()
    {
        Time.timeScale = 0;

        pauseScreen.SetActive(true);
    }

    public void playGame()
    {
        Time.timeScale = 1;

        pauseScreen.SetActive(false);
    }

    public void goToMainMenu(int sceneID)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        SceneManager.LoadSceneAsync(sceneID);
    }

    

}
