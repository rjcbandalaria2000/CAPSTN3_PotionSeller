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
    public GameObject conditionPanel;

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

   
}
