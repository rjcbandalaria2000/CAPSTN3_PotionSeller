using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public Wallet           playerWallet;

    [Header("Condition Objects")]
    public int              goalMoney = 1000;
    public GameObject       conditionPanel;
    public TextMeshProUGUI  conditionText;
    public Color            winColor;
    public Color            loseColor;

    [Header("Unity Events")]
    public OnGameWin        onGameWin = new();
    public OnGameLose       onGameLose = new();
    public OnGameFinish     onGameFinish = new();

    private UIManager       uiManager;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        uiManager = SingletonManager.Get<UIManager>();
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
            //Display End day prompt
            conditionPanel.SetActive(true);
        }
    }

    public void OnWin()
    {
        // Fire WIN - event transition something
        //conditionPanel.SetActive(true);
        //conditionText.text = "YOU WIN!!!";
        //conditionText.color = winColor;
        // Play animation transition?
        // Play particle system?
        // Play sound?
        Assert.IsNotNull(uiManager, "UI manager not set or null");
        uiManager.ActivateWinConditionPanel(true);
    }

    public void OnLose()
    {
        // Fire LOSE - event transition something
        Assert.IsNotNull(uiManager, "UI manager not set or null");
        uiManager.ActivateWinConditionPanel(false);
        //conditionText.text = "YOU LOSE...";
        //conditionText.color = loseColor;
        // Play animation transition?
        // Play particle system?
        // Play sound?
    }

    public void CheckEndingCondition()
    {
        if (playerWallet.Money >= goalMoney) // WIN CONDITION
        {
            OnWin();
        }
        else // LOSE CONDITION
        {
            OnLose();
        }
        onGameFinish.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}