using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewStation : SelectableObject
{
    public bool isCompleted;

    public Arrow arrow;

    //public SelectableObject selectable;
    // Start is called before the first frame update
    void Start()
    {
        objectPanelUI.SetActive(false);
    }
    private void OnEnable()
    {
        onSelectableObjectClickedEvent.AddListener(OnInteract);
    }

    private void OnDisable()
    {
        onSelectableObjectClickedEvent.RemoveListener(OnInteract);
    }

    public override void OnInteract()
    {
        objectPanelUI.SetActive(true);
        if(arrow == null) { return; }
        arrow.StartArrowMovement();
    }

    public void CloseUIPanel()
    {
        if(objectPanelUI == null) { return; }
        objectPanelUI.SetActive(false);
    }
}
