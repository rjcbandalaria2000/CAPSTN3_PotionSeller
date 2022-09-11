using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Positions")]
    public Transform pos1, pos2;
    private Vector3 nextPos;
    public Transform startPos;

    private Coroutine movementRoutine;

   // public Meter meter;

    public int speed;
    [SerializeField] bool isHitPoint;

    private UI_Manager managerUI;
    

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = startPos.position;
        nextPos = pos2.transform.position;
        if(managerUI == null)
        {
            if (GameObject.FindObjectOfType<UI_Manager>() != null)
            {
                managerUI = GameObject.FindObjectOfType<UI_Manager>().GetComponent<UI_Manager>();
            }
        }

        movementRoutine = StartCoroutine(arrowMovement());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

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
                managerUI.FailureTXT.gameObject.SetActive(true);
                Debug.Log("TIME UP");
                StopCoroutine(movementRoutine);

            }

            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnClick()
    {
        StopCoroutine(movementRoutine);

        if (isHitPoint)
        {
           managerUI.SuccessTXT.gameObject.SetActive(true);
           
            Debug.Log("Score");
        }
        else
        {
            managerUI.FailureTXT.gameObject.SetActive(true);
            Debug.Log("Miss");
        }
    }



}
