using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    /*private static TimeManager _instance;
    public static TimeManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TimeManager>();
            }
            return _instance;
        }
    }*/

    public static TimeChangedEvent onTimeChangedEvent = new();
    public static HourChangedEvent onHourChangedEvent = new();
    public static DayEndedEvent onDayEndedEvent = new();
    public static PauseGameTime onPauseGameTime = new();

    public int dayCount;

    [SerializeField] private int startHour;
    [SerializeField] private int endHour;
    public static int minute;
    public static int minuteByTens;
    private int hour;
    [SerializeField] private float minToRealSeconds;
    public bool doTimer = true;
    public IEnumerator coroutineTime;

    private void Awake()
    {
        //_instance = this;
        SingletonManager.Register(this);
    }

    private void Start()
    {
        minute = 0;
        hour = startHour;
        onHourChangedEvent.Invoke(hour);
        coroutineTime = DoTimerCoroutine();
        StartCoroutine(coroutineTime);
    }

    private void OnEnable()
    {
        onTimeChangedEvent.AddListener(OnTimeCheck);
        onPauseGameTime.AddListener(SetPauseGame);
    }

    private void OnDisable()
    {
        onTimeChangedEvent.RemoveListener(OnTimeCheck);
        onPauseGameTime.RemoveListener(SetPauseGame);
    }

    private IEnumerator DoTimerCoroutine()
    {
        while (doTimer)
        {
            yield return new WaitForSeconds(minToRealSeconds);
            minute++;

            if (minute % 10 == 0)
            {
                minuteByTens = minute;
            }

            if (minute >= Constants.MINUTES_IN_HOUR)
            {
                hour++;

                if (hour > Constants.HOURS_IN_DAY)
                {
                    hour = 1;
                }

                onHourChangedEvent.Invoke(hour);
                minute = 0;
                minuteByTens = 0;
            }
            onTimeChangedEvent.Invoke(hour, minuteByTens);
        }
    }

    private void OnTimeCheck(int p_hour, int p_minuteByTens)
    {
        if (p_hour >= endHour)
        {
            hour = startHour;
            EndDay();            
        }
    }

    public void EndDay()
    {
        // Next day
        dayCount++;
        // Invoke event ?
        onDayEndedEvent.Invoke(dayCount);
        ResetTime();        
    }

    public void OnTimeSkipButtonHit()
    {
        TimeSkip();
    }

    private void TimeSkip()
    {      
        dayCount = 30;
        onDayEndedEvent.Invoke(dayCount);
        ResetTime();
    }    

    public void OnResetTimeButtonHit()
    {
        ResetTime();
    }

    private void ResetTime()
    {
        hour = startHour;
        minute = 0;
        minuteByTens = 0;
        if (coroutineTime != null)
        {
            StopCoroutine(coroutineTime);
            coroutineTime = null;
        }
        coroutineTime = DoTimerCoroutine();
        StartCoroutine(coroutineTime);
    }

    private void SetPauseGame(bool p_bool)
    {
        doTimer = p_bool;
        if (p_bool) // Pause the game if TRUE
        {
            if (coroutineTime != null)
            {
                Debug.Log(GetPauseLogic(p_bool));
                StopCoroutine(coroutineTime);
                coroutineTime = null;
            }
        }
        else // Play the game if FALSE
        {
            Debug.Log(GetPauseLogic(p_bool));
            coroutineTime = DoTimerCoroutine();  
            StartCoroutine(coroutineTime);
        }
    }

    private string GetPauseLogic(bool p_bool)
    {
        string debugString;
        if (p_bool)
        {
            debugString = "Play game!";
        }
        else
        {
            debugString = "Game is paused.";
        }
        return debugString;
    }
}
