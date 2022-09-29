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

    public Wallet playerWallet;
    List<GameObject> ordersList = new();
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
        orderListPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = potion.potionName;        
        orderListPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = potion.buyPrice.ToString();        
        orderListPrefab.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => SellOrder(potion));
    }

    public void SellOrder(PotionScriptableObject potion)
    {
        //if(Inventory.instance.IsPotionAvailable(potion.potionName))        
        Debug.Log(potion.potionName);

        // Gain money
        playerWallet.AddMoney((int)potion.buyPrice);
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
