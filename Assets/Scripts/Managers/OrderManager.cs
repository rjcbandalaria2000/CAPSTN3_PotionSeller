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

    public float                            markupPercent = 0f;
    public Wallet                           playerWallet;
    public StoreLevel                       storeLevel;
    public int                              sellExpPoints = 10;

    [Header("Unity Events")]
    public OnCustomerOrder                  onCustomerOrderEvent = new OnCustomerOrder();
    public QuestCompletedEvent              onQuestCompletedEvent = new QuestCompletedEvent();

    [Header("UI Element")]
    public GameObject                       orderParentPanel;
    public GameObject                       orderPrefabUI;
    
    private List<PotionScriptableObject>    potions = new();
    private List<GameObject>                ordersList = new();
    private Inventory                       playerInventory;

    private void Awake()
    {
        _instance = this;
        playerInventory = SingletonManager.Get<Inventory>();
        //SingletonManager.Register(this);
    }

    private void OnEnable()
    {
        onCustomerOrderEvent.AddListener(AddOrder);
    }
    private void OnDisable()
    {
        onCustomerOrderEvent.RemoveListener(AddOrder);
    }

    public void AddOrder(PotionScriptableObject potion)
    {
        GameObject orderListPrefab = Instantiate(orderPrefabUI, orderParentPanel.transform, false);
        ordersList.Add(orderListPrefab);
        potions.Add(potion);
        orderListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = potion.potionName;
        orderListPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = potion.buyPrice.ToString();
        orderListPrefab.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => SellOrder(potion));
    }

    public void RefreshList(int value)
    {
        SetMarkupPercent(value);
        for (int i = 0; i < ordersList.Count; i++)
        {
            //sellingPrice = selling price + (selling price* markup percent)
            float sellingPrice = 0;
            //Debug.Log(potions[j] + " | " + potions[j].buyPrice);
            sellingPrice = potions[i].buyPrice + (potions[i].buyPrice * markupPercent);
            //Debug.Log(sellingPrice);
            ordersList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(sellingPrice).ToString();
        }
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

    public void SellOrder(PotionScriptableObject potion)
    {
        
        //if(Inventory.instance.IsPotionAvailable(potion.potionName))        
        //Debug.Log(potion.potionName);
        //List<PotionScriptableObject> tempPotionsList = new();
        // Gain money
        foreach (PotionScriptableObject potionScriptableObject in potions.ToList())
        {
            if (potionScriptableObject.potionName == potion.potionName)
            { 
                // check if there is an available potion in the player inventory
                for(int i = 0; i < playerInventory.potions.Count; i++)
                {
                    if (playerInventory.potions[i].itemName == potion.potionName)
                    {
                        if (playerInventory.potions[i].itemAmount >= 1)
                        {
                            onQuestCompletedEvent?.Invoke(QuestManager.instance.sellPotionQuest);
                            Debug.Log("Has enough " + potion.potionName + " in the inventory... Selling");
                            playerInventory.potions[i].itemAmount -= 1;
                            potions.Remove(potionScriptableObject);
                            Debug.Log("SOLD: Markup Percent is " + markupPercent);
                            playerWallet.AddMoney(Mathf.RoundToInt(potionScriptableObject.buyPrice + (potionScriptableObject.buyPrice * markupPercent)));

                            //Add experience 
                            storeLevel.onGainExp.Invoke(sellExpPoints);

                            //Remove Order from OrderList
                            // Remove Order from UI
                            foreach (GameObject order in ordersList.ToList())
                            {
                                if (order.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == potion.potionName)
                                {
                                    order.transform.GetChild(3).GetComponent<Button>().onClick.RemoveListener(() => SellOrder(potion));
                                    ordersList.Remove(order);
                                    Destroy(order.gameObject);
                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            Debug.Log("Not enough potion");
                        }
                    }
                }                
            }
        }       
    }
}