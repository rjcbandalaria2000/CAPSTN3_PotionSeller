using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Potion : MonoBehaviour
{
    public TextMeshProUGUI nameUI;

    public PotionScriptableObject baseObj;
    public bool isSelect;

    // Start is called before the first frame update
    void Start()
    {
        nameUI.text = baseObj.name.ToString();
        isSelect = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
