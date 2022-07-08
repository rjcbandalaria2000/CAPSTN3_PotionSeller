using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int HighestScore { get; set; }

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        HighestScore = 1000;
    }
}
