using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int customerQuantity;
    public List<Transform> spawnPoint;
    public List<Transform> targetPoints;

    Coroutine customerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        customerSpawn = StartCoroutine(spawnCustomer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnCustomer()
    {
        for(int i = 0; i < customerQuantity; i++)
        {
            GameObject spawnCustomer = Instantiate(customer, spawnPoint[i].position, Quaternion.identity);
            spawnCustomer.GetComponent<Customer>().targetPos = targetPoints[i];
            //Debug.Log("Customer");
        }
        yield return null;
    }

    public void Spawn()
    {
        customerSpawn = StartCoroutine(spawnCustomer());
    }
}
