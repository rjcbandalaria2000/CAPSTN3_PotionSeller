using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Player : MonoBehaviour
{
    public float overallScore { get; set; }

    public void Awake()
    {
        SingletonManager.Register(this);
    }

    public void Start()
    {
        overallScore = 100;
    }
}
