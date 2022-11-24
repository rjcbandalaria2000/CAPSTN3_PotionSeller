using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class Stock : SelectableObject
{
    public OnboardingClickEvent onOnboardingClickEvent = new();
    [Header("Stock Variables")]
    public List<GameObject> stockObjects = new();
    public GameObject itemPanelPrefab;
    public GameObject stockPanelParent;
    public Inventory playerInventory;
    //private Inventory playerInventory = SingletonManager.Get<Inventory>(); currently does not work

    private void Awake()
    {
        //playerInventory = SingletonManager.Get<Inventory>();
        //playerInventory = SingletonManager.Get<Inventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
     
        objectPanelUI.SetActive(false);
        //playerInventory = SingletonManager.Get<Inventory>();
    }
    private void OnEnable()
    {
        //if(playerInventory == null)
        //{
        //    playerInventory = SingletonManager.Get<Inventory>();          
        //}
        playerInventory.onAddItemEvent.AddListener(CreateStock);
        playerInventory.onRemoveItemEvent.AddListener(RemoveStock);
        onSelectableObjectClickedEvent.AddListener(OnInteract);
        //SingletonManager.Get<Inventory>().onAddItemEvent.AddListener(InstantiateObject);
       
    }

    private void OnDisable()
    {
        onSelectableObjectClickedEvent.RemoveListener(OnInteract);
        //SingletonManager.Get<Inventory>().onAddItemEvent.RemoveListener(InstantiateObject);
        playerInventory.onAddItemEvent.RemoveListener(CreateStock);
        playerInventory.onRemoveItemEvent.RemoveListener(RemoveStock);
    }

    public void CreateStock(ScriptableObject scriptableObject, int amount)
    {
        //Debug.Log(name);
        bool isItemFound = false;
        if (stockObjects.Count > 0)
        {
            foreach (GameObject gameObject in stockObjects)
            {
                //Debug.Log(gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text);
                if (gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text == scriptableObject.name)
                {
                    isItemFound = true;
                    //Debug.Log("Found it! Updating...");
                    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Quantity: " + amount;
                    break;
                }
            }
        }
        if (!isItemFound)
        {            
            //Debug.Log(scriptableObject.GetType());
            GameObject item = Instantiate(itemPanelPrefab, stockPanelParent.transform, false);
            if (scriptableObject.GetType() == typeof(PotionScriptableObject))
            {
                PotionScriptableObject potionScriptableObject = (PotionScriptableObject)scriptableObject;
                item.transform.GetChild(1).GetComponent<Image>().sprite = potionScriptableObject.potionIconSprite;
            }
            else if (scriptableObject.GetType() == typeof(IngredientScriptableObject))
            {
                IngredientScriptableObject ingredientScriptableObject = (IngredientScriptableObject)scriptableObject;
                item.transform.GetChild(1).GetComponent<Image>().sprite = ingredientScriptableObject.ingredientSprite;
            }
            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Quantity: " + amount;
            item.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = scriptableObject.name;
            stockObjects.Add(item);
        }
    }

    public void RemoveStock(ScriptableObject scriptableObject, int amount)
    {
        foreach(GameObject gameObject in stockObjects)
        {
            if (gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text == scriptableObject.name)
            {
                gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Quantity: " + amount;    
                if (gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == $"Quantity: 0")
                {
                    stockObjects.Remove(gameObject);
                    Destroy(gameObject);
                }
                break;
            }
        }
    }

    public override void OnInteract()
    {
        //base.OnInteract();
        onOnboardingClickEvent.Invoke();

        if (objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);
    }

    public void CloseUIPanel()
    {
        objectPanelUI.SetActive(false);
    }
}
