using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Wallet playerWallet;

    [Header("Condition Objects")]
    public int goalMoney = 1000;
    public GameObject conditionPanel;
    public TextMeshProUGUI conditionText;
    public Color winColor;
    public Color loseColor;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void OnEnable()
    {
        TimeManager.onDayEndedEvent.AddListener(GoalCheck);
    }
    private void OnDisable()
    {
        TimeManager.onDayEndedEvent.RemoveListener(GoalCheck);
    }

    private void GoalCheck(int dayCount)
    {
        if (dayCount >= Constants.MAX_DAY)
        {
            TimeManager.onPauseGameTime.Invoke(true);
            if (playerWallet.Money >= goalMoney) // WIN CONDITION
            {
                // Fire WIN - event transition something
                conditionPanel.SetActive(true);
                conditionText.text = "YOU WIN!!!";
                conditionText.color = winColor;
                // Play animation transition?
                // Play particle system?
                // Play sound?
            }
            else // LOSE CONDITION
            {
                // Fire LOSE - event transition something
                conditionPanel.SetActive(true);
                conditionText.text = "YOU LOSE...";
                conditionText.color = loseColor;
                // Play animation transition?
                // Play particle system?
                // Play sound?
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}