using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLevel : MonoBehaviour
{
    public int Level;
    public List<int> MaxExperiencePoints;
    public int CurrentExperiencePoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddExpPoints(int value)
    {
        CurrentExperiencePoints += value;
        if(CurrentExperiencePoints >= MaxExperiencePoints[Level])
        {
            Level++;
            int tempExpPoints = CurrentExperiencePoints - MaxExperiencePoints[Level];
            if(tempExpPoints > 0)
            {
                CurrentExperiencePoints = tempExpPoints;
            }
        }
    }
}
