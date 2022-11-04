using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI SuccessTXT;
    public TextMeshProUGUI FailureTXT;

    public GameObject IngredientBook;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        if(SuccessTXT != null && FailureTXT != null)
        {
            SuccessTXT.gameObject.SetActive(false);
            FailureTXT.gameObject.SetActive(false);
        }
      
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
}
