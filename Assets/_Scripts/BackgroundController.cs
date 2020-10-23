using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/********************************************************
BackgroundController.cs
Name: Cameron Akey
Student ID: 101166181
Date last modified: 23/10/2020
This is the script that handles the backgrounds's position, movement, bounds checking and position resetting
Revision history:
-Added horizontal boundary, changed verticalSpeed to backgroundSpeed, added the CheckOrientation function
-Forced a position reset in the CheckOrientation function
-Adjusted rotation and scale of the background based on orientation
-Created a bool to distinguish between the two background objects
 *******************************************************/

public class BackgroundController : MonoBehaviour
{
    public float backgroundSpeed;
    public float verticalBoundary;
    public float horizontalBoundary;
    public bool resetAtTop;
    private int orientation;
    private int orientationCheck;

    // Update is called once per frame
    void Update()
    {
        _CheckOrientation();
        _Move();
        _CheckBounds();
    }

    private void _Reset()
    {
        if (orientation == 1)
        {
            transform.position = new Vector3(horizontalBoundary, 0.0f);
        }
        else if (orientation == 2)
        {
            transform.position = new Vector3(-horizontalBoundary, 0.0f);
        }
        else if (orientation == 3)
        {
            transform.position = new Vector3(0.0f, -verticalBoundary);
        }
        else if (orientation == 4)
        {
            transform.position = new Vector3(0.0f, verticalBoundary);
        }
        else
        {
            Debug.Log("Orientation error in _Reset() function in Background Controller");
        }
    }

    private void _Move()
    {
        if (orientation == 1)
        {
            transform.position -= new Vector3(backgroundSpeed, 0.0f) * Time.deltaTime;
        }
        else if (orientation == 2)
        {
            transform.position += new Vector3(backgroundSpeed, 0.0f) * Time.deltaTime;
        }
        else if (orientation == 3)
        {
            transform.position += new Vector3(0.0f, backgroundSpeed) * Time.deltaTime;
        }
        else if (orientation == 4)
        {
            transform.position -= new Vector3(0.0f, backgroundSpeed) * Time.deltaTime;
        }
        else
        {
            Debug.Log("Orientation error in _Move() function in Background Controller");
        }
        //transform.position -= new Vector3(0.0f, backgroundSpeed) * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        // if the background is lower than the bottom of the screen then reset
        if (orientation == 1)
        {
            if (transform.position.x <= -horizontalBoundary)
            {
                _Reset();
            }
        }
        else if (orientation == 2)
        {
            if (transform.position.x >= horizontalBoundary)
            {
                _Reset();
            }
        }
        else if (orientation == 3)
        {
            if (transform.position.y >= verticalBoundary)
            {
                _Reset();
            }
        }
        else if (orientation == 4)
        {
            if (transform.position.y <= -verticalBoundary)
            {
                _Reset();
            }
        }
    }

    private void _CheckOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            orientation = 4;

            if (orientationCheck != orientation)
            {
                if (resetAtTop)
                {
                    transform.position = new Vector3(0.0f, 5.0f, 0.0f);
                }
                else
                {
                    transform.position = new Vector3(0.0f, -5.0f, 0.0f);
                }
                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.localScale = new Vector3(3, 3, 1);
            }

            orientationCheck = orientation;
        }

        else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            orientation = 3;

            if (orientationCheck != orientation)
            {
                if (resetAtTop)
                {
                    transform.position = new Vector3(0.0f, -5.0f, 0.0f);
                }
                else
                {
                    transform.position = new Vector3(0.0f, 5.0f, 0.0f);
                }
                transform.rotation = Quaternion.Euler(0, 0, 270);
                transform.localScale = new Vector3(3, 3, 1);
            }

            orientationCheck = orientation;
        }

        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            orientation = 2;

            if (orientationCheck != orientation)
            {
                if (resetAtTop)
                {
                    transform.position = new Vector3(-8.5f, 0.0f, 0.0f);
                }
                else
                {
                    transform.position = new Vector3(8.5f, 0.0f, 0.0f);
                }
                transform.rotation = Quaternion.Euler(10, 0, 0);
                transform.localScale = new Vector3(6, 5, 1);
            }

            orientationCheck = orientation;
        }

        else if (Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            orientation = 1;

            if (orientationCheck != orientation)
            {
                if (resetAtTop)
                {
                    transform.position = new Vector3(8.5f, 0.0f, 0.0f);
                }
                else
                {
                    transform.position = new Vector3(-8.5f, 0.0f, 0.0f);
                }
                transform.rotation = Quaternion.Euler(-10, 0, 180);
                transform.localScale = new Vector3(6, 5, 1);
            }

            orientationCheck = orientation;
        }

        else
        {
            orientation = 0;
        }
    }
}
