using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayMarkUpFeedback : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public float time = 2.5f;
    public string feedback;

    private Coroutine displayMarkupFeedbackRoutine;

    private void Awake()
    {
        //Temporary Singleton
        SingletonManager.Register(this);
        feedbackText = this.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        feedbackText.enabled = false;
    }

    public void StartDisplayMarkupFeedback(bool isAccepted)
    {
        displayMarkupFeedbackRoutine = StartCoroutine(DisplayMarkupFeedback(isAccepted));
    }

    IEnumerator DisplayMarkupFeedback(bool isAccepted)
    {
        feedbackText.enabled = true;
        SetFeedback(isAccepted);
        feedbackText.text = feedback;
        yield return new WaitForSeconds(time);
        feedbackText.enabled = false;
        feedbackText.text = "";
    }

    public void SetFeedback(bool isAccepted)
    {
        if (isAccepted)
        {
            feedback = "Markup Accepted";
        }
        else if(!isAccepted)
        {
            feedback = "Markup Rejected";
        }
        else
        {
            feedback = "";
        }
    }
}
