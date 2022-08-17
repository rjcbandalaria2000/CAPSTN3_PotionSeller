using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneController : MonoBehaviour
{
    public void OnChangeScene(string sceneID)
    {
        SingletonManager.Get<SceneLoad>().LoadScene(sceneID);
    }
}
