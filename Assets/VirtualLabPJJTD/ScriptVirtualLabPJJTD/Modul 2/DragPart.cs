using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPart : MonoBehaviour
{
    private Vector3 offset;
    private float yPos; // Store the original Y position of the object
    private Camera mainCamera;
    private bool isDragging = false;

    // Reference to TruckDone, to check if part is inside it
    public GameObject TruckDone;

    private void Start()
    {
        mainCamera = Camera.main; // Reference to the main camera
    }

    private void Update()
    {
        // Check if TruckDone is still valid before referencing it
        if (TruckDone == null)
        {
            Debug.LogWarning("TruckDone object has been destroyed.");
            return; // Exit early if TruckDone has been destroyed
        }

        // If the part is already inside TruckDone, disable dragging
        if (transform.IsChildOf(TruckDone.transform))
        {
            isDragging = false;
            return; // Do not allow dragging anymore
        }

        // Check if the mouse button is already being held
        if (Input.GetMouseButton(0)) // Left-click
        {
            if (!isDragging)
            {
                StartDragging(); // Start dragging if not already dragging
            }

            OnMouseDrag(); // Keep dragging while the button is held
        }

        if (Input.GetMouseButtonUp(0)) // Release left-click
        {
            isDragging = false; // Stop dragging when the mouse is released
        }
    }

    private void StartDragging()
    {
        // Record the Y position when dragging starts
        yPos = transform.position.y;

        // Calculate the offset between the object's position and the mouse position in world space
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging) // Only drag if dragging is active
        {
            // Calculate the new position with the offset, but keep the Y position fixed
            Vector3 mouseWorldPosition = GetMouseWorldPosition() + offset;
            transform.position = new Vector3(mouseWorldPosition.x, yPos, mouseWorldPosition.z);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the screen position to world position
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);

        // Find the point where the ray intersects with the XZ plane (Y = 0 or another fixed Y position)
        Plane plane = new Plane(Vector3.up, new Vector3(0, yPos, 0)); // Y = yPos
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance); // Return the point on the XZ plane
        }

        return Vector3.zero;
    }
}
