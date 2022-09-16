using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Shop : MonoBehaviour
{
    public List<GameObject> Items = new();
    public Wallet PlayerWallet;
    // Start is called before the first frame update
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
                    Inventory.instance.AddItem(itemIngredient.ingredientScriptableObject.ingredientName);
                   //SingletonManager.Get<Inventory>().AddItem(itemIngredient.ingredientScriptableObject.ingredientName);
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
}
