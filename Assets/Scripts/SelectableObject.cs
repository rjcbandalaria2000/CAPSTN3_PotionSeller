using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{    
    public OnSelectableObjectClicked onSelectableObjectClickedEvent = new OnSelectableObjectClicked();

    public string objectName;
    public GameObject objectUI;

    private void OnEnable()
    {
        onSelectableObjectClickedEvent.AddListener(OnInteract);
    }

    private void OnDisable()
    {
        onSelectableObjectClickedEvent.RemoveListener(OnInteract);
    }

    public virtual void OnInteract()
    {
        // Temporary Output Log
        Debug.Log(objectName + " clicked!");
        
        // Object's UI Panel must open up
        // objectUI.SetActive(true);
    }
}
