using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelsCompleted;

    public GameData (GameManager manager)
    {
        levelsCompleted = manager.currentLevel;
    }
}
