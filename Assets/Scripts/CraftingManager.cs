using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public PotionScriptableObject       selectedPotionScriptableObject;
    public TextMeshProUGUI              selectedPotionText;
    public List<PotionScriptableObject> potionList = new();

    [Header("Crafting States")]
    public bool                         isMixingComplete;
    public bool                         isCookingComplete;

    private Inventory                   playerInventory;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        playerInventory = SingletonManager.Get<Inventory>();
    }

    public void OnSelectedPotionClick(PotionScriptableObject potionScriptableObject)
    {
        Debug.Log(potionScriptableObject + " selected.");
        //Check if the player has enough ingredients in the inventory

        for(int i = 0; i < potionScriptableObject.requiredIngredients.Count; i++) // checks how many required ingredients are there
        {
            if (playerInventory == null) { return; }
            //check if the player has the right ingredient
            if(playerInventory.ingredients.Count < 0) { return; }
            for(int j = 0; j < playerInventory.ingredients.Count; j++)
            {
                //if the player has the same ingredient as the required ingredient 
                if (playerInventory.ingredients[j].itemName == potionScriptableObject.requiredIngredients[i].ingredient.ingredientName)
                {
                    // if the player has the right quantity for the potion
                    if (playerInventory.ingredients[j].itemAmount >= potionScriptableObject.requiredIngredients[i].quantity)
                    {
                        Debug.Log("Has enough ingredients"); 
                        selectedPotionScriptableObject = potionScriptableObject;
                        break;
                    }
                    else
                    {
                        Debug.Log("Not enough ingredients" +
                            potionScriptableObject.requiredIngredients[i].ingredient.ingredientName + " "
                            + potionScriptableObject.requiredIngredients[i].quantity);
                        selectedPotionScriptableObject = null;
                        break;
                    }
                }
                else
                {
                    selectedPotionScriptableObject = null;
                    Debug.Log("No ingredients for potion" +
                            potionScriptableObject.requiredIngredients[i].ingredient.ingredientName);
                   
                }
            }

        }

       
        //selectedPotionText.text = selectedPotionScriptableObject.potionName;
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
            //Inventory.instance.AddItem(selectedPotionScriptableObject.potionName);
            SingletonManager.Get<Inventory>().AddItem(selectedPotionScriptableObject.potionName);
        }
    }


}
