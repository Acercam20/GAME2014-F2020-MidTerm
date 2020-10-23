using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************************
EnemyController.cs
Name: Cameron Akey
Student ID: 101166181
Date last modified: 23/10/2020
This is the script that handles the enemy's position, movement and bounds checking
Revision history:
-Added vertical boundary, the CheckOrientation function, and modified the Move function to check orientation
-Modified CheckBounds to accomadate multiple orientations
-Added a position, rotation, and scalar change when the orientation changes
 *******************************************************/

public class EnemyController : MonoBehaviour
{
    public float enemySpeed;
    public float horizontalBoundary;
    public float verticalBoundary;
    public float direction;
    private int orientation;
    private int orientationCheck;

    // Update is called once per frame
    void Update()
    {
        _CheckOrientation();
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        if (orientation == 1 || orientation == 2)
        {
            transform.position += new Vector3(0.0f, enemySpeed * direction * Time.deltaTime, 0.0f);
        }
        else if (orientation == 3 || orientation == 4)
        {
            transform.position += new Vector3(enemySpeed * direction * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            Debug.Log("Orientation error in _Move() in EnemyController");
        }
    }

    private void _CheckBounds()
    {
        if (orientation == 1 || orientation == 2)
        {
            // check right boundary
            if (transform.position.y >= verticalBoundary)
            {
                direction = -1.0f;
            }

            // check left boundary
            if (transform.position.y <= -verticalBoundary)
            {
                direction = 1.0f;
            }
        }
        else if (orientation == 3 || orientation == 4)
        {
            // check right boundary
            if (transform.position.x >= horizontalBoundary)
            {
                direction = -1.0f;
            }

            // check left boundary
            if (transform.position.x <= -horizontalBoundary)
            {
                direction = 1.0f;
            }
        }
        else
        {
            Debug.Log("Orientation error in _CheckBounds in EnemyController");
        }
    }

    private void _CheckOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            orientation = 4;

            if (orientationCheck != orientation)
            {
                transform.position = new Vector3(-transform.position.y, Random.Range(4.0f, 2.0f), 0.0f);
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
                transform.position = new Vector3(-transform.position.y, Random.Range(-4.0f, -2.0f), 0.0f);
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
                transform.position = new Vector3(Random.Range(8.0f, 5.0f), -transform.position.x, 0.0f);
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
                transform.position = new Vector3(Random.Range(-8.0f, -5.0f), -transform.position.x, 0.0f);
                transform.rotation = Quaternion.Euler(0, 0, -90);
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
