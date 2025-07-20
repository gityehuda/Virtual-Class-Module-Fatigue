using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 500f;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial rotation of the object
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Check for rotation input
        if (Input.GetMouseButton(1)) // Right-click held
        {
            // Get horizontal mouse movement (left or right)
            float mouseX = Input.GetAxis("Mouse X");

            // Rotate the object based on the mouse X movement
            RotateObjectLeftRight(mouseX);
        }
        else if (Input.GetKey(KeyCode.J)) // J key
        {
            RotateObjectLeft();
        }
        else if (Input.GetKey(KeyCode.L)) // L key
        {
            RotateObjectRight();
        }


        // Reset rotation
        if (Input.GetKeyDown(KeyCode.R)) // R key
        {
            ResetRotation();
        }
    }

    void RotateObjectLeftRight(float direction)
    {
        // Rotate based on mouse X movement
        transform.Rotate(Vector3.up, -direction * rotationSpeed * Time.deltaTime);
    }

    void RotateObjectLeft()
    {
        // Rotate left
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void RotateObjectRight()
    {
        // Rotate right
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    void ResetRotation()
    {
        // Reset the rotation to the initial state
        transform.rotation = initialRotation;
    }
}
