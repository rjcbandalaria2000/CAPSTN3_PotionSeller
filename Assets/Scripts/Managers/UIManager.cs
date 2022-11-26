using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{ 
    public GameObject IngredientBook;
    public GameObject storeReportPanel;
    

    [Header("Condition Panels")]
    public GameObject conditionPanel;
    public GameObject winConditionPanel;
    public GameObject loseConditionPanel;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
      
    }

    public void ClosePanel()
    {

    }

    public void ActivateIngredientBook()
    {
        IngredientBook.SetActive(true);
    }

    public void DeactivateIngredientBook()
    {
        IngredientBook.SetActive(false);
    }

    public void ActivateStoreReport()
    {
        if (storeReportPanel == null) { return; }
        storeReportPanel.SetActive(true);
    }

    public void DeactivateStoreReport()
    {
        if (storeReportPanel == null) { return; }
        storeReportPanel.SetActive(false);
    }

    public void ActivateConditionPanel()
    {
        if(conditionPanel == null) { return; }
        conditionPanel.SetActive(true);
    }

    public void DeactivateConditionPanel()
    {
        if (conditionPanel == null) { return; }
        conditionPanel.SetActive(false);
    }

    public void ActivateWinConditionPanel(bool hasWon)
    {
        if(winConditionPanel == null) { return; }
        if(loseConditionPanel == null) { return; }
        winConditionPanel.SetActive(hasWon);
        loseConditionPanel.SetActive(!hasWon);
    }

    public void DeactivateWinConditionPanel()
    {
        if(winConditionPanel== null) { return; }
        winConditionPanel.SetActive(false);
    }

    public void DeactivateLoseConditionPanel()
    {
        if (loseConditionPanel == null) { return; }
        loseConditionPanel.SetActive(false);
    }
   
}
