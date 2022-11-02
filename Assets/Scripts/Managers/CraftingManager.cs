using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public QuestCompletedEvent onQuestCompletedEvent = new QuestCompletedEvent();

    public PotionScriptableObject       selectedPotionScriptableObject;

    [Header("UI")]
    public TextMeshProUGUI              selectedPotionText;
    public Image                        selectedPotionIconImage;
    public Sprite                        defaultPotionIconImage;
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

        List<ItemData> tempPlayerIngredients = new(); // temporarily store the required ingredients for the potion

        //Check if the player has enough ingredients in the inventory
        for(int i = 0; i < potionScriptableObject.requiredIngredients.Count; i++) // checks how many required ingredients are there
        {
            if (playerInventory == null) { return; }
            ////check if the player has the right ingredient
            if(playerInventory.ingredients.Count < 0) { return; }
            for (int j = 0; j < playerInventory.ingredients.Count; j++) // checks the player inventory
            {
                //if the player has the same ingredient as the required ingredient 
                if (playerInventory.ingredients[j].itemName == potionScriptableObject.requiredIngredients[i].ingredient.ingredientName)
                {
                    // if the player has the right quantity for the potion
                    if (playerInventory.ingredients[j].itemAmount >= potionScriptableObject.requiredIngredients[i].quantity)
                    {
                        Debug.Log("Has enough ingredients");
                        tempPlayerIngredients.Add(playerInventory.ingredients[j]); // store the ingredients in a temporary storage to be used
                        Debug.Log("Ingredient: " + playerInventory.ingredients[j].itemName);
                        break;
                    }
                    else
                    {
                        Debug.Log("Not enough ingredients" +
                            potionScriptableObject.requiredIngredients[i].ingredient.ingredientName + " "
                            + potionScriptableObject.requiredIngredients[i].quantity);
                    }
                }
            }
            
        }
        //if the player has the right ingredients and the right quantity
        if (tempPlayerIngredients.Count >= potionScriptableObject.requiredIngredients.Count)
        {
            for (int i = 0; i < potionScriptableObject.requiredIngredients.Count; i++)
            {
                for(int j = 0; j < tempPlayerIngredients.Count; j++)
                {
                    if (tempPlayerIngredients[j].itemName == potionScriptableObject.requiredIngredients[i].ingredient.ingredientName)
                    {
                        tempPlayerIngredients[j].itemAmount -= potionScriptableObject.requiredIngredients[i].quantity;
                        break;
                    }
                }
            
            }
            
            selectedPotionScriptableObject = potionScriptableObject;
            if (selectedPotionText)
            {
                selectedPotionText.text = selectedPotionScriptableObject.potionName.ToString();
            }
            if (selectedPotionIconImage)
            {
                selectedPotionIconImage.sprite = selectedPotionScriptableObject.potionIconSprite;
            }
            Debug.Log("Setting potion");
        }
        
       
        //selectedPotionText.text = selectedPotionScriptableObject.potionName;
    }

    public void OnRemoveSelectedPotionClick()
    {
        if (selectedPotionScriptableObject != null)
        {
            selectedPotionScriptableObject = null;
            selectedPotionText.text = "";
        }
    }

    public void OnCompleteCrafting()
    {
        if(isMixingComplete && isCookingComplete)
        {
            //Inventory.instance.AddItem(selectedPotionScriptableObject.potionName);
            if (selectedPotionScriptableObject)
            {
                SingletonManager.Get<Inventory>().AddItem(selectedPotionScriptableObject);
                onQuestCompletedEvent?.Invoke(QuestManager.instance.createPotionQuest);
            }
           
            isCookingComplete = false;
            isMixingComplete = false;
            selectedPotionScriptableObject = null;
            selectedPotionText.text = "";
            selectedPotionIconImage.sprite = defaultPotionIconImage;
        }
    }
}
