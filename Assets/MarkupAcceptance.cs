using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkupAcceptance : MonoBehaviour
{
    [Header("Acceptance Range")]
    public float minMarkUp;
    public float maxMarkUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool IsWithinMarkupRange(float markUpAmount)
    {
        return true;
    }

   
}
