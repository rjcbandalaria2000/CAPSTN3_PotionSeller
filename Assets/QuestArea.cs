using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArea : SelectableObject
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
        OpenUIPanel();
    }

    public void OpenUIPanel()
    {
        if (objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);
    }

    public void CloseUIPanel()
    {
        objectPanelUI.SetActive(false);
    }

}
