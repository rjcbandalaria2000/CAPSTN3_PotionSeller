using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : MonoBehaviour
{
    public List<PotionScriptableObject> availablePotions;
    public List<string> customerOrder;

    public int OrderQuantity;
    public int RNG;

    [Range(0,10)]
    public int markUP;

   
    // Start is called before the first frame update
    void Start()
    {
        markUP = Random.Range(0, 10); 

        StartCoroutine(initializeCustomerOrderList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator initializeCustomerOrderList()
    {
        for (int i = 0; i < OrderQuantity; i++)
        {
            RNG = Random.Range(0, availablePotions.Count);
            customerOrder.Add(availablePotions[RNG].name);
            Debug.Log("Customer wants: " + availablePotions[RNG].name);
        }
        yield return null;

    }

    // baseprice + markup
}
