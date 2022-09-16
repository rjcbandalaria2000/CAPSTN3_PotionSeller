using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        Items.Add(item);
    }

    public void RemoveItem(GameObject item)
    {
        Items.Remove(item);
    }
 
}
