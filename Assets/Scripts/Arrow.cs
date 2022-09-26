
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [Header("Positions")]
    public Transform pos1;
    public Transform pos2;
    private Vector3 nextPos;
    public Transform startPos;

    [Header("Win/Lose UI")]
    public GameObject winConditionUI;
    public GameObject loseConditionUI; 

    private Coroutine movementRoutine;

   // public Meter meter;

    public int speed;
    [SerializeField] bool isHitPoint;

    private UIManager managerUI;

    [Header("Hitpoint")]
    public GameObject hitPoint;
    public float edgeVal1;
    public float edgeVal2;

    private RectTransform transform;

    private void Awake()
    {
          this.gameObject.transform.position = startPos.position;
        transform = this.GetComponent<RectTransform>();
        nextPos = pos2.transform.position;
       
        if(managerUI == null)
        {
            if (GameObject.FindObjectOfType<UIManager>() != null)
            {
                managerUI = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.GetComponent<RectTransform>().anchoredPosition.x);

    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Hitpoint"))
    //    {
    //        isHitPoint = true;
    //        Debug.Log("Target");

    //    }
    //}
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Hitpoint"))
    //    {
    //        isHitPoint = true;
    //        Debug.Log("Target");

    //    }
    //}

    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Hitpoint"))
    //    {
    //        isHitPoint = true;
    //        Debug.Log("Target");

    //    }
    //}
    //public void OnTriggerExit(Collider other)
    //{
    //    isHitPoint = false;
    //}

    IEnumerator arrowMovement()
    {
        while(true)
        {
            if(this.transform.position.x == pos1.position.x)
            {
                nextPos = pos2.position;
            }


            if (transform.anchoredPosition.x >= edgeVal1 && transform.anchoredPosition.x <= edgeVal2) // edge value
            {
                isHitPoint = true;
                Debug.Log("Target");
            }
            else
            {
                isHitPoint = false;
            }

            if (this.transform.position.x == pos2.position.x)
            {
                //managerUI.FailureTXT.gameObject.SetActive(true);
                if (loseConditionUI)
                {
                    loseConditionUI.SetActive(true);
                }
                Debug.Log("TIME UP");
                StopCoroutine(movementRoutine);

                //nextPos = pos1.position;
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

    public void StartArrowMovement()
    {
        movementRoutine = StartCoroutine(arrowMovement());
    }

}
