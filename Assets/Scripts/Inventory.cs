using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    //private static Inventory _instance;
    //public static Inventory instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = GameObject.FindObjectOfType<Inventory>();
    //        }
    //        return _instance;
    //    }
    //}    

    public AddItemEvent onAddItemEvent = new AddItemEvent();
    public RemoveItemEvent onRemoveItemEvent = new RemoveItemEvent();

    public List<ItemData> potions = new();
    public List<ItemData> ingredients = new();

    private void Awake()
    {
        //_instance = this;
        SingletonManager.Register(this);
       
    }

    public void Start()
    {
        for (int i = 0; i < potions.Count; i++)
        {
            potions[i].itemName = potions[i].potionSO.potionName;
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            ingredients[i].itemName = ingredients[i].ingredientSO.ingredientName;
        }
    }

    public void AddItem(ScriptableObject scriptableObject)
    {
        int amount = 0;
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == scriptableObject.name)
            {
                potions[i].itemAmount++;                
                amount = potions[i].itemAmount;
                break;
            }
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].itemName == scriptableObject.name)
            {
                ingredients[i].itemAmount++;                
                amount = ingredients[i].itemAmount;
                break;
            }
        }
        //Debug.Log(name);
        onAddItemEvent.Invoke(scriptableObject, amount);
    }

    public void RemoveItem(ScriptableObject scriptableObject)
    {
        int amount = 0;
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == scriptableObject.name)
            {
                potions[i].itemAmount--;
                amount = potions[i].itemAmount;
                break;
            }
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].itemName == scriptableObject.name)
            {
                ingredients[i].itemAmount--;
                amount = ingredients[i].itemAmount;
                break;
            }
        }
        onRemoveItemEvent.Invoke(scriptableObject, amount);    
    }

    public bool IsPotionAvailable(ScriptableObject scriptableObject)
    {
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == scriptableObject.name)
            {
                if (potions[i].itemAmount > 0)
                {
                    return true;
                }
            }            
        }
        return false;
    }
}
