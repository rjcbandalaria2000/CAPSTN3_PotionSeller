using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCauldronLockFeedback : MonoBehaviour
{
    public const string     FEEDBACK_TEXT = "Go to Crafting Station first";
    public float            duration = 2f;
    public string           message = FEEDBACK_TEXT;
    public TextMeshProUGUI  text;

    private CraftingManager craftingManager;
    private UIManager       uiManager;
    private Coroutine       cauldronFeedbackRoutine;

    private void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
        craftingManager = SingletonManager.Get<CraftingManager>();
        uiManager = SingletonManager.Get<UIManager>();
        if (craftingManager)
        {
            craftingManager.onCauldronLocked.AddListener(StartCauldronFeedback);
        }
        if (uiManager)
        {
            uiManager.basicSceneManager.onSceneChange.AddListener(OnSceneChange);
        }
    }

    public void StartCauldronFeedback()
    {
        cauldronFeedbackRoutine = StartCoroutine(CauldronFeedback());
    }

    public void StopCauldronFeedBack()
    {
        if(cauldronFeedbackRoutine == null) { return; }
        text.enabled = false;
        StopCoroutine(cauldronFeedbackRoutine);
    }

    IEnumerator CauldronFeedback()
    {
        if (!text.enabled)
        {
            text.enabled = true;
            text.text = message;
            yield return new WaitForSeconds(duration);
            text.enabled = false;
        }
    }

    public void OnSceneChange()
    {
        StopCauldronFeedBack();
        craftingManager.onCauldronLocked.RemoveListener(StartCauldronFeedback);
        uiManager.basicSceneManager.onSceneChange.RemoveListener(OnSceneChange);
    }
}
