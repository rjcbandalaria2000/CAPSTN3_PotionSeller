using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayInfoUI : MonoBehaviour
{
    private TMP_Text dayText;

    private void Awake()
    {
        dayText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        TimeManager.onDayEndedEvent.AddListener(UpdateDayText);
    }

    private void OnDisable()
    {
        TimeManager.onDayEndedEvent.RemoveListener(UpdateDayText);
    }

    private void UpdateDayText(int dayCount)
    {
        dayText.text = "Day " + dayCount;
    }
}
