using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using DG.Tweening;


public class Customer : SelectableObject
{
    public Inventory                        playerInventory;
    public List<PotionScriptableObject>     availablePotions;
    // Customer might have more orders, hence "List"
    public List<PotionScriptableObject>     customerPotion = new();
    public Transform                        targetPos;


    public GameObject                       thisParent;

    public int                              OrderQuantity;
    public int                              RNG;

    public TextMeshProUGUI                  orderName;

    [Range(0, 10)]
    public int                              speed;

    public bool                             isSelect;

    Coroutine                               animationRoutine;

    public OnboardingClickEvent             onOnboardingClickEvent = new();
    public OnboardingOrderComplete          onOnboardingOrderComplete = new();
    public OnOrderComplete                  onOrderComplete = new OnOrderComplete();

    public MarkupAcceptance                 markupAcceptance;
   
    [Header("Animator")]
    public Animator                         animator;

    private StatsManager                    statsManager;
    private StoreLevel                      storeLevel;

    // Start is called before the first frame update
    void Awake()    
    {
      
    }
    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        thisParent = this.transform.parent.gameObject;
        isSelect = false;
        //markUP = Random.Range(0, 10);
        playerInventory = SingletonManager.Get<Inventory>();
        StartCoroutine(initializeCustomerOrderList());
        animationRoutine = StartCoroutine(moveAnimation());
        markupAcceptance = this.GetComponent<MarkupAcceptance>();
        statsManager = SingletonManager.Get<StatsManager>();
        storeLevel = SingletonManager.Get<StoreLevel>();    
    }
    private void OnEnable()
    {
        onSelectableObjectClickedEvent.AddListener(OnInteract);
    }

    private void OnDisable()
    {
        onSelectableObjectClickedEvent.RemoveListener(OnInteract);
    }

    IEnumerator initializeCustomerOrderList()
    {
        for (int i = 0; i < OrderQuantity; i++)
        {
            RNG = Random.Range(0, availablePotions.Count);
            PotionScriptableObject potion = availablePotions[RNG];
            customerPotion.Add(potion);
            OrderManager.instance.onCustomerOrderEvent.Invoke(potion, this);
           //orderName.text = potion.name;
            //Debug.Log("Customer wants: " + potion.name);
        }
        yield return null;
    }

    public void checkItem()
    {
        for(int i = 0; i < customerPotion.Count; i++)
        {
            if(customerPotion[i] == availablePotions[i]) //This line is draft, change statement availablePotion
            {
                Debug.Log("Correct Item");
                customerPotion.RemoveAt(i);
            }
        }
    }
   
    public override void OnInteract()
    {
        //Debug.Log("Customer Select");

        // Set ObjectPanelUI
        objectPanelUI = OrderManager.instance.orderPanelUI;
        OrderManager.instance.potionOrder = customerPotion[0];

        // Change to OrderManager things
        OrderManager.instance.orderImage.sprite = customerPotion[0].potionIconSprite;
        OrderManager.instance.orderName.text =  customerPotion[0].description[0];
        OrderManager.instance.orderPrice.text = $"<sprite=0> " + GetCalculatedPriceWithLevel(customerPotion[0]).ToString();
        OrderManager.instance.origOrderPrice.text = $"<sprite=0> " + GetCalculatedPriceWithLevel(customerPotion[0]).ToString();
        OrderManager.instance.sellButton.onClick.RemoveAllListeners();
        OrderManager.instance.orderDropdown.value = 0;
        OrderManager.instance.sellButton.onClick.AddListener(() => SellOrder());

        onOnboardingClickEvent.Invoke();

        Assert.IsNotNull(objectPanelUI, "UI panel not set or found");
        if (objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);

        if (isSelect == false)
        {
            isSelect = true;
        }
    }

    public void SellOrder()
    {
        for (int i = 0; i < playerInventory.potions.Count; i++)
        {
            if (playerInventory.potions[i].itemName == customerPotion[0].potionName)
            {
                if (playerInventory.potions[i].itemAmount >= 1)
                {
                    OrderManager.instance.onQuestCompletedEvent?.Invoke(QuestManager.instance.sellPotionQuest);

                    Debug.Log("Has enough " + playerInventory.potions[i].itemName + " in the inventory. Selling...");
                    //Debug.Log("SOLD: Markup Percent is " + markupPercent);
                    //Debug.Log("SOLD: Sold for " + Mathf.RoundToInt(potion.buyPrice + (potion.buyPrice * markupPercent)));

                    // Remove potion order item
                    playerInventory.RemoveItem(customerPotion[0]);

                    if (markupAcceptance.IsWithinMarkupRange(OrderManager.instance.markupPercent))
                    {

                        // Gain money
                        OrderManager.instance.playerWallet.AddMoney(GetCalculatedSellPriceWithMarkup(customerPotion[0]));
                        
                        // Play money sound
                        SingletonManager.Get<AudioManager>().Play(Constants.COINS_SOUND);

                        // Add experience 
                        //OrderManager.instance.storeLevel.onGainExp.Invoke(OrderManager.instance.sellExpPoints);
                        SingletonManager.Get<StoreLevel>().AddExpPoints(OrderManager.instance.sellExpPoints);                        

                        Debug.Log("Correct Markup price");

                        if (statsManager)
                        {
                            statsManager.AddTotalGoldEarned(GetCalculatedSellPriceWithMarkup(customerPotion[0]));
                            statsManager.customersServed++;
                        }

                    }
                    else
                    {
                        if (statsManager)
                        {
                            statsManager.customersMissed++;
                        }
                        
                        // Play rejected sound
                        SingletonManager.Get<AudioManager>().Play(Constants.REJECTED_SOUND);

                        Debug.Log("Incorrect mark up price");
                    }

                    //Temporary display of markup feedback 
                    SingletonManager.Get<DisplayMarkUpFeedback>().StartDisplayMarkupFeedback(markupAcceptance.IsWithinMarkupRange(OrderManager.instance.markupPercent));

                    // Remove listener reference ?
                    OrderManager.instance.sellButton.onClick.RemoveListener(() => SellOrder());


                    //Close UI panel after selling transaction
                    OrderManager.instance.orderPanelUI.SetActive(false);
                    //Show UI feedback

                    // Destroy gameObject and call (spawn) a new customer (gameObject)
                    if (SingletonManager.Get<CustomerSpawner>())
                    {
                        SingletonManager.Get<CustomerSpawner>().CustomerToRemove(thisParent);
                    }

                    //onOrderComplete.Invoke();
                    onOnboardingOrderComplete.Invoke();

                   

                    Destroy(thisParent.gameObject);

                                    
                    //SingletonManager.Get<CustomerSpawner>().CheckForNull();
                }
                else
                {
                    SingletonManager.Get<AudioManager>().Play(Constants.WARNING_SOUND);
                    Debug.Log("Not enough potions to sell");                    
                }
                break;
            }            
        }
    }

    IEnumerator  moveAnimation()
    {
        if (targetPos)
        {
            thisParent.transform.DOMove(targetPos.position, speed);



            while (thisParent.gameObject.transform.position != targetPos.position)
            {
                animator.SetBool("IsIdle", false);
                yield return null;
            }
        }
        else
        {
            Debug.Log("No target position");
        }

        if (thisParent.gameObject.transform.position == targetPos.position)
        {
            SingletonManager.Get<AudioManager>().Play(Constants.OWL_SOUND);
        }
            
        animator.SetBool("IsIdle", true);
    }

    public float GetCalculatedPriceWithLevel(PotionScriptableObject potion)
    {
        if (storeLevel)
        {
            return potion.buyPrice + storeLevel.Level;
        }
        else
        {
            return potion.buyPrice;
        }

    }

    public int GetCalculatedSellPriceWithMarkup(PotionScriptableObject potion)
    {
        return Mathf.RoundToInt(GetCalculatedPriceWithLevel(potion) + (GetCalculatedPriceWithLevel(potion) * OrderManager.instance.markupPercent));
    }
    // baseprice + markup
}
