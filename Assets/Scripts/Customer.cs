using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : SelectableObject
{
    public List<PotionScriptableObject> availablePotions;
    public List<string> customerOrder;

  
    public Transform targetPos;

    public int OrderQuantity;
    public int RNG;

    [Range(0,10)]
    public int markUP;

    [Range(0, 10)]
    public int speed;

    public bool isSelect;

    Coroutine animationRoutine;
   
    // Start is called before the first frame update
    void Awake()    
    {
        isSelect = false;
         markUP = Random.Range(0, 10); 

        StartCoroutine(initializeCustomerOrderList());
    }
    private void Start()
    {
        animationRoutine = StartCoroutine(moveAnimation());
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
            Debug.Log("Customer wants: " + potion.name);
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

    IEnumerator  moveAnimation()
    {
        while (this.gameObject.transform.position != targetPos.position)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, targetPos.position, speed * Time.deltaTime);
            yield return null;
        }

        animationRoutine = null;
    }
    // baseprice + markup
}
