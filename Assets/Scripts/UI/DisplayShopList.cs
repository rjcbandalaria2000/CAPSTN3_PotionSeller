using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for(int i = 0; i < Shop.Items.Count; i++) {
            
            GameObject itemListPrefab = Instantiate(ItemShopPrefab);
            itemListPrefab.transform.SetParent(ShopListUI.transform);

        
        }
    }

}
