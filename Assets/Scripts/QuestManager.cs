using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuestManager>();
            }
            return _instance;
        }
    }

    public AddQuestEvent onAddQuestEvent = new AddQuestEvent();
    public RemoveQuestEvent onRemoveQuestEvent = new RemoveQuestEvent();
    public RemoveAllQuestsEvent onRemoveAllQuestEvent = new RemoveAllQuestsEvent();

    public List<Quest> questsList = new();
    private List<Quest> questsOfTheDay = new();

    [Header("Object References")]
    public CraftingManager craftingManager;
    public Wallet playerWallet;
    public Cauldron cauldron;
    public GameObject questIndicator;

    [Header("Quest References")]
    public Quest sellPotionQuest;
    public Quest createPotionQuest;
    public Quest addMoneyQuest;
    public Quest spendMoneyQuest;
    public Quest useCauldronQuest;

    private void Awake()
    {
        _instance = this;
        //SingletonManager.Register(this);
    }

    private void Start()
    {
        InitNewQuests(1);
    }

    private void OnEnable()
    {
        TimeManager.onDayEndedEvent.AddListener(InitNewQuests);
        OrderManager.instance.onQuestCompletedEvent?.AddListener(QuestComplete);
        craftingManager.onQuestCompletedEvent?.AddListener(QuestComplete);
        playerWallet.onQuestCompletedEvent.AddListener(QuestComplete);
        cauldron.onQuestCompletedEvent.AddListener(QuestComplete);
    }
    private void OnDisable()
    {
        TimeManager.onDayEndedEvent.RemoveListener(InitNewQuests);
        OrderManager.instance.onQuestCompletedEvent?.RemoveListener(QuestComplete);
        craftingManager.onQuestCompletedEvent?.RemoveListener(QuestComplete);
        playerWallet.onQuestCompletedEvent.RemoveListener(QuestComplete);
        cauldron.onQuestCompletedEvent.RemoveListener(QuestComplete);
    }

    private void InitNewQuests(int dayCount)
    {
        questIndicator.SetActive(true);
        // Play sound
        SingletonManager.Get<AudioManager>().Play(Constants.DAYRESET_SOUND);

        if (questsOfTheDay.Count > 0)
        {
            questsOfTheDay.Clear();
            onRemoveAllQuestEvent.Invoke();
        }        
        // Randomly pick three (3) quests for the day
        for (int i = 0; i < 3; i++)
        {
            int num = GetRandNum(0, questsList.Count);
            
            // NO samesies allowed (?)
            if (questsOfTheDay.Count > 0)
            {
                for (int j = 0; j < questsOfTheDay.Count; j++)
                {
                    if (questsList[num].questTitle == questsOfTheDay[j].questTitle)
                    {
                        num = GetRandNum(0, questsList.Count);
                        break;
                    }
                }
            }

            questsOfTheDay.Add(questsList[num]);
            onAddQuestEvent.Invoke(questsList[num]);
        }
    }

    public void QuestComplete(Quest quest)
    {
        foreach (Quest q in questsOfTheDay.ToList())
        {
            if (q.questTitle == quest.questTitle)
            {
                int questReward = q.questReward;
                // Set quest to completion
                q.isCompleted = true;
                // Turn off Quest
                q.isActive = false; 
                onRemoveQuestEvent.Invoke(q);  
                // Remove quest
                questsOfTheDay.Remove(q);
                // Give reward/s to player
                playerWallet.AddMoney(q.questReward);
                // Inventory.AddItem(quest.rewardsList[GetRandNum(0, quest.RewardsList.Count)])             
                // Play sound
                SingletonManager.Get<AudioManager>().Play(Constants.COINS_SOUND);
                break;
            }
        }
    }

    private int GetRandNum(int min, int max)
    {
        int num;
        num = Random.Range(min, max);
        return num;
    }
}
