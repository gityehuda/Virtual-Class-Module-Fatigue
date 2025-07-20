using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorAntro : MonoBehaviour
{
    public Button spawnButton; // Reference to the button that will spawn the prefab
    public GameObject prefabToSpawn; // Reference to the prefab to be spawned
    public GameObject panelToWatch; // Reference to the panel whose state will trigger the button to appear

    private bool wasPanelActiveLastFrame = false; // Tracks the panel's state in the previous frame

    private void Start()
    {
        // Add the click listener to the button
        spawnButton.onClick.AddListener(HandleButtonClick);

        // Initially hide the button
        spawnButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Check if the panel state has changed from the previous frame
        if (panelToWatch.activeSelf != wasPanelActiveLastFrame)
        {
            // Update the flag to the current state
            wasPanelActiveLastFrame = panelToWatch.activeSelf;

            // Make the button appear if the panel was active and is now inactive
            if (!panelToWatch.activeSelf)
            {
                spawnButton.gameObject.SetActive(true);
            }
        }
    }

    // Method to handle button click
    void HandleButtonClick()
    {
        // Spawn the prefab
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn);
        }

        // Hide the Lecturer GameObject
        GameObject lecturer = GameObject.Find("Lecturer");
        if (lecturer != null)
        {
            lecturer.SetActive(false); // This hides the Lecturer
        }
        else
        {
            Debug.LogWarning("Lecturer GameObject not found.");
        }

        // Disable the button to prevent further clicks
        spawnButton.gameObject.SetActive(false);
    }
}
