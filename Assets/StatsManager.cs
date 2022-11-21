using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public int totalGoldEarned = 0;
    public int totalGoldSpent = 0;
    public int potionsCreated = 0;
    public int customersServed = 0;
    public int customersMissed = 0;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddTotalGoldEarned(int goldEarned)
    {
        totalGoldEarned += goldEarned;
    }

    public void AddTotalGoldSpent(int goldSpent)
    {
        totalGoldSpent += goldSpent;
    }

    
  
}
