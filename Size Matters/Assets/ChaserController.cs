using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : MonoBehaviour
{

    private GameObject chaserGFX;

    [SerializeField] private float moveSpeed = 2;

    private Rigidbody2D rb2D;

    private BoxCollider2D myCollider;

    public LayerMask ground;
    public LayerMask obstacle;

    float shortestRay;
    string closestHitString;

    public float scaleMultiplyer; // how fast the player changes size

    public float excessSpace; // a little breathing room, like "skin width"

    [SerializeField] private bool hasDetectedRight;
    [SerializeField] private bool hasDetectedUp;
    [SerializeField] private bool HasPassed;

    void Awake()
    {
        chaserGFX = transform.GetChild(0).gameObject;

        rb2D = chaserGFX.GetComponent<Rigidbody2D>();
        myCollider = chaserGFX.GetComponent<BoxCollider2D>();
        shortestRay = 100f;

        HasPassed = true;
        hasDetectedRight = false;
        hasDetectedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float maxDistance = 100f;

        // shoots 4 rays in all directions to see which ray is closest to the floor
        // to see what our rotation is without using transform.rotation (quaternions are a pain)
        RaycastHit2D hit1 = Physics2D.Raycast(chaserGFX.transform.position, transform.right, maxDistance, ground);
        RaycastHit2D hit2 = Physics2D.Raycast(chaserGFX.transform.position, -transform.up, maxDistance, ground);
        RaycastHit2D hit3 = Physics2D.Raycast(chaserGFX.transform.position, -transform.right, maxDistance, ground);
        RaycastHit2D hit4 = Physics2D.Raycast(chaserGFX.transform.position, transform.up, maxDistance, ground);

        // draws the 4 rays for visuals
        Debug.DrawRay(chaserGFX.transform.position, transform.right * hit1.distance, Color.red);
        Debug.DrawRay(chaserGFX.transform.position, -transform.up * hit2.distance, Color.green);
        Debug.DrawRay(chaserGFX.transform.position, -transform.right * hit3.distance, Color.blue);
        Debug.DrawRay(chaserGFX.transform.position, transform.up * hit4.distance, Color.grey);

        // checks the distance of all the rays through a system of finding the shortest one
        RayCastHitItterations(hit1, "hit1");
        RayCastHitItterations(hit2, "hit2");
        RayCastHitItterations(hit3, "hit3");
        RayCastHitItterations(hit4, "hit4");

        shortestRay = 100f;

        maxDistance = 1f;

        Vector3 topRightOffset = chaserGFX.transform.position + new Vector3(chaserGFX.transform.localScale.x * 0.5f, chaserGFX.transform.localScale.y * 0.5f, 0f);
        Vector3 toprightSpacing = new Vector3(topRightOffset.x, topRightOffset.y + excessSpace, topRightOffset.z);
        RaycastHit2D topRight = Physics2D.Raycast(toprightSpacing, transform.right, maxDistance, obstacle);
        Debug.DrawRay(toprightSpacing, transform.right * topRight.distance, Color.yellow);

        Vector3 topLeftOffset = chaserGFX.transform.position + new Vector3(chaserGFX.transform.localScale.x * -0.5f, chaserGFX.transform.localScale.y * 0.5f, 0f);
        Vector3 topLeftSpacing = new Vector3(topLeftOffset.x - excessSpace, topLeftOffset.y, topLeftOffset.z);
        RaycastHit2D topLeft = Physics2D.Raycast(topLeftSpacing, transform.up, maxDistance, obstacle);
        Debug.DrawRay(topLeftSpacing, transform.up * topLeft.distance, Color.yellow);


        if (topRight.distance != 0)
        {
            hasDetectedRight = true;
            HasPassed = false;
        }
        else
        {
            hasDetectedRight = false;
        }

        if (topLeft.distance != 0)
        {
            hasDetectedUp = true;
        }
        else
        {
            if(hasDetectedUp)
            {
                HasPassed = true;
            }
        }



        Vector3 newScale = chaserGFX.transform.localScale;

        if (closestHitString.Contains("2") || closestHitString.Contains("4"))
        {
            if (hasDetectedRight)
            {
                newScale += new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);
                newScale -= new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);

                chaserGFX.transform.position -= new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);

                chaserGFX.transform.localScale = newScale;
            }
            else if (HasPassed && chaserGFX.transform.localScale.x > 1)
            {
                newScale -= new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);
                newScale += new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);
                //
                chaserGFX.transform.position += new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);
                //
                chaserGFX.transform.localScale = newScale;
            }

        }
        else
        {

        }
    


        // reset for shortest ray testing. comes at the end of the raycast length testing

        rb2D.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
        //print("moving");
    }

    void RayCastHitItterations(RaycastHit2D hit, string hitString)
    {
        if (hit.distance != 0)
        {
            if (hit.distance < shortestRay)
            {
                shortestRay = hit.distance;
                closestHitString = hitString;
            }
        }
    }
}
