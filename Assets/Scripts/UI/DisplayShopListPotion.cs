using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShopListPotion : MonoBehaviour
{
    public Shop Shop;
    public GameObject ItemShopPrefab;
    public List<GameObject> DisplayItems;
    public GameObject ShopListUI;

    // Start is called before the first frame update
    void Start()
    {
        DisplayList();
    }

    public void DisplayList()
    {
        foreach (GameObject shopItem in Shop.Items)
        {
            GameObject itemListPrefab = Instantiate(ItemShopPrefab);
            DisplayItems.Add(itemListPrefab);
            itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItem.GetComponent<ShopPotion>()?.potionScriptableObject.potionName;
            itemListPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = shopItem.GetComponent<ShopPotion>()?.potionScriptableObject.buyPrice.ToString();
            itemListPrefab.transform.GetChild(GetLastChildIndex(itemListPrefab)).GetComponent<Button>().onClick.AddListener(() => Shop.SellItem(itemListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text));
            itemListPrefab.transform.SetParent(ShopListUI.transform, false);
        }
    }

    public void RefreshList(int value)
    {
        float markupPercent = 0;
        switch(value)
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
        for (int i = 0; i < DisplayItems.Count; i++)
        {
            float markupVal = Shop.Items[i].GetComponent<ShopPotion>().potionScriptableObject.buyPrice * markupPercent;
            DisplayItems[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(markupVal).ToString();
        }
    }

    private int GetLastChildIndex(GameObject parentObject)
    {
        int num = parentObject.transform.childCount - 1;
        return num;
    }
}
