using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{    
    public OnSelectableObjectClicked onSelectableObjectClickedEvent = new OnSelectableObjectClicked();

    [Header("SelectableObject Variables")]
    public string objectName;
    public GameObject objectNameUI;
    public GameObject objectPanelUI;

    public GameObject objectOrderUI;

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
        //onSelectableObjectClickedEvent.Invoke();
        // Object's UI Panel must open up
        // objectUI.SetActive(true);
    }
}
