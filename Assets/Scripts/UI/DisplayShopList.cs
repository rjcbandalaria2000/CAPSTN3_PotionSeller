using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShopList : MonoBehaviour
{
    public Shop Shop;
    public GameObject ItemShopPrefab;
    public GameObject ShopListUI;

    [Header("Item List Elements")]
    public GameObject buyPriceUI;


    [Header("Add Button")]
    public List<Button> addButtons;
    [Header("Minus Button")]
    public List<Button> decreaseButtons;

  

    // Start is called before the first frame update
    void Start()
    {
       // DisplayList();
    }

    public void DisplayList()
    {
        for (int i = 0; i < Shop.Items.Count; i++)
        {
            GameObject itemListPrefab = Instantiate(ItemShopPrefab);            
            itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Shop.Items[i].GetComponent<ShopIngredient>().ingredientScriptableObject.ingredientName;
            itemListPrefab.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = Shop.Items[i].GetComponent<ShopIngredient>().ingredientScriptableObject.buyPrice.ToString();
            Debug.Log(Shop.Items[i].name);
            itemListPrefab.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Shop.BuyItem(Shop.Items[i].GetComponent<ShopIngredient>().ingredientScriptableObject.ingredientName));//itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
            itemListPrefab.transform.SetParent(ShopListUI.transform, false);

            Shop.displayIngredientsQuantity.Add(itemListPrefab.GetComponentInChildren<DisplayIngredientQuantity>());
            Shop.displayCosts.Add(itemListPrefab.GetComponentInChildren<DisplayCost>());
            Shop.itemSprites.Add(itemListPrefab.GetComponentInChildren<DisplayItemSprite>());
            Shop.itemNames.Add(itemListPrefab.GetComponentInChildren<DisplayItemName>());
            addButtons.Add(itemListPrefab.transform.GetChild(1).GetChild(1).GetComponent<Button>());
            decreaseButtons.Add(itemListPrefab.transform.GetChild(1).GetChild(2).GetComponent<Button>());

            setIndex(i);

            int j = i;
            
            addButtons[j].onClick.AddListener(() => Shop.addQuantity(j));


            decreaseButtons[j].onClick.AddListener(() => Shop.decreaseQuantitiy(j));
        }

        //foreach (GameObject shopItem in Shop.Items)
        //{
        //    index = 0;

        //    GameObject itemListPrefab = Instantiate(ItemShopPrefab);            
        //    itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItem.GetComponent<ShopIngredient>().ingredientScriptableObject.ingredientName;
        //    itemListPrefab.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItem.GetComponent<ShopIngredient>().ingredientScriptableObject.buyPrice.ToString();            
        //    itemListPrefab.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Shop.BuyItem(shopItem.GetComponent<ShopIngredient>().ingredientScriptableObject.ingredientName));//itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
        //    itemListPrefab.transform.SetParent(ShopListUI.transform, false);

        //    Shop.displayIngredientsQuantity.Add(itemListPrefab.GetComponentInChildren<DisplayIngredientQuantity>());
        //    Shop.displayCosts.Add(itemListPrefab.GetComponentInChildren<DisplayCost>());
        //    Shop.itemSprites.Add(itemListPrefab.GetComponentInChildren<DisplayItemSprite>());
        //    Shop.itemNames.Add(itemListPrefab.GetComponentInChildren<DisplayItemName>());
        //    addButtons.Add(GameObject.FindGameObjectWithTag("ADD").GetComponent<Button>());
        //    decreaseButtons.Add(GameObject.FindGameObjectWithTag("MINUS").GetComponent<Button>());

        //    setIndex(index);

        //    Button add = addButtons[index].GetComponent<Button>();
        //    add.onClick.AddListener(() => Shop.addQuantity(index));

        //    Button minus = decreaseButtons[index].GetComponent<Button>();
        //    minus.onClick.AddListener(() => Shop.decreaseQuantitiy(index));
            
           
        //}
    }

    public void setIndex(int index)
    {
        Shop.displayCosts[index].index = index;
        Shop.itemSprites[index].index = index;  
        Shop.itemNames[index].index = index;    

    }

    private int GetLastChildIndex(GameObject parentObject)
    {
        int num = parentObject.transform.childCount - 1;
        return num;
    }
}
