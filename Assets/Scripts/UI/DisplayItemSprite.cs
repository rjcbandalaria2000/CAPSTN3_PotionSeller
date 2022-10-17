using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItemSprite : MonoBehaviour
{
    public Shop ingredientComp;

    public int index;
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ingredientComp == null)
        {
            ingredientComp = GameObject.FindObjectOfType<Shop>();
        }

        updateSprite(index);
    }
    public void updateSprite(int index)
    {
        ShopIngredient itemIngredient = ingredientComp.Items[index].GetComponent<ShopIngredient>();
        this.GetComponent<Image>().sprite = itemIngredient.ingredientScriptableObject.ingredientSprite;
    }
}
