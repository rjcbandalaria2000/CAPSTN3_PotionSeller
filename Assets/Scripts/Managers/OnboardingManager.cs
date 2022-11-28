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
    [HideInInspector] public int id;
    private int i = 0;

    public List<Dialogues> introText;
    public List<Dialogues> dialogueSOList;
    public TextMeshProUGUI panelText;
    public GameObject nextIntroButton;
    public GameObject nextButton;

    [Header("Inside Main Loop Variables")]
    public List<GameObject> arrowFlows;
    public List<Button> potionSelectButtons;
    
    public GameObject selectionManager;

    public Customer customer;
    public GameObject recipeButton;
    public Button recipeQuitButton;
    public ShopSelect shopSelect;
    public Button shopQuitButton;
    public BrewStation brewStation;
    public Arrow arrow;
    public Cauldron cauldron;
    public Stock stock;
    public QuestArea questArea;
    public GameObject pausePlayButton;
    public GameObject quitButton;

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
        SingletonManager.Register(this);
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
            selectionManager.SetActive(true);
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
            // Inside Main Loop
            case 0: // Customer / Order
                // turn on Customer component
                customer.GetComponent<Customer>().enabled = true;
                customer.onOnboardingClickEvent.AddListener(CustomerHit);
                break;
            case 1: // Check Recipe
                // turn on Recipe Button
                recipeButton.SetActive(true);
                recipeQuitButton.onClick.AddListener(RecipeQuitButtonHit);                
                break;
            case 2: // Go to Shop
                // turn on ShopSelect component
                shopSelect.GetComponent<ShopSelect>().enabled = true;
                shopQuitButton.onClick.AddListener(ShopQuitButtonHit);
                break;
            case 3: // Select Potion
                // turn on Brew Station component
                brewStation.GetComponent<BrewStation>().enabled = true;
                foreach (Button button in potionSelectButtons)
                {
                    button.onClick.AddListener(PotionSelectHit);
                }
                break;
            case 4: // Craft                
                // Player must craft through brewing to only proceed
                arrow.onOnboardingClickEvent.AddListener(CraftingDoneHit);
                break;
            case 5: // Cauldron
                // turn on Cauldron
                cauldron.GetComponent<Cauldron>().enabled = true;
                SingletonManager.Get<CraftingManager>().onQuestCompletedEvent.AddListener(PotionCreated);
                break;
            case 6: // Selling
                customer.onOnboardingClickEvent.AddListener(CustomerHit);
                customer.onOnboardingOrderComplete.AddListener(NextButtonHit);
                break;
            
            // Outside Main Loop
            case 7: // Stock
                // turn on Stock component
                stock.GetComponent<Stock>().enabled = true;
                break;
            case 8: // Quests
                // turn on QuestArea component
                questArea.GetComponent<QuestArea>().enabled = true;
                break;            
            case 12:
                // turn on Pause/Play Button
                pausePlayButton.SetActive(true);
                break;
            case 13:
                // turn on Exit Button
                quitButton.SetActive(true);
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

    private void CraftingDoneHit()
    {
        NextButtonHit();
        arrow.onOnboardingClickEvent.RemoveListener(CraftingDoneHit);
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
