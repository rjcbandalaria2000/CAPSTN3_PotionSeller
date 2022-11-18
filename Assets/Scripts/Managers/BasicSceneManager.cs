using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicSceneManager : MonoBehaviour
{
    public GameObject loadingsScreen;
    public Image loadingBar;

    public void loadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingsScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.fillAmount = progress;

            yield return null;
        }
    }


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
