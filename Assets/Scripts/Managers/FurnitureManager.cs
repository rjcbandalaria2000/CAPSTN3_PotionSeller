using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public List<GameObject> furnitures = new List<GameObject>();

    public void Awake()
    {
        SingletonManager.Register(this);
    }

    public void ActivateFurniture(int storeLevel)
    {
        furnitures[storeLevel - 1].SetActive(true);
    }


}
