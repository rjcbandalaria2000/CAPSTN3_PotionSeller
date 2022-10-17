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
        
    }

    public void AddItem(string name)
    {
        int amount = 0;
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == name)
            {
                potions[i].itemAmount++;                
                amount = potions[i].itemAmount;
                break;
            }
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].itemName == name)
            {
                ingredients[i].itemAmount++;                
                amount = ingredients[i].itemAmount;
                break;
            }
        }
        //Debug.Log(name);
        onAddItemEvent.Invoke(name, amount);
    }

    public void RemoveItem(string name)
    {
        int amount = 0;
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == name)
            {
                potions[i].itemAmount--;
                amount = potions[i].itemAmount;
                break;
            }
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].itemName == name)
            {
                ingredients[i].itemAmount--;
                amount = ingredients[i].itemAmount;
                break;
            }
        }
        onRemoveItemEvent.Invoke(name, amount);
    
    }

    public bool IsPotionAvailable(string name)
    {
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == name)
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
