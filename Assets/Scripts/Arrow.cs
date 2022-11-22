
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [Header("Positions")]
    public RectTransform        pos1;
    public RectTransform        pos2;
    private Vector3         nextPos;
    public Transform        startPos;

    [Header("Win/Lose UI")]
    public GameObject       winConditionUI;
    public GameObject       loseConditionUI; 

    private Coroutine       movementRoutine;

   // public Meter meter;

    public int              speed;
    public List<int> speeds = new();
    [SerializeField] 
    bool                    isHitPoint;

    private UIManager       managerUI;

    [Header("Hitpoint")]
    public GameObject       hitPoint;
    public RectTransform    startHitPoint;
    public RectTransform    endHitPoint;
    public float            edgeVal1;
    public float            edgeVal2;
    private int             hitCount = 0;
    public int              requiredHits = 3;

    [Header("VFX")]
    public GameObject effect;
    public GameObject characterModel;

    private RectTransform transform;

    private void Awake()
    {
        this.gameObject.transform.position = startPos.position;
        transform = this.GetComponent<RectTransform>();
        nextPos = pos2.anchoredPosition;
        winConditionUI.SetActive(false);
        loseConditionUI.SetActive(false);
        if (managerUI == null)
        {
            if (GameObject.FindObjectOfType<UIManager>() != null)
            {
                managerUI = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
            }
        }
    }

    IEnumerator arrowMovement()
    {
        while(true)
        {
            if (transform.anchoredPosition.x >= startHitPoint.anchoredPosition.x && transform.anchoredPosition.x <= endHitPoint.anchoredPosition.x)//(transform.anchoredPosition.x >= edgeVal1 && transform.anchoredPosition.x <= edgeVal2) // edge value
            {
                isHitPoint = true;
                Debug.Log("Target");
            }
            else
            {
                isHitPoint = false;
            }

            if(this.transform.anchoredPosition.x <= pos1.anchoredPosition.x)//(this.transform.position.x == pos1.position.x)
            {
                nextPos = pos2.anchoredPosition;
                Debug.Log("Going to end point");
            }

            if (this.transform.anchoredPosition.x >= pos2.anchoredPosition.x)
            {
                //managerUI.FailureTXT.gameObject.SetActive(true);
                //if (loseConditionUI)
                //{
                //    loseConditionUI.SetActive(true);
                //}
                //Debug.Log("TIME UP");
                //StopCoroutine(movementRoutine);

                nextPos = pos1.anchoredPosition;
                Debug.Log("Going to start point");
            } 
            

            transform.anchoredPosition = Vector3.MoveTowards(this.transform.anchoredPosition, nextPos, speeds[hitCount] * Time.deltaTime);
            Debug.Log("Moving");
            yield return null;
        }
    }

    public void OnClick()
    {
        if (isHitPoint)
        {
            if (hitCount >= requiredHits - 1)
            {
                StopArrowMovement();
                ActivateSuccessUI();
                CraftingManager craftingManager = SingletonManager.Get<CraftingManager>();
                if (craftingManager)
                {
                    craftingManager.isCookingComplete = true;
                    craftingManager.OnCompleteCrafting();
                }
                PlayVFX();
                Debug.Log("Score");
            }
            else
            {
                hitCount++;
                this.gameObject.transform.position = startPos.position;
                nextPos = pos2.anchoredPosition;
            }
            //SingletonManager.Get<UIManager>().SuccessTXT.gameObject.SetActive(true);
            
        }
        else
        {
            //SingletonManager.Get<UIManager>().FailureTXT.gameObject.SetActive(true);
            //ActivateLoseUI();
            //Debug.Log("Miss");
        }
    }

    public void StartArrowMovement()
    {
        movementRoutine = StartCoroutine(arrowMovement());
    }

    public void StopArrowMovement()
    {
        if(movementRoutine == null) { return; }
        StopCoroutine(movementRoutine);
    }

    public void ResetBrewMeter()
    {
        this.gameObject.transform.position = startPos.position;
        winConditionUI.SetActive(false);
        loseConditionUI.SetActive(false);
        isHitPoint = false;
        hitCount = 0;
        StopArrowMovement();
    }

    public void ActivateSuccessUI()
    {
        if (winConditionUI == null) { return; }
        if(loseConditionUI == null) { return ; }
        winConditionUI.SetActive(true);
        loseConditionUI.SetActive(false);
    }

    public void ActivateLoseUI()
    {
        if (winConditionUI == null) { return; }
        if (loseConditionUI == null) { return; }
        winConditionUI.SetActive(false);
        loseConditionUI.SetActive(true);
    }

    public void PlayVFX()
    {
        if (effect == null) { return; }
        effect.SetActive(true);
        ParticleSystem effectParticle = effect.GetComponent<ParticleSystem>();
        effectParticle.Play();
    }
}
