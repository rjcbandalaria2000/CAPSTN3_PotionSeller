using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class DisplayWallet : MonoBehaviour
{
    public GameObject Player;
    public TextMeshProUGUI MoneyCountText;
    private Wallet wallet;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(Player, "Player is null or empty");
        Wallet playerWallet = Player.GetComponent<Wallet>();
        if (playerWallet)
        {
            wallet = playerWallet;
        }
        if (wallet)
        {
            wallet.WalletUIUpdate.AddListener(DisplayWalletCount);
        }

    }
    
    void DisplayWalletCount()
    {
        Assert.IsNotNull(MoneyCountText, "Money count text is null or empty");
        MoneyCountText.text = "$ " + wallet.Money.ToString();
    }
}
