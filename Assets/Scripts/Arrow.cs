using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Positions")]
    public Transform pos1, pos2;
    private Vector3 nextPos;
    public Transform startPos;

    public Meter meter;

    public int speed;
    [SerializeField] bool isHitPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = startPos.position;
        nextPos = pos2.transform.position;
        if (meter == null)
        {
            if(GameObject.FindObjectOfType<Meter>() != null )
            {
                meter = GameObject.FindObjectOfType<Meter>().GetComponent<Meter>();
            }
            
        }
        StartCoroutine(arrowMovement());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Hitpoint"))
    //    {
    //        Debug.Log("Score");
    //        StopCoroutine(arrowMovement());
    //    }
    //    else
    //    {
    //        Debug.Log("Miss");
    //    }
    //}

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Hitpoint"))
    //    {
    //        isHitPoint = true;
    //        Debug.Log("Target");

    //    }
        
    //}
    //public void OnCollisionExit(Collision collision)
    //{
    //    isHitPoint = false;
    //}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hitpoint"))
        {
            isHitPoint = true;
            Debug.Log("Target");

        }
    }
    public void OnTriggerExit(Collider other)
    {
        isHitPoint = false;
    }

    IEnumerator arrowMovement()
    {
        while(true)
        {
            if(this.transform.position.x == pos1.position.x)
            {
                nextPos = pos2.position;
            }
            if(this.transform.position.x == pos2.position.x)
            {
                nextPos = pos1.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnClick()
    {
        StopCoroutine(arrowMovement());

        if (isHitPoint)
        {
           
           
            Debug.Log("Score");
        }
        else
        {
            Debug.Log("Miss");
        }
    }



}
