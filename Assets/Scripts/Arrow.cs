
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [Header("Positions")]
    public Transform        pos1;
    public Transform        pos2;
    private Vector3         nextPos;
    public Transform        startPos;

    [Header("Win/Lose UI")]
    public GameObject       winConditionUI;
    public GameObject       loseConditionUI; 

    private Coroutine       movementRoutine;

   // public Meter meter;

    public int              speed;
    [SerializeField] 
    bool                    isHitPoint;

    private UIManager       managerUI;

    [Header("Hitpoint")]
    public GameObject       hitPoint;
    public RectTransform    startHitPoint;
    public RectTransform    endHitPoint;
    public float            edgeVal1;
    public float            edgeVal2;

    [Header("VFX")]
    public GameObject effect;

    private RectTransform transform;

    private void Awake()
    {
        this.gameObject.transform.position = startPos.position;
        transform = this.GetComponent<RectTransform>();
        nextPos = pos2.transform.position;
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
            if(this.transform.position.x == pos1.position.x)
            {
                nextPos = pos2.position;
            }


            if (transform.anchoredPosition.x >= startHitPoint.anchoredPosition.x && transform.anchoredPosition.x <= endHitPoint.anchoredPosition.x)//(transform.anchoredPosition.x >= edgeVal1 && transform.anchoredPosition.x <= edgeVal2) // edge value
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
            //SingletonManager.Get<UIManager>().SuccessTXT.gameObject.SetActive(true);
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
            //SingletonManager.Get<UIManager>().FailureTXT.gameObject.SetActive(true);
            ActivateLoseUI();
            Debug.Log("Miss");
        }
    }

    public void StartArrowMovement()
    {
        movementRoutine = StartCoroutine(arrowMovement());
    }

    public void ResetBrewMeter()
    {
        this.gameObject.transform.position = startPos.position;
        winConditionUI.SetActive(false);
        loseConditionUI.SetActive(false);
        isHitPoint = false; 
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
