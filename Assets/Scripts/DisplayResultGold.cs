using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayResultGold : MonoBehaviour
{
    public Wallet playerWallet;
    public TextMeshProUGUI resultGoldText; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        resultGoldText = this.GetComponent<TextMeshProUGUI>();
        ShowPlayerGold();
    }

    public void ShowPlayerGold()
    {
        if(playerWallet == null) { return; }
        resultGoldText.text = "Gold: " + playerWallet.Money.ToString("0");
    }
}
