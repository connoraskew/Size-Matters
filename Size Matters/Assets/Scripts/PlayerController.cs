using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;

    [SerializeField] private float moveSpeed = 3;

    private float horizontalMove;

    private Rigidbody2D rb2D;

    private BoxCollider2D myCollider;

    public LayerMask ground;

    float shortestRay;
    string closestHitString;

    public float scaleMultiplyer; // how fast the player changes size

    public float cussionZone; // a little breathing room, like "skin width"

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        shortestRay = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
    }

    void HorizontalMovement()
    {
        if (joystick.Horizontal >= 0.2f)
        {
            rb2D.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            rb2D.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            horizontalMove = 0;
        }

        rb2D.AddForce(Vector3.right * horizontalMove, ForceMode2D.Force);
    }

    void VerticalMovement()
    {
        float maxDistance = 100f;

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right, maxDistance, ground);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -transform.up, maxDistance, ground);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, -transform.right, maxDistance, ground);
        RaycastHit2D hit4 = Physics2D.Raycast(transform.position, transform.up, maxDistance, ground);

        Debug.DrawRay(transform.position, transform.right * hit1.distance, Color.red);
        Debug.DrawRay(transform.position, -transform.up * hit2.distance, Color.green);
        Debug.DrawRay(transform.position, -transform.right * hit3.distance, Color.blue);
        Debug.DrawRay(transform.position, transform.up * hit4.distance, Color.grey);
        
        RayCastHitItterations(hit1, "hit1");
        RayCastHitItterations(hit2, "hit2");
        RayCastHitItterations(hit3, "hit3");
        RayCastHitItterations(hit4, "hit4");

        shortestRay = 100f;

        Vector3 newScale = transform.localScale;


        if (closestHitString.Contains("2") || closestHitString.Contains("4"))
        {
            if (joystick.Vertical >= 0.5f)
            {
                if (transform.localScale.x >= 0.3f)
                {
                    newScale -= new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);
                    newScale += new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);
                    //
                    transform.position += new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);
                    //
                    transform.localScale = newScale;
                }
            }
            else if ((joystick.Vertical <= -0.5f))
            {
                if (transform.localScale.y >= 0.3f)
                {
                    newScale += new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);
                    newScale -= new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);

                    transform.position -= new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);

                    transform.localScale = newScale;
                }
            }
            else if ((joystick.Vertical <= -0.1f) && (joystick.Vertical >= 0.1f))
            {
                if (transform.localScale.y >= 1f + cussionZone)
                {
                    newScale += new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);
                    newScale -= new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);

                    transform.position -= new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);

                    transform.localScale = newScale;
                }
                else if (transform.localScale.x >= 1f + cussionZone)
                {
                    newScale -= new Vector3(scaleMultiplyer * Time.deltaTime * 0.5f, 0, 0);
                    newScale += new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);

                    transform.position += new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);

                    transform.localScale = newScale;
                }
            }
            else
            {

            }
        }
        else
        {
            if (joystick.Vertical >= 0.5f)
            {
                if (transform.localScale.y >= 0.3f)
                {
                    newScale -= new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);
                    newScale += new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);
                    //
                    transform.position += new Vector3(scaleMultiplyer * Time.deltaTime * 0.5f, 0, 0);
                    //
                    transform.localScale = newScale;
                }
            }
            else if ((joystick.Vertical <= -0.5f))
            {
                if (transform.localScale.x >= 0.3f)
                {
                    newScale += new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);
                    newScale -= new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);

                    transform.position += new Vector3(scaleMultiplyer * Time.deltaTime * 0.5f, 0, 0);

                    transform.localScale = newScale;
                }
            }
            else if ((joystick.Vertical <= -0.1f) && (joystick.Vertical >= 0.1f))
            {
                if (transform.localScale.y <= 1f - cussionZone)
                {
                    print("HERE CUNT");
                    newScale += new Vector3(0, scaleMultiplyer * Time.deltaTime, 0);
                    newScale -= new Vector3(scaleMultiplyer * Time.deltaTime, 0, 0);

                    transform.position -= new Vector3(scaleMultiplyer * Time.deltaTime * 0.5f, 0, 0);

                    transform.localScale = newScale;
                }
                else if (transform.localScale.x <= 1f - cussionZone)
                {
                    print("HERE CUNT 2");
                    newScale -= new Vector3(0, scaleMultiplyer * Time.deltaTime * 0.5f, 0);
                    newScale += new Vector3(scaleMultiplyer * Time.deltaTime * 0.5f, 0, 0);

                    transform.position -= new Vector3(scaleMultiplyer * Time.deltaTime * 0.5f, 0, 0);

                    transform.localScale = newScale;
                }
            }
        }
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