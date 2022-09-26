using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public PotionScriptableObject selectedPotionScriptableObject;
    public TextMeshProUGUI selectedPotionText; 

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
}
