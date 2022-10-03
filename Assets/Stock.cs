using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEditor.Progress;

public class Stock : SelectableObject
{
    [Header("Stock Variables")]
    public List<GameObject> stockObjects = new();
    public GameObject itemPanelPrefab;
    public GameObject stockPanelParent;
    public Inventory playerInventory;
    //private Inventory playerInventory = SingletonManager.Get<Inventory>(); currently does not work

    // Start is called before the first frame update
    void Start()
    {
        objectPanelUI.SetActive(false);
    }
    private void OnEnable()
    {
        onSelectableObjectClickedEvent.AddListener(OnInteract);
        //SingletonManager.Get<Inventory>().onAddItemEvent.AddListener(InstantiateObject);
        playerInventory.onAddItemEvent.AddListener(CreateStock);
        playerInventory.onRemoveItemEvent.AddListener(RemoveStock);
    }

    private void OnDisable()
    {
        onSelectableObjectClickedEvent.RemoveListener(OnInteract);
        //SingletonManager.Get<Inventory>().onAddItemEvent.RemoveListener(InstantiateObject);
        playerInventory.onAddItemEvent.RemoveListener(CreateStock);
        playerInventory.onRemoveItemEvent.RemoveListener(RemoveStock);
    }

    public void CreateStock(string name, int amount)
    {
        //Debug.Log(name);
        bool isItemFound = false;
        foreach (GameObject gameObject in stockObjects)
        {
            //Debug.Log(gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text);
            if (gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text == name)
            {
                isItemFound = true;
                //Debug.Log("Found it! Updating...");
                gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Amount: " + amount;
                break;
            }            
        }
        if (!isItemFound)
        {
            GameObject item = Instantiate(itemPanelPrefab, stockPanelParent.transform, false);
            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Amount: " + amount;
            item.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            stockObjects.Add(item);
        }
    }

    public void RemoveStock(string name, int amount)
    {
        foreach(GameObject gameObject in stockObjects)
        {
            if (gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text == name)
            {
                gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Amount: " + amount;    
                if (gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == $"Amount: 0")
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
        if (objectPanelUI == null) { return; }
        objectPanelUI.SetActive(true);
    }

    public void CloseUIPanel()
    {
        objectPanelUI.SetActive(false);
    }
}
