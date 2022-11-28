using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class Shop : MonoBehaviour
{
    public List<GameObject> Items = new();
    public Wallet PlayerWallet;
    public float markupPercent = 1f;
    public List<DisplayIngredientQuantity> displayIngredientsQuantity = new();
    public List<DisplayCost> displayCosts = new();
    public List<DisplayItemSprite> itemSprites = new();
    public List<DisplayItemName> itemNames = new();

    private StatsManager statsManager;

    void Start()
    {
        if(PlayerWallet == null)
        {
            PlayerWallet = GameObject.FindObjectOfType<Wallet>().GetComponent<Wallet>();
        }
        else
        {
            Assert.IsNotNull(PlayerWallet, "Player wallet is not set");
        }
        statsManager = SingletonManager.Get<StatsManager>();
    }

    private void OnEnable()
    {
        
        foreach (GameObject shopItem in Items)
        {
            ShopIngredient itemIngredient = shopItem.GetComponent<ShopIngredient>();
     
            quantities.Add(itemIngredient.ingredientScriptableObject.ingredientQuantity);
            buyItemPrice.Add((int)itemIngredient.ingredientScriptableObject.buyPrice);
            sellItemPrice.Add((int)itemIngredient.ingredientScriptableObject.sellPrice);

        }
    }

    private void OnDisable()
    {
        buyItemPrice.Clear();
        sellItemPrice.Clear();
        quantities.Clear();
    }

    public void setIndex(int inspecIndex)
    {
        itemIndex = inspecIndex;
    }

    public void BuyItem(string itemName)
    {
        bool isItemFound = false;
        foreach (GameObject shopItem in Items)
        {
            ShopIngredient itemIngredient = shopItem.GetComponent<ShopIngredient>();
            if (itemIngredient.ingredientScriptableObject.ingredientName == itemName)
            {
                isItemFound = true;
                if (PlayerWallet.Money >= buyItemPrice[itemIndex])
                {
                    PlayerWallet.SpendMoney(buyItemPrice[itemIndex]);
                  
                    Debug.Log("Bought " + itemIngredient.ingredientScriptableObject.ingredientName);
                    //Inventory.instance.AddItem(itemIngredient.ingredientScriptableObject.ingredientName);
                    SingletonManager.Get<Inventory>().AddItem(itemIngredient.ingredientScriptableObject);
                    if (statsManager)
                    {
                        statsManager.AddTotalGoldSpent((int)itemIngredient.ingredientScriptableObject.buyPrice);
                    }
                }
                else
                {
                    Debug.Log("Not enough funds...");
                }
                break;
            }
        }
        if (!isItemFound)
        {
            Debug.Log("No item found");            
        }
    }

    public void SellItem(string itemName)
    {
        bool isItemFound = false;
        foreach (GameObject shopItem in Items)
        {
            ShopPotion itemPotion = shopItem.GetComponent<ShopPotion>();
            if (itemPotion.potionScriptableObject.potionName == itemName)
            {
                isItemFound = true;
                if (PlayerWallet.Money >= itemPotion.potionScriptableObject.buyPrice)
                {
                    float getPrice = itemPotion.potionScriptableObject.buyPrice * markupPercent;
                    Debug.Log("Bought " + itemPotion.potionScriptableObject.potionName);
                    PlayerWallet.AddMoney(Mathf.RoundToInt(getPrice));
                    if (statsManager)
                    {
                        statsManager.AddTotalGoldEarned(Mathf.RoundToInt(getPrice));
                    }
                    //Inventory.instance.RemoveItem(itemPotion.potionScriptableObject.potionName);
                }
                else
                {
                    Debug.Log("Not enough funds...");
                }
                break;
            }
        }
        if (!isItemFound)
        {
            Debug.Log("No item found");
        }
    }

    public void SetMarkupPercent(int value)
    {
        switch (value)
        {
            case 0:
                markupPercent = 1f;
                break;
            case 1:
                markupPercent = 1.25f;
                break;
            case 2:
                markupPercent = 1.50f;
                break;
            case 3:
                markupPercent = 1.75f;
                break;
            case 4:
                markupPercent = 2f;
                break;
        }
    }
    
    public void addQuantity(int index)
    {
        ShopIngredient itemIngredient = Items[index].GetComponent<ShopIngredient>();
        if (itemIngredient.ingredientScriptableObject.ingredientQuantity < 99)
        {
            if(itemIngredient.ingredientScriptableObject.ingredientQuantity > 99)
            {
                return;
            }
            else
            {
                quantities[index]++;
                buyItemPrice[index] += 2;
                sellItemPrice[index] += 1;
               
                displayIngredientsQuantity[index].updateCount(index);
                displayCosts[index].updateCount(index);
            }
        }
    }

    public void decreaseQuantitiy(int index)
    {
        ShopIngredient itemIngredient = Items[index].GetComponent<ShopIngredient>();
        if (itemIngredient.ingredientScriptableObject.ingredientQuantity > 0)
        {
            if(itemIngredient.ingredientScriptableObject.ingredientQuantity <= 0)
            {
                return;
            }
            else
            {
                quantities[index]--;
                buyItemPrice[index] -= 2;
                sellItemPrice[index] -= 1;

                displayIngredientsQuantity[index].updateCount(index);
                displayCosts[index].updateCount(index);
            }            
        }
    }
}
