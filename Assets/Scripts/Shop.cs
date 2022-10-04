using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Shop : MonoBehaviour
{
    public List<GameObject> Items = new();
    public Wallet PlayerWallet;
    public float markupPercent = 1f;
    
    void Start()
    {
        Assert.IsNotNull(PlayerWallet, "Player wallet is not set");
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
                if (PlayerWallet.Money >= itemIngredient.ingredientScriptableObject.buyPrice)
                {
                    PlayerWallet.SpendMoney((int)itemIngredient.ingredientScriptableObject.buyPrice);
                    Debug.Log("Bought " + itemIngredient.ingredientScriptableObject.ingredientName);
                    //Inventory.instance.AddItem(itemIngredient.ingredientScriptableObject.ingredientName);
                    SingletonManager.Get<Inventory>().AddItem(itemIngredient.ingredientScriptableObject.ingredientName);
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
                    PlayerWallet.AddMoney(Mathf.RoundToInt(getPrice));
                    Debug.Log("Bought " + itemPotion.potionScriptableObject.potionName);
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
}
