using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : SelectableObject
{
    public OnboardingClickEvent     onOnboardingClickEvent = new();
    public QuestCompletedEvent      onQuestCompletedEvent = new();

    [Header("Stations")]
    public Mixing                   mixingPot;
    public GameObject               character;

    private CraftingManager         craftingManager;

    // Start is called before the first frame update
    void Start()
    {
        objectPanelUI.SetActive(false);
        DisplayCharacter(true);
        craftingManager = SingletonManager.Get<CraftingManager>();
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
        if (craftingManager)
        {
            if (!craftingManager.isCookingComplete) { return; }
        }
        if(objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);
        onQuestCompletedEvent.Invoke(QuestManager.instance?.useCauldronQuest);
        onOnboardingClickEvent.Invoke();
        mixingPot.SwitchTrailEffects(true);
        //DisplayCharacter(true);
    }

    public void CloseUIPanel()
    {
        if(mixingPot == null) { return;}
        mixingPot.ResetMixing();
        mixingPot.SwitchTrailEffects(false);
        if(objectPanelUI == null) { return; }
        objectPanelUI.SetActive(false);
        //DisplayCharacter(false);
    }
    
    public void DisplayCharacter(bool state)
    {
        if (character)
        {
            character.SetActive(state);
        }
    }

}
