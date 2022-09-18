using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixingTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [Header("States")]
    public bool IsMixing;
    public bool SwipedRight;
    public bool SwipedLeft;

    [Header("Values")]
    public int RequiredSwipes = 1;
    public int SwipeCount;
    public float SwipeRightAccept = 0.5f;
    public float SwipeLeftAccept = -0.5f;

    [Header("Potion")]
    public GameObject Parent;
    public GameObject PotionToGive;
    public GameObject PotionReceived;
    
    private Vector2 InitialPosition;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        IsMixing = true;
        InitialPosition = eventData.position;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (IsMixing)
        {
            Debug.Log("OnPointerMove");
            Vector2 mousePosition = eventData.position - InitialPosition;
            if(mousePosition.normalized.x > SwipeRightAccept)
            {
                Debug.Log("SwipeRight");
                SwipedRight = true;
            }
            if(mousePosition.normalized.x < SwipeLeftAccept)
            {
                Debug.Log("SwipeLeft");
                SwipedLeft = true;
            }
            if(SwipedLeft && SwipedRight)
            {
                SwipeCount++;
                SwipedLeft = false;
                SwipedRight = false;
            }
            Debug.Log("Mouse Position " + mousePosition.normalized.x);
            Debug.Log("Object selected: " + eventData.pointerPress.name);
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsMixing = false;

        Debug.Log("OnPointerUp");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
