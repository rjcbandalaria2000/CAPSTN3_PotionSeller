using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//DELETE THIS CODE UPON FINISH PROTOTYPE
public class Score_Manager : MonoBehaviour
{
    public Text score;

    [SerializeField] private Data_Player data;

    private void Start()
    {
        if(data == null)
        {
            data = SingletonManager.Get<Data_Player>();
        }
    }

    private void Update()
    {
        score.text = "Score: " + data.overallScore.ToString();
    }
}
