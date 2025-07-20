using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    // Set the initial position, rotation, and scale in the Unity Editor
    public Vector3 spawnPosition = new Vector3(2.320009f, 1.108f, 8.528004f);
    public Quaternion spawnRotation = Quaternion.Euler(0f, 180f, 0f);
    public Vector3 spawnScale = new Vector3(1f, 1f, 1f);

    private bool canReset = true;  // Flag to control the reset functionality

    private void Start()
    {
        // Store the initial position, rotation, and scale
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Check if "R" key is pressed and reset is allowed
        if (Input.GetKeyDown(KeyCode.R) && canReset)
        {
            // Reset the object to its initial state
            ResetObject();
        }
    }

    public void ResetObject()
    {
        // Reset the position, rotation, and scale to their initial values
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
    }

    public void ResetToSpecificPosition()
    {
        // Reset the position, rotation, and scale to the specific values provided
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        transform.localScale = spawnScale;
    }

    // Method to enable or disable the reset functionality
    public void SetCanReset(bool value)
    {
        canReset = value;
    }
}
