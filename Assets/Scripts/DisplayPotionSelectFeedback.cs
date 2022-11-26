using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class DisplayPotionSelectFeedback : MonoBehaviour
{
    public CraftingManager craftingManager;
    public TextMeshProUGUI feedbackText;

    private void Awake()
    {
        feedbackText = this.GetComponent<TextMeshProUGUI>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        craftingManager = SingletonManager.Get<CraftingManager>();
        if (craftingManager)
        {
            craftingManager.onCompleteIngredientPotion.AddListener(DisplayCompleteMessage);
            craftingManager.onIncompleteIngredientPotion.AddListener(DisplayIncompleteMessage);
        }
    }

    public void DisplayIncompleteMessage()
    {
        if (feedbackText == null) { return; }
        feedbackText.text = "Incomplete ingredients for the potion";
    }

    public void DisplayCompleteMessage()
    {
        if (feedbackText == null) { return; }
        feedbackText.text = "Chosen potion: " + craftingManager.selectedPotionScriptableObject.potionName;
    }

    public void RemoveListeners()
    {

    }
}
