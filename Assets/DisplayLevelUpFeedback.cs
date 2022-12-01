using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevelUpFeedback : MonoBehaviour
{
    public StoreLevel storeLevel;
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        storeLevel = SingletonManager.Get<StoreLevel>();
        uiManager = SingletonManager.Get<UIManager>();
        if (storeLevel)
        {
            storeLevel.onLevelUp.AddListener(ActivateLevelUpFeedback);
        }
        if (uiManager)
        {
            uiManager.basicSceneManager.onSceneChange.AddListener(OnSceneChange);
        }
        this.gameObject.SetActive(false);
    }

    public void ActivateLevelUpFeedback()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeactivateLevelUpFeedback()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnSceneChange()
    {
        storeLevel.onLevelUp.RemoveListener(ActivateLevelUpFeedback);
        uiManager.basicSceneManager.onSceneChange.RemoveListener(OnSceneChange);
    }

}
