using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : SelectableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        //base.OnInteract();
        if(objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);
    }

    public void CloseUIPanel()
    {
        objectPanelUI.SetActive(false);
    }
   

}
