using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourController : MonoBehaviour
{

    private Color colourtoLerpTo; // assigned in the inspector, the colour we want the object to bounce between
    public Color lerpedColor; // just to see in the inspector , this is the current colour rendered on the object
    public float lerpTime; // how fast we want it to bounce between, this doesnt increase the speed, just the time it waits before going to the next colour, 
                           // if lerptime is 0.5 it would go half way before turning back to the other colour, 
                           // if lerptime is 1 it will go all the way to the other colour before coming back
    public float lerpSpeed;

    private SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        colourtoLerpTo = SR.color;
    }

    // Update is called once per frame
    void Update()
    {
        lerpedColor = Color.Lerp(colourtoLerpTo, Color.black, Mathf.PingPong(Time.time * lerpSpeed, lerpTime)); // calculating the colour to lerp to
        SR.color = lerpedColor; // assigning the colour to the object
    }
}

