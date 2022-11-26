using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkupAcceptance : MonoBehaviour
{
    [Header("Acceptance Range")]
    public float minMarkUp;
    public float maxMarkUp;

    //[Header("Random Acceptance Range")]
    //[Range(0f, 2f)]
    //public float minMarkUpRandomValue;
    //[Range(0f, 2f)]
    //public float maxMarkUpRandomValue;
    //public List<float> markupValues = new();

    // Start is called before the first frame update
    void Start()
    {
        //SetRandomMarkUpRandomValue();
    }

    public bool IsWithinMarkupRange(float markUpAmount)
    {
        // if the the mark up is below the max and above the minimum
        if (markUpAmount <= maxMarkUp && markUpAmount >= minMarkUp)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    //public void SetRandomMarkUpRandomValue()
    //{
    //    // Only get the half for the minRandomValue
    //    int minRandomValue = Random.Range(0, Mathf.RoundToInt(markupValues.Count / 2));
    //    minMarkUp = markupValues[minRandomValue];
    //    int maxRandomValue = Random.Range(markupValues.Count / 2, markupValues.Count);
    //    maxMarkUp = markupValues[maxRandomValue];      
    //}

   
}
