using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public int StartingMoney = 5000;
    public int Money = 0;
    public UnityEvent WalletUIUpdate = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        InitializeWallet();
    }

    void InitializeWallet()
    {
        Money = StartingMoney;
        WalletUIUpdate.Invoke();
    }

    public void SpendMoney(int cost)
    {
        if(Money > 0)
        {
            Money -= cost;
        }
        WalletUIUpdate.Invoke();
    }

    public void AddMoney(int money)
    {
        Money += money;
        WalletUIUpdate.Invoke();
    }

   
}
