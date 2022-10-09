using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Scriptable Object", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public bool isActive;
    public bool isCompleted;    
    public string questTitle;
    [TextArea] public string questDescription;
    public int questReward;
}
