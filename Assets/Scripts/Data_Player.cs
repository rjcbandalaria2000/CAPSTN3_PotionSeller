using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Player : MonoBehaviour
{
    private float overallScore { get; set; }

    public void Awake()
    {
        SingletonManager.Register(this);
    }
}
