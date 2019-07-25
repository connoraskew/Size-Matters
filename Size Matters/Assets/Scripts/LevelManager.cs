using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool devMode;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject Enemy;

    public GameObject[] levels;

    private GameObject currentLevel;

    public int iCurrentLevel;

    GameManager gameManager;

    public float delay;

    public bool isEnding;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindObjectOfType<GameManager>();

        if (devMode)
        {
            gameManager.currentLevel = iCurrentLevel;
            gameManager.SaveData();
        }

        gameManager.LoadData();

        iCurrentLevel = gameManager.currentLevel;

        SetLevel();

        isEnding = false;
    }

    void SetLevel()
    {
        print(iCurrentLevel + " | " + gameManager.currentLevel);

        if (iCurrentLevel >= levels.Length)
        {
            iCurrentLevel = 0;
        }

        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        // create the level
        currentLevel = Instantiate(levels[iCurrentLevel], transform.position, Quaternion.identity);
        // set the players position to be the same as the first gameobject child of the level parent
        // always have "startpos" object as the first child
        ResetEntities();
    }

    void ResetEntities()
    {
        Transform startPos = currentLevel.transform.GetChild(0).transform;
        Transform EnemystartPos = currentLevel.transform.GetChild(1).transform;

        player.transform.position = startPos.position;
        Enemy.transform.position = EnemystartPos.position;

        player.transform.localScale = new Vector3(1, 1, 1);
        Enemy.transform.localScale = new Vector3(1, 1, 1);

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void FadeIn()
    {
        FadeController fadecontroller = FindObjectOfType<FadeController>();
        fadecontroller.fadeIn = true;
    }

    void FadeOut()
    {
        FadeController fadecontroller = FindObjectOfType<FadeController>();
        fadecontroller.fadeIn = false;
    }

    public void PreEndOfLevel()
    {
        if (!isEnding)
        {
            Invoke("EndOfLevel", delay);
            FadeOut();
            isEnding = true;
        }
    }

    // after delay call this
    void EndOfLevel()
    {
        if (iCurrentLevel + 1 == levels.Length)
        {
            iCurrentLevel = 0;
        }
        else
        {
            iCurrentLevel++;
        }
        gameManager.currentLevel++;
        gameManager.SaveData();
        SetLevel();
        ResetEntities();

        isEnding = false;
        Invoke("FadeIn", delay);
    }

    public void HitBadObject()
    {
        if (!isEnding)
        {
            Invoke("Reset", delay);

            FadeOut();

            isEnding = true;
        }
    }

    public void Reset()
    {
        ResetEntities();
        isEnding = false;
        Invoke("FadeIn", delay);
    }


}
