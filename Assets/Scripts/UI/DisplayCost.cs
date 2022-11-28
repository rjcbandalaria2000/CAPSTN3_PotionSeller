using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class DisplayCost : MonoBehaviour
{
    public TextMeshProUGUI quantityCount;
    public Shop shopIngredient;
    public int index;
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        if (shopIngredient == null)
        {
            shopIngredient = GameObject.FindObjectOfType<Shop>();
        }

        updateCount(index);
        
    }

    public void updateCount(int index)
    {
        ShopIngredient itemIngredient = shopIngredient.Items[index].GetComponent<ShopIngredient>();
        quantityCount.text = itemIngredient.ingredientScriptableObject.buyPrice.ToString();
    }
}
