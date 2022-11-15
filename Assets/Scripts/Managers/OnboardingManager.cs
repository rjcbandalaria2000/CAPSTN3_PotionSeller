using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    private int id;
    private int i = 0;
    
    public List<Dialogues> dialogueSOList;
    public TextMeshProUGUI panelText;
    public GameObject nextButton;

    [Header("Inside Main Loop Variables")]
    public List<GameObject> arrowFlows;
    public List<Customer> customers;
    public List<Button> potionSelectButtons;
    public Button recipeQuitButton;
    public Button shopQuitButton;
    public Button brewHitButton;
    public Cauldron cauldron;
    public Button sellButton;

    private void Start()
    {
        NextButtonHit();
    }

    public void NextButtonHit()
    {
        AddEventListenerOnID(id);
        foreach(GameObject gameObject in arrowFlows)
        {
            gameObject.SetActive(false);
        }
        arrowFlows[id].SetActive(true);
        nextButton.SetActive(dialogueSOList[id].isButtonShown);
        if (dialogueSOList[id].dialogues.Count > 1)
        {
            panelText.text = dialogueSOList[id].dialogues[i].ToString();
            i++;
            if (i >= dialogueSOList[id].dialogues.Count)
            {
                id++;
                i = 0;
                nextButton.SetActive(true);
            }            
        }
        else
        {
            panelText.text = dialogueSOList[id].dialogues[0].ToString();
            id++;
            i = 0;
        }     
    }

    private void AddEventListenerOnID(int id)
    {
        switch (id)
        {
            // Inside Main Loop
            case 0:
                foreach (Customer customer in customers)
                {
                    customer.onOnboardingClickEvent.AddListener(CustomerHit);
                }                
                break;
            case 1:
                foreach (Button button in potionSelectButtons)
                {
                    button.onClick.AddListener(PotionSelectHit);
                }
                break;
            case 2:
                recipeQuitButton.onClick.AddListener(RecipeQuitButtonHit);
                break;
            case 3:
                shopQuitButton.onClick.AddListener(ShopQuitButtonHit);
                break;
            case 4:
                foreach (Button button in potionSelectButtons)
                {
                    button.onClick.AddListener(PotionSelectHit);
                }
                break;
            case 5:
                brewHitButton.onClick.AddListener(BrewButtonHit);
                break;
            case 6:
                cauldron.onOnboardingClickEvent.AddListener(CauldronHit);
                break;
            case 7:
                foreach (Customer customer in customers)
                {
                    customer.onOnboardingClickEvent.AddListener(CustomerHit);
                }                
                sellButton.onClick.AddListener(SellButtonHit);
                // markup listener
                break;
            // Outside Main Loop
        }
    }

    #region Onboarding Inside Main Loop Function Flows
    private void CustomerHit()
    {
        NextButtonHit();
        foreach (Customer customer in customers)
        {
            customer.onOnboardingClickEvent.RemoveListener(CustomerHit);
        }
    }

    private void PotionSelectHit()
    {
        NextButtonHit();
        foreach (Button button in potionSelectButtons)
        {
            button.onClick.RemoveListener(PotionSelectHit);
        }
    }

    private void RecipeQuitButtonHit()
    {
        NextButtonHit();
        recipeQuitButton.onClick.RemoveListener(RecipeQuitButtonHit);
    }

    private void ShopQuitButtonHit()
    {
        NextButtonHit();
        shopQuitButton.onClick.RemoveListener(ShopQuitButtonHit);
    }

    private void BrewButtonHit()
    {
        NextButtonHit();
        brewHitButton.onClick.RemoveListener(BrewButtonHit);
    }

    private void CauldronHit()
    {
        NextButtonHit();
        cauldron.onOnboardingClickEvent.RemoveListener(CauldronHit);
    }

    private void SellButtonHit()
    {
        NextButtonHit();
        sellButton.onClick.RemoveListener(SellButtonHit);
    }
    #endregion

    #region Onboarding Outside Main Loop Function Flows

    #endregion
}
