using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Potion Scriptable Object", menuName = "Scriptable Objects/Potion")]
public class PotionScriptableObject : ScriptableObject
{
    public string potionName;
    public Sprite potionSprite;
    public Sprite potionIconSprite;
    public float buyPrice;
    public float sellPrice;

    public List<string> description;

    public List<RequiredIngredients> requiredIngredients= new List<RequiredIngredients>();
}
