using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGoal : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI goalText;
    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = SingletonManager.Get<GameManager>();
        }
        goalText = this.GetComponent<TextMeshProUGUI>();
        UpdatePlayerGoal();
    }

    public void UpdatePlayerGoal()
    {
       if(gameManager == null) { return; }
        goalText.text = "Goal: " + gameManager.goalMoney.ToString("0");
        
    }
}
