using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class CustomerSpawner : MonoBehaviour
{

    public List<GameObject> customer;
    public int customerQuantity;
    public Transform spawnPoint;
    public List<GameObject> spawnCustomers = new();
    public List<bool> isOccupied;
    public int index;
    public List<Transform> targetPoints = new();

    public List<PotionScriptableObject> unlockPotion;
    public StoreLevel storeLevel;
    //public List<GameObject> spawnedCustomers = new();

    Coroutine customerSpawn;
    Coroutine newSpawn;

    private int RNG;
    private void Awake()
    {
       
        index = 0;
        SingletonManager.Register(this);
        storeLevel = GameObject.FindObjectOfType<StoreLevel>();
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //StartCoroutine(CheckForNull());
        initializeUnlockPotion();
        customerSpawn = StartCoroutine(spawnCustomer());
    }
    
    public void initializeUnlockPotion()
    {
        if(storeLevel.Level >= SingletonManager.Get<PotionManager>().Potions.Count) {
            Debug.Log("Store level is higher than the potions count");
            return; }
        unlockPotion.Clear();

        for (int i = 0; i <= storeLevel.Level; i++)
        {
            unlockPotion.Add(SingletonManager.Get<PotionManager>().Potions[i]);
        }
    }

    public void resetSpawner()
    {
        spawnCustomers.Clear();

        for(int i = 0; i <= isOccupied.Count; i++)
        {
            isOccupied[i] = false;
        }

        index = 0;
    }


    IEnumerator spawnCustomer()
    {
        while (index < customerQuantity)
        {
            RNG = Random.Range(0, customer.Count);

            GameObject spawnCustomer = Instantiate(customer[RNG], spawnPoint.position, Quaternion.identity);
            spawnCustomer.transform.GetChild(0).GetComponent<Customer>().targetPos = targetPoints[index];
            spawnCustomer.transform.GetChild(0).GetComponent<Customer>().availablePotions.Clear();
            spawnCustomer.transform.GetChild(0).GetComponent<Customer>().availablePotions = unlockPotion;
            spawnCustomer.transform.GetChild(0).GetComponent<Customer>().onOrderComplete.AddListener(newCustomerSpawn);
            spawnCustomers.Add(spawnCustomer);
            isOccupied[index] = true;
          
            yield return new WaitForSeconds(2.5f);
            index++;
        }
        index = 0;
    }

    public void RemoveCustomer()
    {
        //if(spawnCustomers.Count <= 0) { return; }
        for (int i = 0; i < spawnCustomers.Count; i++)
        {
            if (spawnCustomers[i] == null)
            {
                //spawnCustomers.Remove(spawnCustomers[i]);
                spawnCustomers.RemoveAt(i);
                isOccupied[i] = false;
                Debug.Log("Remove null");
                break;
            }
        }
    }


    public void newCustomerSpawn()
    {
        Debug.Log("Calling New Customer");
        newSpawn = StartCoroutine(callNewCustomer());
    }

    public IEnumerator callNewCustomer()
    {
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Calling New Customer");
        if (spawnCustomers.Count <= customerQuantity)
        {
            while(index < isOccupied.Count)
            {
                if (isOccupied[index] == false)
                {
                    RNG = Random.Range(0,customer.Count);

                    GameObject spawnCustomer = Instantiate(customer[RNG], spawnPoint.position, Quaternion.identity);
                    spawnCustomer.transform.GetChild(0).GetComponent<Customer>().targetPos = targetPoints[index];
                    spawnCustomer.transform.GetChild(0).GetComponent<Customer>().availablePotions.Clear();
                    spawnCustomer.transform.GetChild(0).GetComponent<Customer>().availablePotions = unlockPotion;
                    spawnCustomers[index] = spawnCustomer;
                    isOccupied[index] = true;
                    break; // Remove this if wants to spawn simultaneously
                }
                else
                {
                    Debug.Log("IsOccupied");
                    index += 1;
                }
            }
           
        }
        else
        {
            Debug.Log("Full Capacity");
        }
        index = 0;
    }

    public void CustomerToRemove(GameObject customerToRemove)
    {
        for(int i = 0; i < spawnCustomers.Count; i++)
        {
            if (spawnCustomers[i] == customerToRemove)
            {
               // Destroy(spawnCustomers[i]);
                spawnCustomers[i] = null;
                isOccupied[i] = false;
                Debug.Log("Remove Customer");

                newCustomerSpawn();
                break;
            }
        }
    }
    
}
