using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OrderManager : MonoBehaviour
{
    private static OrderManager             _instance;
    public static OrderManager              instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<OrderManager>();
            }
            return _instance;
        }
    }

    public float markupPercent = 0f;
    public Wallet playerWallet;
    public StoreLevel storeLevel;
    public int sellExpPoints = 10;

    [Header("Unity Events")]
    public OnCustomerOrder onCustomerOrderEvent = new OnCustomerOrder();
    public QuestCompletedEvent onQuestCompletedEvent = new QuestCompletedEvent();

    [Header("UI Element")]
    public GameObject orderPanelUI;
    public GameObject orderParentPanel;
    public GameObject orderPrefabUI;
    public Image orderImage;
    public TextMeshProUGUI orderName;
    public TextMeshProUGUI orderPrice;
    public TextMeshProUGUI origOrderPrice;
    public TMP_Dropdown orderDropdown;
    public Button sellButton;

    [HideInInspector]
    public PotionScriptableObject potionOrder;

    private List<PotionScriptableObject> potions = new();
    private List<GameObject> ordersList = new();
    private List<Customer> customers = new();
    private Inventory playerInventory;
    public Dictionary<Customer, PotionScriptableObject> customerOrderDictionary = new(); // test

    private StatsManager statsManager;
    private void Awake()
    {
        _instance = this;
        
        //SingletonManager.Register(this);
    }

    private void Start()
    {
        playerInventory = SingletonManager.Get<Inventory>();
        statsManager = SingletonManager.Get<StatsManager>();
        storeLevel = SingletonManager.Get<StoreLevel>();
    }

    private void OnEnable()
    {
        //onCustomerOrderEvent.AddListener(AddOrder);
    }
    private void OnDisable()
    {
        //onCustomerOrderEvent.RemoveListener(AddOrder);
    }

    public void AddOrder(PotionScriptableObject potion, Customer customer)
    {
        GameObject orderListPrefab = Instantiate(orderPrefabUI, orderParentPanel.transform, false);
        ordersList.Add(orderListPrefab);
        potions.Add(potion);
        customers.Add(customer);
        customerOrderDictionary.Add(customer, potion);
        orderListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = potion.potionName;
        orderListPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"<sprite=0> " + GetCalculatedPriceWithLevel(potion).ToString();
        orderListPrefab.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => SellOrder(orderListPrefab.transform, potion, customer.gameObject));
    }

    public void RefreshList(int value)
    {
        float sellingPrice = GetCalculatedSellPriceWithMarkup(potionOrder);//potionOrder.buyPrice + (potionOrder.buyPrice * markupPercent);
        orderPrice.text = $"<sprite=0> "+ Mathf.RoundToInt(sellingPrice).ToString();
    }

    public void SetMarkupPercent(int value)
    {
        switch (value)
        {
            case 0:
                markupPercent = 0f;
                break;
            case 1:
                markupPercent = 0.5f;
                break;
            case 2:
                markupPercent = 1f;
                break;
            case 3:
                markupPercent = 1.5f;
                break;
            case 4:
                markupPercent = 2f;
                break;
        }
    }

    public void SellOrder(Transform transform, PotionScriptableObject potion, GameObject gameObject)
    { 
        // check if there is an available potion in the player inventory
        for (int i = 0; i < playerInventory.potions.Count; i++)
        {
            if (playerInventory.potions[i].itemName == potion.potionName)
            {                
                if (playerInventory.potions[i].itemAmount >= 1)
                {
                    onQuestCompletedEvent?.Invoke(QuestManager.instance.sellPotionQuest);

                    //Debug.Log("Has enough " + potion.potionName + " in the inventory... Selling");
                    //Debug.Log("SOLD: Markup Percent is " + markupPercent);
                    //Debug.Log("SOLD: Sold for " + Mathf.RoundToInt(potion.buyPrice + (potion.buyPrice * markupPercent)));
                    // Remove potion order item
                    playerInventory.RemoveItem(potion,0);
                    // Gain money
                    playerWallet.AddMoney(Mathf.RoundToInt(potion.buyPrice + (potion.buyPrice * markupPercent)));
                    
                    //Add experience 
                    storeLevel.onGainExp.Invoke(sellExpPoints);

                

                    //Remove Order from OrderList
                    // Remove Order from UI
                    /*foreach (GameObject order in ordersList.ToList())
                    {
                        if (order.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == potion.potionName)
                        {
                            order.transform.GetChild(3).GetComponent<Button>().onClick.RemoveListener(() => SellOrder(potion));
                            ordersList.Remove(order);
                            Destroy(order.gameObject);
                            break;
                        }
                    }*/

                    //Temp fix reduce customer index to be able to spawn next available customer
                    //SingletonManager.Get<CustomerSpawner>().index--;
                    //if the potion is ordered by the customer

                    //foreach (Customer customer in customers)
                    //{
                    //    if (customerOrderDictionary.TryGetValue(customer, out potion))
                    //    {
                    //        customerOrderDictionary.Remove(customer);
                    //        break;
                    //    }
                    //}

                    //transform.GetChild(8).GetComponent<Button>().onClick.RemoveListener(() => OrderManager.instance.SellOrder(transform, potion, customer));

                    Destroy(gameObject);
                    //SingletonManager.Get<CustomerSpawner>().RemoveCustomer();
                    //StartCoroutine(SingletonManager.Get<CustomerSpawner>().callNewCustomer());                    
                }
                else
                {
                    Debug.Log("Not enough potions to sell");                    
                }
                break;
            }
        }     
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
        return Mathf.RoundToInt(GetCalculatedPriceWithLevel(potion) + (GetCalculatedPriceWithLevel(potion) * markupPercent));
    }
}