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

    public void updateCount(int index)
    {
        ShopIngredient itemIngredient = ingredientComp.Items[index].GetComponent<ShopIngredient>();
        quantityCount.text = itemIngredient.ingredientScriptableObject.ingredientQuantity.ToString();
    }
}
