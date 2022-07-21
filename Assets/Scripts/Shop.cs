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
        foreach(GameObject shopItem in Items)
        {
            ShopIngredient itemIngredient = shopItem.GetComponent<ShopIngredient>();
            if (itemIngredient)
            {
                if(itemIngredient.Name == itemName)
                {
                    if(PlayerWallet.Money >= itemIngredient.Cost)
                    {
                        PlayerWallet.SpendMoney(itemIngredient.Cost);
                        Debug.Log("Bought " + itemIngredient.Name);
                    }
                }
                else
                {
                    Debug.Log("Item no found");
                }
            }
        }
    }
}
