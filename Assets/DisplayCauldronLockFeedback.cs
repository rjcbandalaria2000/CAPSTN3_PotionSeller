using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCauldronLockFeedback : MonoBehaviour
{
    public float duration;
    public TextMeshProUGUI text;

    private CraftingManager craftingManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartCauldronFeedback()
    {

    }

    IEnumerator CauldronFeedback()
    {
        yield return null;
    }
}
