using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int customerQuantity;
    public Transform spawnPoint;
    public List<GameObject> spawnCustomers = new();
    public List<bool> isOccupied;
    public int index;
    public List<Transform> targetPoints = new();
    //public List<GameObject> spawnedCustomers = new();

    Coroutine customerSpawn;
    Coroutine newSpawn;
    

    private void Awake()
    {
        index = 0;
        SingletonManager.Register(this);
        customerSpawn = StartCoroutine(spawnCustomer());
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CheckForNull());
    }

    void Update()
    {
        
    }

    IEnumerator spawnCustomer()
    {
        while (index < customerQuantity)
        {
            GameObject spawnCustomer = Instantiate(customer, spawnPoint.position, Quaternion.identity);
            spawnCustomer.transform.GetChild(0).GetComponent<Customer>().targetPos = targetPoints[index];
            spawnCustomer.transform.GetChild(0).GetComponent<Customer>().onOrderComplete.AddListener(newCustomerSpawn);
            spawnCustomers.Add(spawnCustomer);
            isOccupied[index] = true;
          
            yield return new WaitForSeconds(2.5f);
            index++;
        }
        index = 0;
    }

    //public IEnumerator CheckForNull()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        for (int i = 0; i < spawnCustomers.Count; i++)
    //        {
    //            if (spawnCustomers[i] == null)
    //            {
    //                Debug.Log("Remove null");
    //                spawnCustomers.Remove(spawnCustomers[i]);
    //                //spawnCustomers.RemoveAt(i);
    //                isOccupied[i] = false;
    //                break;
    //            }
    //        }
    //    }

    //}
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
        newSpawn = StartCoroutine(callNewCustomer());
    }

    public IEnumerator callNewCustomer()
    {
        Debug.Log("Remove Customer");
        RemoveCustomer();
        yield return new WaitForSeconds(1.0f);

        Debug.Log("Calling New Customer");
        if (spawnCustomers.Count < customerQuantity)
        {
            while(index < isOccupied.Count)
            {
                if (isOccupied[index] == false)
                {
                    GameObject spawnCustomer = Instantiate(customer, spawnPoint.position, Quaternion.identity);
                    spawnCustomer.transform.GetChild(0).GetComponent<Customer>().targetPos = targetPoints[index];
                    spawnCustomers.Add(spawnCustomer);
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
                spawnCustomers.RemoveAt(i);
                isOccupied[i] = false;
                Debug.Log("Remove Customer");
                break;
            }
        }
    }
    
}
