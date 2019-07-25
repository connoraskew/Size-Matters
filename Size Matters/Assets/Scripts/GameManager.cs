using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // basically load and save
    public int currentLevel;

    public void SaveData()
    {
        GameData data = SaveSystem.LoadData();

        SaveSystem.SaveData(this);
    }
    
    public void LoadData()
    {
        GameData data = SaveSystem.LoadData();

        currentLevel = data.levelsCompleted;
    }
}
