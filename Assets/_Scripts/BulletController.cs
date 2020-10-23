using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************************
BulletController.cs
Name: Cameron Akey
Student ID: 101166181
Date last modified: 23/10/2020
This is the script that handles the bullet's direction, movement, bounds checking and daamge
Revision history:
-Added horizontal boundary, CheckOrientation function, modified Move function to check orientation and change direction accordingly
-Modified CheckBounds to accomodate all 4 direction's limits
 *******************************************************/

public class BulletController : MonoBehaviour, IApplyDamage
{
    public float bulletSpeed;
    public float verticalBoundary;
    public float horizontalBoundary;
    public BulletManager bulletManager;
    public int damage;
    private int orientation;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _CheckOrientation();
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        if (orientation == 1 )
        {
            transform.position -= new Vector3(bulletSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
        else if (orientation == 2)
        {
            transform.position += new Vector3(bulletSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
        else if (orientation == 3)
        {
            transform.position -= new Vector3(0.0f, bulletSpeed, 0.0f) * Time.deltaTime;
        }
        else if (orientation == 4)
        {
            transform.position += new Vector3(0.0f, bulletSpeed, 0.0f) * Time.deltaTime;
        }
        else
        {
            Debug.Log("Orientation error in _Move() in BulletController");
        }
    }

    private void _CheckBounds()
    {
        if (transform.position.x < -horizontalBoundary || transform.position.x > horizontalBoundary || transform.position.y > verticalBoundary || transform.position.y < -verticalBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        bulletManager.ReturnBullet(gameObject);
    }

    public int ApplyDamage()
    {
        return damage;
    }

    private void _CheckOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            orientation = 4;
        }
        else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            orientation = 3;
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            orientation = 2;
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            orientation = 1;

        }
        else
        {
            orientation = 0;
        }
    }
}
