using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public List<PotionScriptableObject> Potions;
    //public StoreLevel storeLevel;
    //public CustomerSpawner customerSpawner;

    private void Awake()
    {
        SingletonManager.Register(this);

       // customerSpawner = GameObject.FindObjectOfType<CustomerSpawner>();
    }

    private void Start()
    {
        //initializePotion();
    }

    public void initializePotion()
    {
        
        ////customerSpawner.unlockPotion.Clear();

        //for (int i = 0; i < storeLevel.Level; i++)
        //{
        //    customerSpawner.unlockPotion.Add(Potions[i]);
        //}

       
    }
}
