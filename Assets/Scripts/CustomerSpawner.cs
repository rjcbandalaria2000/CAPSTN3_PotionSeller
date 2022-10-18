using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int customerQuantity;
    public Transform spawnPoint;
    public List<Transform> targetPoints;
    public List<GameObject> spawnCustomers;
    public List<bool> isOccupied;
    public int index;

    Coroutine customerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        customerSpawn = StartCoroutine(spawnCustomer());
    }

    // Update is called once per frame
    void Update()
    {
        
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
