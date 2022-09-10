using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform startPos, endPos;

    public Meter meter;

    public int speed;


    // Start is called before the first frame update
    void Start()
    {
        if(meter == null)
        {
            if(GameObject.FindObjectOfType<Meter>() != null )
            {
                meter = GameObject.FindObjectOfType<Meter>().GetComponent<Meter>();
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(arrowMovement());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hitpoint") && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Score");
        }
    }

    IEnumerator arrowMovement()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        yield return null;
       
     
    }
}
