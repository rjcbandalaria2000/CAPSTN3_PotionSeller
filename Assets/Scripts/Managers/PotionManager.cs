using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public List<PotionScriptableObject> Potions;
    public StoreLevel storeLevel;
    public CustomerSpawner customerSpawner;

    public void initializePotion()
    {

        for (int i = 0; i < storeLevel.Level; i++)
        {
            customerSpawner.unlockPotion[i] = Potions[i];
        }

        //switch (storeLevel.Level)
        //{
        //    case 1:
        //        for (int i = 0; i < storeLevel.Level; i++)
        //        {
        //            customerSpawner.unlockPotion[i] = Potions[i];
        //        }
        //        break;
        //    case 2:
        //        for (int i = 0; i < storeLevel.Level; i++)
        //        {
        //            customerSpawner.unlockPotion[i] = Potions[i];
        //        }
        //        break;

        //}

        
    }
}
