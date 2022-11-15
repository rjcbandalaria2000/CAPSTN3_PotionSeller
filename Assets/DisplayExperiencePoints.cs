using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class DisplayExperiencePoints : MonoBehaviour
{
    public StoreLevel storeLevel;
    public TextMeshProUGUI currentExperienceText;
    public TextMeshProUGUI requiredExperienceText;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        storeLevel = SingletonManager.Get<StoreLevel>();
        UpdateExperiencePoints();
        storeLevel.onRefreshLevelUI.AddListener(UpdateExperiencePoints);
    }

    public void UpdateExperiencePoints()
    {
        Assert.IsNotNull(storeLevel, "Store level not set or is null");
        if (currentExperienceText == null) { return; }
        if(requiredExperienceText == null) { return; }
        currentExperienceText.text = storeLevel.CurrentExperiencePoints.ToString();
        requiredExperienceText.text = storeLevel.MaxExperiencePoints[storeLevel.Level].ToString();
    }

    public void OnSceneChange()
    {
        storeLevel.onRefreshLevelUI.RemoveListener(UpdateExperiencePoints);
    }


}
