using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShopList : MonoBehaviour
{
    public Shop Shop;
    public GameObject ItemShopPrefab;
    public GameObject ShopListUI;

    // Start is called before the first frame update
    void Start()
    {
        DisplayList();
    }

    public void DisplayList()
    {
        foreach(GameObject shopItem in Shop.Items)
        {
            GameObject itemListPrefab = Instantiate(ItemShopPrefab);            
            itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItem.GetComponent<ShopIngredient>().ingredientScriptableObject.ingredientName;
            itemListPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = shopItem.GetComponent<ShopIngredient>().ingredientScriptableObject.buyPrice.ToString();            
            itemListPrefab.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Shop.BuyItem(itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
            itemListPrefab.transform.SetParent(ShopListUI.transform, false);
        }
    }

}
