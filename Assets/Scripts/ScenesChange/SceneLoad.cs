using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Linq;
using System;


public class SceneLoad : MonoBehaviour
{
    [Header("Config")]
    public string FirstSceneId;

    private string currentSceneId;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    private void Start()
    {
        LoadScene(FirstSceneId);

        Data_Player playerData = SingletonManager.Get<Data_Player>();
    }

    private IEnumerator LoadSequence(string sceneId)
    {
        if (!string.IsNullOrEmpty(currentSceneId)) //currentsceneID != string.empty
        {
            Complex_SceneManager addSceneLoader = SingletonManager.Get<Complex_SceneManager>();

            if (addSceneLoader) //remove extra scenes to the current scene 
            {
                yield return addSceneLoader.UnloadScene();
                Debug.Log("Complex_SceneManager Unloaded");
            }


            yield return SceneManager.UnloadSceneAsync(currentSceneId);
            currentSceneId = string.Empty;
        }

        Resources.UnloadUnusedAssets();
        yield return null;
        GC.Collect(); // Trigger a collection to free memory
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);

        yield return operation;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneId));
        currentSceneId = sceneId;

        yield return null;

    }

    public Coroutine LoadScene(string sceneId)
    {
        return StartCoroutine(LoadSequence(sceneId));
    }


}
