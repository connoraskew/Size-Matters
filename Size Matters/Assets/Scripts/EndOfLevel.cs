using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.PreEndOfLevel();
        }
    }
}
