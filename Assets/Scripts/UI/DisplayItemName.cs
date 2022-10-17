using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItemName : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public Shop ingredientComp;
    public int index = 0;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        if (ingredientComp == null)
        {
            ingredientComp = GameObject.FindObjectOfType<Shop>();
        }

        updateCount(index);
    }

    public void updateCount(int index)
    {
        ShopIngredient itemIngredient = ingredientComp.Items[index].GetComponent<ShopIngredient>();
        itemName.text = itemIngredient.ingredientScriptableObject.ingredientName.ToString();
    }
}
