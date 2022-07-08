using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    public Text ScoreText;

    private PlayerData playerData;

    private void Start()
    {
        playerData = SingletonManager.Get<PlayerData>();
    }
        
    private void Update()
    {
        ScoreText.text = "Score: " + playerData.HighestScore.ToString("N0");
    }
}
