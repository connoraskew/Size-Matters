using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{

    public bool fadeIn;
    public bool isMenu;
    public float scaleMultiplier = 1;
    private float actualScaleMultiplier = 1;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;

        actualScaleMultiplier = 1 / scaleMultiplier;

        fadeIn = true;
        isMenu = false;
    }

    void Update()
    {
        if (fadeIn == true)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * actualScaleMultiplier;
            }
            else
            {
                if (canvasGroup.blocksRaycasts != false)
                {
                    canvasGroup.blocksRaycasts = false;
                }
            }
        }
        else
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime * actualScaleMultiplier;

                if (canvasGroup.blocksRaycasts != true)
                {
                    canvasGroup.blocksRaycasts = true;
                }
            }
            else if(isMenu)
            {
                MainMenu mainMenu = FindObjectOfType<MainMenu>();
                mainMenu.PlayGame();
            }
        }
    }

    public void Play()
    {
        isMenu = true;
        fadeIn = false;
    }
}