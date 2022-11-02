using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkupAcceptance : MonoBehaviour
{
    [Header("Acceptance Range")]
    public float minMarkUp;
    public float maxMarkUp;

    [Header("Random Acceptance Range")]
    [Range(0f, 2f)]
    public float minMarkUpRandomValue;
    [Range(0f, 2f)]
    public float maxMarkUpRandomValue;
    public List<float> markupValues = new();

    // Start is called before the first frame update
    void Start()
    {
        SetRandomMarkUpRandomValue();
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

    public void SetRandomMarkUpRandomValue()
    {
        float minRandomValue = Random.Range(minMarkUpRandomValue, maxMarkUpRandomValue);
        float maxRandomValue = Random.Range(minRandomValue, maxMarkUpRandomValue);
        //Adjust the multiplier to be rounded off in decimals 
        minMarkUp = Mathf.Round(minRandomValue * Constants.ROUND_OFF_DECIMAL) / Constants.ROUND_OFF_DECIMAL;
        maxMarkUp = Mathf.Round(maxRandomValue * Constants.ROUND_OFF_DECIMAL) / Constants.ROUND_OFF_DECIMAL;
        
    }

   
}
