using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI SuccessTXT;
    public TextMeshProUGUI FailureTXT;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        if(SuccessTXT != null && FailureTXT != null)
        {
            SuccessTXT.gameObject.SetActive(false);
            FailureTXT.gameObject.SetActive(false);
        }
      
    }

    public void ClosePanel()
    {

    }
}
