using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStoreLevel : MonoBehaviour
{
    public GameObject storeLevelGO;
    public Image fillImage;

    private StoreLevel storeLevel;

    private void Awake()
    {
        if(storeLevelGO != null)
        {
            storeLevel = storeLevelGO.GetComponent<StoreLevel>();
            storeLevel.onRefreshLevelUI.AddListener(UpdateStoreLevelFill);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateStoreLevelFill()
    {
        if(storeLevel == null) { return; }
        if(fillImage == null) { return; }
        fillImage.fillAmount = storeLevel.GetNormalizedExpPoints();
    }
}
