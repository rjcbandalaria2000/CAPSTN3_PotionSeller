using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int customerQuantity;
    public Transform spawnPoint;
    public List<GameObject> spawnCustomers;
    public List<bool> isOccupied;
    public int index;
    public List<Transform> targetPoints = new();
    public List<GameObject> spawnedCustomers = new();

    Coroutine customerSpawn;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        customerSpawn = StartCoroutine(spawnCustomer());
    }

    IEnumerator spawnCustomer()
    {
        while (index < customerQuantity)
        {
            GameObject spawnCustomer = Instantiate(customer, spawnPoint.position, Quaternion.identity);
            spawnCustomer.GetComponent<Customer>().targetPos = targetPoints[index];
            spawnCustomers.Add(spawnCustomer);
            isOccupied[index] = true;
          
            yield return new WaitForSeconds(2.5f);
            index++;
        }

        index = 0;
    }

    public void Spawn()
    {
        customerSpawn = StartCoroutine(spawnCustomer());
    }

    public void RemoveCustomer(GameObject customer, string order)
    {
        if(spawnedCustomers.Count <= 0) { return; }
        for(int i = 0; i < spawnedCustomers.Count; i++)
        {
            if (spawnedCustomers[i] == customer)
            {
                Destroy(spawnedCustomers[i]);
                spawnedCustomers.Remove(spawnedCustomers[i]);
            }
        }
    }

    public void callNewCustomer()
    {
        if(spawnCustomers.Count < customerQuantity)
        {
            while(index < isOccupied.Count)
            {
                if (isOccupied[index] == false)
                {
                    GameObject spawnCustomer = Instantiate(customer, spawnPoint.position, Quaternion.identity);
                    spawnCustomer.GetComponent<Customer>().targetPos = targetPoints[index];
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
}
