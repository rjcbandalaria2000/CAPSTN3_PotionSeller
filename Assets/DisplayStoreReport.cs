using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class DisplayStoreReport : MonoBehaviour
{
    [Header("Managers")]
    public StoreLevel       storeLevel;
    public StatsManager     statsManager;
    public GameManager      gameManager;

    [Header("Texts")]
    public TextMeshProUGUI  storeLevelText;
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
        gameManager = SingletonManager.Get<GameManager>();
        if (gameManager)
        {
            gameManager.onGameFinish.AddListener(UpdateDisplayStoreReport);
        }
        UpdateDisplayStoreReport();
    }

    public void UpdateDisplayStoreReport()
    {
        Assert.IsNotNull(storeLevel, "Store level is not set or null");
        Assert.IsNotNull(statsManager, "Stats Managers is not set or null");
        Assert.IsNotNull(gameManager, "Game Manager is not set or null");

        if(storeLevelText == null) { return; }
        storeLevelText.text = storeLevel.Level.ToString();
        if (totalGoldEarnedText == null) { return; }
        totalGoldEarnedText.text = statsManager.totalGoldEarned.ToString("0");
        if (totalGoldSpentText == null) { return; }
        totalGoldSpentText.text = statsManager.totalGoldSpent.ToString("0");
        if (potionsCreatedText == null) { return; }
        potionsCreatedText.text = statsManager.potionsCreated.ToString("0");
        if (customersServedText == null) { return; }
        customersServedText.text = statsManager.customersServed.ToString("0");
        if (customersMissedText == null) { return; }
        customersMissedText.text = statsManager.customersMissed.ToString("0");

    }

   
}
