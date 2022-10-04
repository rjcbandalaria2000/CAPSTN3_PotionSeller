using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    private static OrderManager _instance;
    public static OrderManager instance
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
    private List<PotionScriptableObject> potions = new();
    private List<GameObject> ordersList = new();
    public OnCustomerOrder onCustomerOrderEvent = new OnCustomerOrder();
    public GameObject orderParentPanel;
    public GameObject orderPrefabUI;

    private void Awake()
    {
        _instance = this;

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

        // Gain money
        foreach (PotionScriptableObject potionScriptableObject in potions)
        {
            if (potionScriptableObject.potionName == potion.potionName)
            {
                potions.Remove(potionScriptableObject);
                Debug.Log("SOLD: Markup Percent is " + markupPercent);
                playerWallet.AddMoney(Mathf.RoundToInt(potionScriptableObject.buyPrice + (potionScriptableObject.buyPrice * markupPercent)));
                break;
            }
        }
        //Remove Order from OrderList
        // Remove Order from UI
        foreach (GameObject order in ordersList)
        {
            if (order.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == potion.potionName)
            {
                ordersList.Remove(order);
                Destroy(order.gameObject);
                break;
            }
        }
    }
}