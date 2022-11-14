using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLevel : MonoBehaviour
{
    [Header("Unity Events")]
    public OnGainExp onGainExp = new();
    public OnRefreshLevelUI onRefreshLevelUI = new();
    
    [Header("Values")]
    public int          Level;
    public List<int>    MaxExperiencePoints;
    public int          CurrentExperiencePoints;

    

    private void Awake()
    {
        SingletonManager.Register(this);
       
        onGainExp.AddListener(AddExpPoints);

      //  Level = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
       

        onRefreshLevelUI.Invoke();
        SingletonManager.Get<FurnitureManager>().ActivateFurniture(Level);
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
        onRefreshLevelUI.Invoke();
    }

    public void LevelUp()
    {
        if(Level < MaxExperiencePoints.Count)
        {
            CurrentExperiencePoints -= MaxExperiencePoints[Level];
            Level++;
            SingletonManager.Get<FurnitureManager>().ActivateFurniture(Level);

        }
    }

    public float GetNormalizedExpPoints()
    {
        return (float)CurrentExperiencePoints / (float)MaxExperiencePoints[Level];
    }
}
