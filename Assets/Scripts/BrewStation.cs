using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewStation : SelectableObject
{
    [Header("States")]
    public bool isCompleted;

    public Arrow arrow;
    public GameObject prepStationPanel;
    public GameObject brewStationPanel;

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
        prepStationPanel.SetActive(true);
        brewStationPanel.SetActive(false);
        //if(arrow == null) { return; }
        //arrow.StartArrowMovement();
    }

    public void CloseUIPanel()
    {   //Reset Brew Meter
        if(arrow == null) { return; }
        arrow.ResetBrewMeter();
        if(objectPanelUI == null) { return; }
        objectPanelUI.SetActive(false);
       
    }

    public void OnNextButtonClicked()
    {
        CraftingManager craftingManager = SingletonManager.Get<CraftingManager>();
        //When the potion in Prep station is completed
        if (craftingManager)
        {
            if (craftingManager.selectedPotionScriptableObject)
            {
                // Only proceed to the next step if a potion to make is selected
                prepStationPanel.SetActive(false);
                brewStationPanel.SetActive(true);
                arrow.StartArrowMovement();
            }
            else
            {
                Debug.Log("No potion has been selected");
            }
        }
    }
}
