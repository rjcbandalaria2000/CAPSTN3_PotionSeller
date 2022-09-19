using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
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
    }

    public static TimeChangedEvent onTimeChangedEvent = new();
    public static HourChangedEvent onHourChangedEvent = new();
    
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
        _instance = this;
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
    }

    private void OnDisable()
    {
        onTimeChangedEvent.RemoveListener(OnTimeCheck);
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
        if (p_hour == endHour)
        {
            // Invoke event ?
            hour = startHour;
            EndDay();
        }
    }

    private void EndDay()
    {
        // Next day
        // Invoke event ?
        StopCoroutine(coroutineTime);
        coroutineTime = null;
    }

    public void OnResetTimeButtonHit()
    {
        coroutineTime = DoTimerCoroutine();
        StartCoroutine(coroutineTime);
    }
}
