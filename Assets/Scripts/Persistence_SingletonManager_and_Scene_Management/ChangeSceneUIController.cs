using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneUIController : MonoBehaviour
{
    public void OnChangeScene(string sceneId)
    {
        SingletonManager.Get<SceneLoader>().LoadScene(sceneId);
    }
}
