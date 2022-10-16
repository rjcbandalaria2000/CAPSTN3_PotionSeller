using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayQuestUI : MonoBehaviour
{
    private List<GameObject> displayQuests = new();
    public GameObject questGameObjectPrefab;
    public GameObject questParentPanel;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        QuestManager.instance.onAddQuestEvent.AddListener(AddToUI);
        QuestManager.instance.onRemoveQuestEvent.AddListener(RemoveToUI);
        QuestManager.instance.onRemoveAllQuestEvent.AddListener(RemoveAll);
    }

    private void OnDisable()
    {
        QuestManager.instance.onAddQuestEvent.RemoveListener(AddToUI);
        QuestManager.instance.onRemoveQuestEvent.RemoveListener(RemoveToUI);
        QuestManager.instance.onRemoveAllQuestEvent.RemoveListener(RemoveAll);
    }

    private void AddToUI(Quest quest)
    {
        GameObject gameObject = Instantiate(questGameObjectPrefab, questParentPanel.transform, false);
        displayQuests.Add(gameObject);
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.questTitle;
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = quest.questReward.ToString();
    }

    private void RemoveToUI(Quest quest)
    {
        foreach(GameObject gameObject in displayQuests)
        {
            if (gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == quest.questTitle)
            {                
                Destroy(gameObject);
                displayQuests.Remove(gameObject);
                break;
            }
        }
    }

    private void RemoveAll()
    {
        foreach (GameObject gameObject in displayQuests)
        {
            Destroy(gameObject);
        }
        displayQuests.Clear();
    }
}
