using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : SelectableObject
{
    public List<PotionScriptableObject> availablePotions;
    public List<string> customerOrder;

    public int OrderQuantity;
    public int RNG;

    [Range(0,10)]
    public int markUP;

    public bool isSelect;
   
    // Start is called before the first frame update
    void Start()    
    {
        isSelect = false;
         markUP = Random.Range(0, 10); 

        StartCoroutine(initializeCustomerOrderList());
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
            customerOrder.Add(potion.name);
            OrderManager.instance.onCustomerOrderEvent.Invoke(potion);
            Debug.Log("Customer wants: " + availablePotions[RNG].name);
        }
        yield return null;
    }

    public void checkItem()
    {
        for(int i = 0; i < customerOrder.Count; i++)
        {
            if(customerOrder[i] == availablePotions[i].name) //This line is draft, change statement availablePotion
            {
                Debug.Log("Correct Item");
                customerOrder.RemoveAt(i);
            }
        }
    }
   
    public override void OnInteract()
    {
        Debug.Log("Customer Select");

        if(isSelect == false)
        {
            isSelect = true;
        
            
        }
    }

    // baseprice + markup
}
