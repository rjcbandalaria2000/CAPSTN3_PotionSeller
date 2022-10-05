using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : SelectableObject
{
    public Mixing mixingPot;
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
        //base.OnInteract();
        if(objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);
    }

    public void CloseUIPanel()
    {
        if(mixingPot == null) { return;}
        mixingPot.ResetMixing();
        if(objectPanelUI == null) { return; }
        objectPanelUI.SetActive(false);
    }
   

}
