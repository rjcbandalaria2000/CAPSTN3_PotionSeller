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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hitpoint") && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Score");
        }
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
}
