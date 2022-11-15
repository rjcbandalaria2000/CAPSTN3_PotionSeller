using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class DisplayLevel : MonoBehaviour
{
    public StoreLevel storeLevel;
    public TextMeshProUGUI levelText;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        storeLevel = SingletonManager.Get<StoreLevel>();
        levelText = this.GetComponent<TextMeshProUGUI>();
        UpdateStoreLevel();
        storeLevel.onRefreshLevelUI.AddListener(UpdateStoreLevel);
    }

    public void UpdateStoreLevel()
    {
        Assert.IsNotNull(storeLevel, "Store level not set or is null");
        if(levelText == null) { return; }
        levelText.text = storeLevel.Level.ToString("0");
    }

    public void OnSceneChange()
    {
        storeLevel.onRefreshLevelUI.RemoveListener(UpdateStoreLevel);
    }

   
}
