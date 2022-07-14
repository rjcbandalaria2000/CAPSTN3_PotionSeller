using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int StartingMoney = 5000;
    public int Money = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeWallet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeWallet()
    {
        Money = StartingMoney;
    }
}
