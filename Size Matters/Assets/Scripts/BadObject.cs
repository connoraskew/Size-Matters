using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadObject : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.HitBadObject();
        }
    }
}
