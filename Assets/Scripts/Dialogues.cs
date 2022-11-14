using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogues Scriptable Object", menuName = "Scriptable Objects/Dialogues")]
public class Dialogues : ScriptableObject
{
    public int id;
    public bool isButtonShown;
    public string dialogueTitle;
    [TextArea] public List<string> dialogues;
}
