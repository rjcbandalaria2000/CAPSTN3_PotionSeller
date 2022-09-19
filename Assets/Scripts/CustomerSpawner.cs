using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public Transform spawnPoint;
    public int customerQuantity;

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
           // GameObject spawnCustomer = Instantiate(customer,spawnPoint.position,Quaternion.identity);
            Debug.Log("Customer");
        }
        yield return null;
    }
}
