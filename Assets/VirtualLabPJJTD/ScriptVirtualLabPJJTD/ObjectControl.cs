using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float rotateSpeed = 3f; // Speed of rotation
    public float zoomSpeed = 5f; // Speed of zooming

    public float minX = -10f; // Minimum x position
    public float maxX = 10f;  // Maximum x position
    public float minY = -10f; // Minimum y position
    public float maxY = 10f;  // Maximum y position

    public GameObject nonRotatableObject; // The object that should not rotate (optional)

    private bool lineReachedEndPoint = false; // Flag to track if the line has reached the end point
    public bool allowRotationOnly = false; // Flag to allow only rotation

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (!lineReachedEndPoint) // Allow interaction only if the line has not reached the end point
        {
            // Allow rotation only if allowRotationOnly is true
            if (!allowRotationOnly)
            {
                // Movement controls
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                // Calculate movement vectors in local space
                Vector3 up = transform.TransformDirection(Vector3.up);

                // Move the object relative to its local axes
                Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

                // If moving left or right, use global right direction
                if (horizontalInput != 0)
                {
                    moveDirection.x = Mathf.Sign(horizontalInput);
                    moveDirection.y = 0;
                }

                Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

                // Clamp the new position within the boundaries
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                transform.position = newPosition;

                // Zooming control (scroll wheel)
                float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                float zoomDelta = scrollInput * zoomSpeed * Time.deltaTime;

                // Get the camera's forward direction
                Vector3 cameraForward = Camera.main.transform.forward;

                // Apply the zoom distance to the object along the camera's forward axis
                transform.Translate(cameraForward * zoomDelta, Space.World);
            }

            // Rotation control using J and L keys for 90-degree rotations
            if (Input.GetKeyDown(KeyCode.J))
            {
                RotateObject(Vector3.up, 90f);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                RotateObject(Vector3.up, -90f);
            }

            // Rotation control using right mouse button (left and right only)
            if (Input.GetMouseButton(1)) // Right mouse button is held down
            {
                float mouseX = -Input.GetAxis("Mouse X") * rotateSpeed;

                // Rotate around the Y axis (horizontal mouse movement)
                RotateObject(Vector3.up, mouseX);
            }
        }
    }

    void RotateObject(Vector3 axis, float angle)
    {
        // Check if the object is the one that should not rotate
        if (nonRotatableObject != null && nonRotatableObject == gameObject)
        {
            // Do not rotate the object
            return;
        }

        // Rotate the object around the specified axis by the specified angle
        transform.Rotate(axis, angle, Space.World);
    }

    // Method to set the flag indicating whether the line has reached the end point
    public void SetLineReachedEndPoint(bool value)
    {
        lineReachedEndPoint = value;
    }

    // Method to reset rotation and zoom
    public void ResetRotationAndZoom()
    {
        lineReachedEndPoint = false;
    }

    // Method to disable movement and rotation
    public void DisableMovementAndRotation()
    {
        lineReachedEndPoint = true;
    }

    // Method to enable movement and rotation
    public void EnableMovementAndRotation()
    {
        lineReachedEndPoint = false;
    }

    // Method to enable only rotation
    public void EnableRotationOnly(bool enable)
    {
        allowRotationOnly = enable;
    }
}