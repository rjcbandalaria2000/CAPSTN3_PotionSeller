using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int customerQuantity;
    public Transform spawnPoint;
    public List<Transform> targetPoints = new();

    public int index;
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
            spawnedCustomers.Add(spawnCustomer);
            yield return new WaitForSeconds(2.5f);
            index++;
        }
       
       
    }

    public void Spawn()
    {
        customerSpawn = StartCoroutine(spawnCustomer());
    }

    public void RemoveCustomer()
    {
        
    }

}
