using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickHandler : MonoBehaviour
{
    public ConfirmationPanel confirmationPanel; // Reference to the ConfirmationPanel script
    public Transform player; // Reference to the player's transform
    public float maxDistance = 5f; // Maximum distance allowed for clicking
    private Collider objectCollider; // Reference to the collider of the object

    private void Start()
    {
        // Get the collider component attached to the object
        objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        // Calculate the distance between the object and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // Disable the collider if the player is too far away
        if (distance > maxDistance)
        {
            objectCollider.enabled = false;
        }
        else
        {
            objectCollider.enabled = true;
        }
    }

    private void OnMouseDown()
    {
        // Show the confirmation panel when the object is clicked
        confirmationPanel.ShowPanel();
    }
}
