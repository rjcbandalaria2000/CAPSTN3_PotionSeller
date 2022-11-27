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
    }

    // Start is called before the first frame update
    void Start()
    {
        onRefreshLevelUI.Invoke();
        SingletonManager.Get<FurnitureManager>()?.ActivateFurniture(Level);
    }

    public void AddExpPoints(int value)
    {   if (Level <= MaxExperiencePoints.Count - 1)
        {
            
            CurrentExperiencePoints += value;
            if(Level+1 >= MaxExperiencePoints.Count)
            {
                if(CurrentExperiencePoints >= MaxExperiencePoints[Level])
                {
                    CurrentExperiencePoints = MaxExperiencePoints[Level];
                }
            }
            if (CurrentExperiencePoints >= MaxExperiencePoints[Level])
            {
                LevelUp();
            }
            onRefreshLevelUI.Invoke();
        }
        
    }

    public void LevelUp()
    {
        if(Level < MaxExperiencePoints.Count-1)
        {
            CurrentExperiencePoints -= MaxExperiencePoints[Level];
            Level++;
            SingletonManager.Get<FurnitureManager>().ActivateFurniture(Level);
            SingletonManager.Get<CustomerSpawner>().initializeUnlockPotion();
            SingletonManager.Get<AudioManager>().Play("levelup");
        }
    }

    public float GetNormalizedExpPoints()
    {
        return (float)CurrentExperiencePoints / (float)MaxExperiencePoints[Level];
    }
}
