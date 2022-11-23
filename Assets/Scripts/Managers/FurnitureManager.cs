using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FurnitureManager : MonoBehaviour
{
    public List<GameObject> furnitures = new List<GameObject>();

    public void Awake()
    {
        SingletonManager.Register(this);
    }

    public void ActivateFurniture(int storeLevel)
    {
        if(furnitures.Count <= 0) { return; }
        if(storeLevel > furnitures.Count) { return; }
        for(int i = 0; i < furnitures.Count; i++)
        { 
            if(i < storeLevel)
            {
                furnitures[i].SetActive(true);
            }
            else
            {
                furnitures[i].SetActive(false);
            }
        } 
        
    }


}
