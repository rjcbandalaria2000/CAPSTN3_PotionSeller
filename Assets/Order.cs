using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : SelectableObject
{
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
        //base.OnInteract();
        if (objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);
    }
}
