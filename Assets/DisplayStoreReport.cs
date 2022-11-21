using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStoreReport : MonoBehaviour
{
    [Header("Managers")]
    public StoreLevel       storeLevel;
    public StatsManager     statsManager;

    [Header("Texts")]
    public TextMeshProUGUI  totalGoldEarnedText;
    public TextMeshProUGUI  totalGoldSpentText;
    public TextMeshProUGUI  potionsCreatedText;
    public TextMeshProUGUI  customersServedText;
    public TextMeshProUGUI  customersMissedText;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        storeLevel = SingletonManager.Get<StoreLevel>();
        statsManager = SingletonManager.Get<StatsManager>();
    }

   
}
