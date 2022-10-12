using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient Scriptable Object", menuName = "Scriptable Objects/Ingredient")]
public class IngredientScriptableObject : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientSprite;
    public float buyPrice;
    public float sellPrice;
    public int ingredientQuantity;
}
