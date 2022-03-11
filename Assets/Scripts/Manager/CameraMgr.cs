using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    float mDelta = 10; // Pixels. The width border at the edge in which the movement work
    float mSpeed = 5.0f; // Scale. Speed of the movement

    private Vector3 mRightDirection = Vector3.right;
    private Vector3 mLeftDirection = Vector3.left;
    private Vector3 mUpDirection = Vector3.up;
    private Vector3 mDownDirection = Vector3.down;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check if on the right edge
        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            // Move the camera
            transform.position += mRightDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.x <= 0)
        {
            transform.position += mLeftDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y >= Screen.height - mDelta)
        {
            // Move the camera
            transform.position += mUpDirection * Time.deltaTime * mSpeed;
        }

        if (Input.mousePosition.y <= 70)
        {
            transform.position += mDownDirection * Time.deltaTime * mSpeed;
        }
    }
}