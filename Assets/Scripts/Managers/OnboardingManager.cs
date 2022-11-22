using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    private int id;
    private int i = 0;

    public List<Dialogues> introText;
    public List<Dialogues> dialogueSOList;
    public TextMeshProUGUI panelText;
    public GameObject nextIntroButton;
    public GameObject nextButton;

    [Header("Inside Main Loop Variables")]
    public List<GameObject> arrowFlows;
    public List<Button> potionSelectButtons;
    public Customer customer;
    public Button recipeQuitButton;
    public Button shopQuitButton;
    public Button brewHitButton;
    public Cauldron cauldron;

    [Header("Scene Change")]
    public int sceneID;

    private BasicSceneManager basicSceneManager;

    private void Awake()
    {        
        basicSceneManager = this.GetComponent<BasicSceneManager>();
    }

    private void Start()
    {
        NextIntroText();
    }

    public void NextIntroText()
    {
        if (id < introText.Count)
        {
            panelText.text = introText[id].dialogues[0].ToString();            
            id++;
        }
        else
        {
            id = 0;
            nextIntroButton.GetComponent<Button>().onClick.RemoveAllListeners();
            nextIntroButton.SetActive(false);
            NextButtonHit();
        }
    }

    public void NextButtonHit()
    {
        if (id >= dialogueSOList.Count)
        {
            // Next scene?
            Assert.IsNotNull(basicSceneManager, "Scene manager is null or is not set");
            basicSceneManager.loadScene(sceneID);
            return;
        }

        AddEventListenerOnID(id);    
        
        foreach(GameObject gameObject in arrowFlows)
        {
            if (gameObject)
            {
                gameObject.SetActive(false);
            }
        }

        if (arrowFlows[id])
        {
            arrowFlows[id].SetActive(true);
        }
        else
        {
            Debug.Log("arrowFlows[" + id + "] is Null.");
        }

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
        Debug.Log(id);
        switch (id)
        {
            case 0:
                customer.onOnboardingClickEvent.AddListener(CustomerHit);
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
                SingletonManager.Get<CraftingManager>().onQuestCompletedEvent.AddListener(PotionCreated);                
                break;
            case 7:                
                customer.onOnboardingClickEvent.AddListener(CustomerHit);
                customer.onOnboardingOrderComplete.AddListener(NextButtonHit);
                break;
        }
    }

    #region Onboarding Inside Main Loop Function Flows
    private void CustomerHit()
    {
        NextButtonHit();
        customer.onOnboardingClickEvent.RemoveListener(CustomerHit);
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

    private void PotionCreated(Quest quest)
    {
        NextButtonHit();
        SingletonManager.Get<CraftingManager>().onQuestCompletedEvent.RemoveListener(PotionCreated);
    }
    #endregion
}
