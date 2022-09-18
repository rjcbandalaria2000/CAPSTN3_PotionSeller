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
        if (Level < MaxExperiencePoints.Count)
        {

            if (CurrentExperiencePoints >= MaxExperiencePoints[Level])
            {
                LevelUp();
            }

        }
       
    }

    public void LevelUp()
    {
        if(Level < MaxExperiencePoints.Count)
        {
            CurrentExperiencePoints -= MaxExperiencePoints[Level];
            Level++;
        }
    }
}
