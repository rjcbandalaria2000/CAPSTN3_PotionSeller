using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClockUI : MonoBehaviour
{
    private TMP_Text displayText;

    private void Awake()
    {
        displayText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        TimeManager.onTimeChangedEvent.AddListener(UpdateTimeText);
    }

    private void OnDisable()
    {
        TimeManager.onTimeChangedEvent.RemoveListener(UpdateTimeText);
    }

    private void UpdateTimeText(int hour, int minuteByTens)
    {
        displayText.text = $"{hour:00}:{minuteByTens:00}";
    }
}
