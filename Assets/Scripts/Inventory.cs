using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Inventory>();
            }
            return _instance;
        }
    }

    public List<ItemData> potions = new();
    public List<ItemData> ingredients = new();

    private void Awake()
    {
        _instance = this;

        //SingletonManager.Register(this);
    }

    public void AddItem(string name)
    {        
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == name)
            {
                potions[i].itemAmount++;
            }
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].itemName == name)
            {
                ingredients[i].itemAmount++;
            }
        }
    }

    public void RemoveItem(string name)
    {
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].itemName == name)
            {
                potions[i].itemAmount--;
            }
        }
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].itemName == name)
            {
                ingredients[i].itemAmount--;
            }
        }
    } 
}
