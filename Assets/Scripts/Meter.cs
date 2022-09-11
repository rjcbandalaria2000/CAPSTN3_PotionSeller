using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    [Header("Header")]
    public GameObject meter_body;
    public GameObject hitPoint;
    public Arrow arrow;


    // Start is called before the first frame update
    void Start()
    {
        meter_body = this.GetComponent<GameObject>();
        arrow = GameObject.FindObjectOfType<Arrow>().GetComponent<Arrow>();
    }

}
