using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class OnboardingManager : MonoBehaviour
{
    public int id;
    public int i = 0;
    public List<Dialogues> dialogueSOList;
    public TextMeshProUGUI panelText;

    private void Start()
    {
        //Debug.Log(dialogueSOList[1].dialogues.Count);
    }

    public void NextButtonHit()
    {
        if (dialogueSOList[id].dialogues.Count <= 1)
        {
            panelText.text = dialogueSOList[id].dialogues[0].ToString();
            id++;
            i = 0;            
        }
        else
        {
            if (i < dialogueSOList[id].dialogues.Count)
            {
                panelText.text = dialogueSOList[id].dialogues[i].ToString();
                i++;                           
            }
            else
            {
                panelText.text = dialogueSOList[id].dialogues[0].ToString();
                id++;
                i = 0;
            }
        }
    }
}
