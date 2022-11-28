using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayIngredientQuantity : MonoBehaviour
{
    public TextMeshProUGUI quantityCount;
    public Shop ingredientComp;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        if(ingredientComp == null)
        {
            ingredientComp = GameObject.FindObjectOfType<Shop>();
        }
    }

    public void updateCount(int index)
    {
        //ShopIngredient itemIngredient = ingredientComp.Items[index].GetComponent<ShopIngredient>();
        //quantityCount.text = "x" + ingredientComp.quantities[index].ToString();
    }
}
