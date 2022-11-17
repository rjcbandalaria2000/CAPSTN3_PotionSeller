using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicSceneManager : MonoBehaviour
{
   public void sceneLoad(string sceneName)
   {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
   }

    public void onCredits(GameObject credits)
    {
        credits.SetActive(true);
    }

    public void offCredits(GameObject credits)
    {
        credits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
