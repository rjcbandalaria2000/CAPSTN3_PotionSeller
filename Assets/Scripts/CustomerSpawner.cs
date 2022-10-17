using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public int customerQuantity;
    public Transform spawnPoint;
    public List<Transform> targetPoints;
    private int index;

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
          
            yield return new WaitForSeconds(2.5f);
            index++;
        }
       
       
    }

    public void Spawn()
    {
        customerSpawn = StartCoroutine(spawnCustomer());
    }
}
