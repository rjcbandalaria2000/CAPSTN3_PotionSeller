using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private ChangeSceneController sceneChange;

    private void Start()
    {
        sceneChange = this.gameObject.GetComponent<ChangeSceneController>();
    }
    public void OnPlayButtonClicked(string scene)
    {
       //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (sceneChange == null) { return; }
        if (scene == null) { return; }

        sceneChange.OnChangeScene(scene);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
