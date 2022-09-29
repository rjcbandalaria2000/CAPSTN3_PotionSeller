using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public PotionScriptableObject selectedPotionScriptableObject;
    public TextMeshProUGUI selectedPotionText;
    public List<PotionScriptableObject> potionList = new();
    public bool isMixingComplete;
    public bool isCookingComplete;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    public void OnSelectedPotionClick(PotionScriptableObject potionScriptableObject)
    {
        Debug.Log(potionScriptableObject + " selected.");
        selectedPotionScriptableObject = potionScriptableObject;
        selectedPotionText.text = selectedPotionScriptableObject.potionName;
    }

    public void OnRemoveSelectedPotionClick()
    {
        if (selectedPotionScriptableObject != null)
        {
            selectedPotionScriptableObject = null;
            selectedPotionText.text = null;
        }
    }

    public void OnCompleteCrafting()
    {
        if(isMixingComplete && isCookingComplete)
        {
            Inventory.instance.AddItem(selectedPotionScriptableObject.potionName);
        }
    }


}
