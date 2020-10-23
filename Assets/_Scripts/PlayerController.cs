using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

/********************************************************
PlayerController.cs
Name: Cameron Akey
Student ID: 101166181
Date last modified: 23/10/2020
This is the script that handles the player's position, movement, bounds checking and shooting
Revision history:
-variables for landscape gameplay added (vertical boundary, changed horizontal speed to maxSpeed)
-Added a rotation check, to allow compatibility for any orientation
-Added a position adjustment in the CheckOrientation function so the player keeps their position
-Modified movement and boundary check to detect the orientation first, and use the appropriate code
 *******************************************************/

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    [Header("Boundary Check")]
    public float horizontalBoundary;
    public float verticalBoundary;

    [Header("Player Speed")]
    public float playerSpeed;
    public float maxSpeed;
    public float horizontalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    [Header("Orientation")]
    private int orientation;
    private int orientationCheck;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _CheckOrientation();
        _Move();
        _CheckBounds();
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    private void _Move()
    {
        float direction = 0.0f;

        // touch input support
        if (orientation == 1 || orientation == 2)
        {
            foreach (var touch in Input.touches)
            {
                var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                if (worldTouch.y > transform.position.y)
                {
                    // direction is positive
                    direction = 1.0f;
                }

                if (worldTouch.y < transform.position.y)
                {
                    // direction is negative
                    direction = -1.0f;
                }

                m_touchesEnded = worldTouch;

            }

            // keyboard support
            if (Input.GetAxis("Vertical") >= 0.1f)
            {
                // direction is positive
                direction = 1.0f;
            }
            if (Input.GetAxis("Vertical") <= -0.1f)
            {
                // direction is negative
                direction = -1.0f;
            }

            if (m_touchesEnded.y != 0.0f)
            {
                transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, m_touchesEnded.y, horizontalTValue));
            }
            else
            {
                Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * playerSpeed, 0.0f);
                m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                m_rigidBody.velocity *= 0.99f;
            }
        }
        else if (orientation == 3 || orientation == 4)
        {
            foreach (var touch in Input.touches)
            {
                var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                if (worldTouch.x > transform.position.x)
                {
                    // direction is positive
                    direction = 1.0f;
                }

                if (worldTouch.x < transform.position.x)
                {
                    // direction is negative
                    direction = -1.0f;
                }

                m_touchesEnded = worldTouch;

            }

            // keyboard support
            if (Input.GetAxis("Horizontal") >= 0.1f)
            {
                // direction is positive
                direction = 1.0f;
            }
            if (Input.GetAxis("Horizontal") <= -0.1f)
            {
                // direction is negative
                direction = -1.0f;
            }

            if (m_touchesEnded.x != 0.0f)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnded.x, horizontalTValue), transform.position.y);
            }
            else
            {
                Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * playerSpeed, 0.0f);
                m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                m_rigidBody.velocity *= 0.99f;
            }
        }
        
    }

    private void _CheckBounds()
    {
        if (orientation == 1 || orientation == 2)
        {
            if (transform.position.y >= verticalBoundary)
            {
                transform.position = new Vector3(transform.position.x, verticalBoundary, 0.0f);
            }

            // check left bounds
            if (transform.position.y <= -verticalBoundary)
            {
                transform.position = new Vector3(transform.position.x, -verticalBoundary, 0.0f);
            }
        }
        else if (orientation == 3 || orientation == 4)
        {
            if (transform.position.x >= horizontalBoundary)
            {
                transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
            }

            // check left bounds
            if (transform.position.x <= -horizontalBoundary)
            {
                transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
            }
        }
        else
        {
            Debug.Log("Check Bounds not working due to orientation not being 1-4");
        }
    }

    private void _CheckOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            orientation = 4;

            if (orientationCheck != orientation)
            {
                transform.position = new Vector3(-transform.position.y, -4, 0.0f);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                transform.localScale = new Vector3(2, 2, 1);
            }

            orientationCheck = orientation;
        }
        else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            orientation = 3;

            if (orientationCheck != orientation)
            {
                transform.position = new Vector3(-transform.position.y, 4, 0.0f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = new Vector3(2, 2, 1);
            }

            orientationCheck = orientation;
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            orientation = 2;

            if (orientationCheck != orientation)
            {
                transform.position = new Vector3(-7.5f, -transform.position.x, 0.0f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.localScale = new Vector3(3, 3, 1);
            }

            orientationCheck = orientation;
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            orientation = 1;

            if (orientationCheck != orientation)
            {
                transform.position = new Vector3(7.5f, -transform.position.x, 0.0f);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                transform.localScale = new Vector3(3, 3, 1);
            }

            orientationCheck = orientation;
        }
        else
        {
            orientation = 0;
        }
    }
}
